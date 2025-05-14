using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Dtos
{
    public class UserDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public UserType UserType { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int Phone { get; set; }

        // Propiedades específicas para los diferentes tipos de usuarios
        public Speciality? TrainerSpeciality { get; set; } // Solo para Trainer
        public double? Weight { get; set; }                // Solo para Client
        public double? Height { get; set; }                // Solo para Client

        //Metodo para crear User
        public User ToUser()
        {
            return UserType switch
            {
                UserType.Trainer => new Trainer
                {
                    Name = this.Name,
                    Surname = this.Surname,
                    Email = this.Email,
                    Password = this.Password,
                    Phone = this.Phone,
                    UserType = this.UserType,
                    TrainerSpeciality = this.TrainerSpeciality,
                    GymSessions = new List<GymSession>(),
                    Routines = new List<Routine>(),
                    IsAvailable = true
                },
                UserType.Client => new Client
                {
                    Name = this.Name,
                    Surname = this.Surname,
                    Email = this.Email,
                    Password = this.Password,
                    Phone = this.Phone,
                    UserType = this.UserType,
                    Weight = this.Weight ?? 0, // Valor por defecto si es null
                    Height = this.Height ?? 0, // Valor por defecto si es null
                    ClientGymSessions = new List<ClientGymSession>(),
                    IsAvailable = true
                },
                UserType.Admin => new Admin
                {
                    Name = this.Name,
                    Surname = this.Surname,
                    Email = this.Email,
                    Password = this.Password,
                    Phone = this.Phone,
                    UserType = this.UserType,
                    IsAvailable = true
                },
                UserType.SuperAdmin => new SuperAdmin
                {
                    Name = this.Name,
                    Surname = this.Surname,
                    Email = this.Email,
                    Password = this.Password,
                    Phone = this.Phone,
                    UserType = this.UserType,
                    IsAvailable = true
                },
                _ => throw new InvalidOperationException("Tipo de usuario no válido.")
            };
        }

        //Metodo para actualizar
        public void UpdateUser(User user)
        {
            user.Name = this.Name;
            user.Surname = this.Surname;
            user.Email = this.Email;
            user.Password = this.Password;
            user.Phone = this.Phone;
            user.UserType = this.UserType;

            // Verifica y actualiza las propiedades específicas
            if (user is Trainer trainer)
            {
                trainer.TrainerSpeciality = this.TrainerSpeciality;
            }
            else if (user is Client client)
            {
                client.Weight = this.Weight ?? client.Weight;
                client.Height = this.Height ?? client.Height;
            }
        }

        // Método para convertir un User a UserDTO 
        public static UserDTO FromUser(User user)
        {
            var dto = new UserDTO
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Password = user.Password,
                Phone = user.Phone,
                UserType = user.UserType
            };

            // Asigna propiedades específicas según el tipo de usuario
            if (user is Trainer trainer)
            {
                dto.TrainerSpeciality = trainer.TrainerSpeciality;
            }
            else if (user is Client client)
            {
                dto.Weight = client.Weight;
                dto.Height = client.Height;
            }

            return dto;
        }
    }
}

