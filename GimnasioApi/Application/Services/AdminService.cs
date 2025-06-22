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
        private readonly IClientGymSessionRepository _clientGymSessionRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ITrainerRepository _trainerRepository;
        private readonly IGymSessionService _gymSessionService;


        public AdminService(IAdminRepository adminRepository, IUserRepository userRepository, IClientGymSessionRepository clientGymSessionRepository, IClientRepository clientRepository, ITrainerRepository trainerRepository, IGymSessionService gymSessionService) 
        {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
            _clientGymSessionRepository = clientGymSessionRepository;
            _clientRepository = clientRepository;
            _trainerRepository = trainerRepository;
            _gymSessionService = gymSessionService;
        }

        public List<AdminDTO> GetAll()
        {
            var admins = _adminRepository.GetAll();
            if (admins == null || !admins.Any())
            {
                throw new KeyNotFoundException("No se encontraron administradores.");
            }

            return admins.Select(admin => new AdminDTO
            {
                Name = admin.Name,
                Surname = admin.Surname,
                Email = admin.Email,
            })
            .ToList();
        }

        public AdminDTO Create(AdminDTO adminDto)
        {
            if (adminDto == null)
                throw new ArgumentNullException(nameof(adminDto), "El DTO del administrador no puede ser nulo.");

            if (_adminRepository.GetUserByEmail(adminDto.Email) != null)
            {
                throw new InvalidOperationException("Ya existe un administrador con ese correo electrónico.");
            }

            var admin = adminDto.ToAdmin();
            var addAdmin = _adminRepository.add(admin);
            return AdminDTO.FromAdmin(addAdmin);
        }

        public UserDTO GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El correo electrónico no puede ser nulo o vacío.", nameof(email));

            var user = _adminRepository.GetUserByEmail(email)
                           ?? throw new KeyNotFoundException("Usuario no encontrado.");


            return UserDTO.FromUser(user);
        }


        public List<UserDTO> GetUsersAvailable()
        {
            var users = _adminRepository.GetUsersAvailable();

            if (users == null || !users.Any())
                throw new KeyNotFoundException("No hay usuarios disponibles.");

            return users.Select(UserDTO.FromUser).ToList();
        }

        public List<UserDTO> GetUsersNotAvailable()
        {
            var users = _adminRepository.GetUsersNotAvailable();

            if (users == null || !users.Any())
                throw new KeyNotFoundException("No hay usuarios suspendidos.");

            return users.Select(UserDTO.FromUser).ToList();
        }

        public List<UserDTO> GetUsersAvailable<T>() where T : User //Este método es para traer usuarios según el tipo (Client, Trainer, etc)
        {

            var users = _adminRepository.GetUsersAvailable<T>();


            if (users == null || !users.Any())
                throw new KeyNotFoundException("No hay usuarios disponibles de este tipo.");


            return users.Select(UserDTO.FromUser).ToList();
        }

     

        public bool DisableUser(string mail)
        {
            if (string.IsNullOrWhiteSpace(mail))
                throw new ArgumentException("El correo electrónico no puede ser nulo o vacío.", nameof(mail));

            var existingUser = _adminRepository.GetUserByEmail(mail)
                                            ?? throw new KeyNotFoundException("No se encontró el usuario.");

            if (!existingUser.IsAvailable)
            {
                throw new InvalidOperationException("El usuario ya ha sido eliminado.");
            }

            if (existingUser.UserType == UserType.Client)
            {
                var client = _clientRepository.FindByCondition(p => p.Email == mail); 
                foreach (var clientGymSession in client.ClientGymSessions)
                {
                    _clientGymSessionRepository.RemoveClientGymSession(clientGymSession);
                }
            }

            if (existingUser.UserType == UserType.Trainer)
            {
                var trainer = _trainerRepository.FindByCondition(p => p.Email == mail);
                foreach (var gymSession in trainer.GymSessions)
                {

                    foreach (var clientGymSession in gymSession.ClientGymSessions)
                    {
                        _clientGymSessionRepository.RemoveClientGymSession(clientGymSession);
                    }

                    _gymSessionService.DeleteGymSession(gymSession.Id);
                }
            }

                existingUser.IsAvailable = false;
            _userRepository.update(existingUser);

            return true;
        }

        public bool ActivateUser(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El correo electrónico no puede ser nulo o vacío.", nameof(email));

            var existingUser = _adminRepository.GetUserByEmail(email)
                                           ?? throw new KeyNotFoundException("Usuario no encontrado.");

            if (existingUser.IsAvailable)
            {
                throw new InvalidOperationException("El usuario ya está activo.");
            }

            existingUser.IsAvailable = true;
            _userRepository.update(existingUser);
            return true;
        }

        public bool HardDeleteUser(string mail)
        {
            if (string.IsNullOrWhiteSpace(mail))
                throw new ArgumentException("El correo electrónico no puede ser nulo o vacío.", nameof(mail));

            var existingUser = _adminRepository.GetUserByEmail(mail)
                                            ?? throw new KeyNotFoundException("No se encontró el usuario.");

            if (!existingUser.IsAvailable)
            {
                throw new InvalidOperationException("El usuario ya ha sido eliminado.");
            }

            if (existingUser.UserType == UserType.Client)
            {
                var client = _clientRepository.FindByCondition(p => p.Email == mail);
                foreach (var clientGymSession in client.ClientGymSessions)
                {
                    _clientGymSessionRepository.RemoveClientGymSession(clientGymSession);
                }
            }

            if (existingUser.UserType == UserType.Trainer)
            {
                var trainer = _trainerRepository.FindByCondition(p => p.Email == mail);
                foreach (var gymSession in trainer.GymSessions)
                {

                    foreach (var clientGymSession in gymSession.ClientGymSessions)
                    {
                        _clientGymSessionRepository.RemoveClientGymSession(clientGymSession);
                    }

                    _gymSessionService.DeleteGymSession(gymSession.Id);
                }
            }

            _userRepository.delete(existingUser);

            return true;
        }
    }
}
