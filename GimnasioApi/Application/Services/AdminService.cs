using Application.Interfaces;
using Domain.Interfaces;
using Application.Models.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;   

        public AdminService(IAdminRepository adminRepository, IUserRepository userRepository) 
        {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
        }

        public List<AdminDTO> GetAll()
        {
            return _adminRepository.GetAll()
                .Select(admin => new AdminDTO
                {
                    Name = admin.Name,
                    Surname = admin.Surname,
                    Email = admin.Email,
                })
                .ToList();
        }

        public AdminDTO Create(AdminDTO clientDto)
        {
            var admin = clientDto.ToAdmin();
            var addAdmin = _adminRepository.add(admin);
            return AdminDTO.FromAdmin(addAdmin);
        }

        public User GetUserByEmail(string email)
        {
            var user = _adminRepository.GetUserByEmail(email) ?? throw new KeyNotFoundException("Usuario no encontrado.");
            return user;
        }

        public List<User> GetUsersAvailable()
        {
            return _adminRepository.GetUsersAvailable();
        }

        public List<T> GetUsersAvailable<T>() where T : User
        {
            return _adminRepository.GetUsersAvailable<T>() ?? throw new KeyNotFoundException("No hay usuarios de este tipo aún.");
        }

        public bool UpdateRoleUser(string mail, UserType newRole)
        {
            var user = _adminRepository.GetUserByEmail(mail)
                      ?? throw new KeyNotFoundException("Usuario no encontrado.");

            if (!user.IsAvailable)
                throw new InvalidOperationException("No se puede actualizar el rol de un usuario eliminado.");

            if (user.UserType == newRole)
                throw new InvalidOperationException("El usuario ya tiene el rol especificado.");

            user.UserType = newRole;
            _userRepository.update(user);
            return true;
        }

        public bool DeleteUser(string mail)
        {
            var existingUser = _adminRepository.GetUserByEmail(mail)
                                    ?? throw new KeyNotFoundException("No se encontró el usuario.");


            if (!existingUser.IsAvailable)
            {
                throw new InvalidOperationException("El usuario ya ha sido eliminado.");
            }


            existingUser.IsAvailable = false;
            _userRepository.update(existingUser);

            return true;
        }
    }
}
