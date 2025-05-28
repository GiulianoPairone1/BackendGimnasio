using Application.Interfaces;
using Domain.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) throw new KeyNotFoundException("Usuario no encontrado.");

            var token = Guid.NewGuid().ToString(); // O usar un JWT con expiración si prefieres
            user.PasswordResetToken = token;
            user.TokenExpiration = DateTime.UtcNow.AddMinutes(30);
            await _userRepository.UpdateAsync(user);

            return token;
        }

        public async Task<bool> ResetPasswordAsync(string token, string newPassword)
        {
            var user = await _userRepository.GetByResetTokenAsync(token);
            if (user == null || user.TokenExpiration < DateTime.UtcNow)
                throw new InvalidOperationException("Token inválido o expirado.");

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword); // Usa el hash que ya estés utilizando
            user.PasswordResetToken = null;
            user.TokenExpiration = null;

            await _userRepository.UpdateAsync(user);
            return true;
        }
    }

}
