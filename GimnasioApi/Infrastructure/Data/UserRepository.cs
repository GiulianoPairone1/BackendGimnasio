using Domain.Entities;
using Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UserRepository:EfRepository<User>,IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        //Para recuperar contraseña
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User?> GetByResetTokenAsync(string token)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == token);
        }
        public virtual async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

    }
}
