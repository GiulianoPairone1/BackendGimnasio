﻿using Domain.Entities;
using Domain.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Dtos
{
    public class SuperAdminDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }

        //Metodo para crear
        public SuperAdmin ToSuperAdmin()
        {
            return new SuperAdmin
            {
                Name = this.Name,
                Surname = this.Surname,
                Email = this.Email,
                Password = this.Password,
                Phone = this.Phone,
                UserType = UserType.SuperAdmin,
                IsAvailable = true
            };
        }

        //Metodo para actualizar
        public void UpdateSuperAdmin(SuperAdmin superadmin)
        {
            superadmin.Name = this.Name;
            superadmin.Surname = this.Surname;
            superadmin.Email = this.Email;
            superadmin.Password = this.Password;
            superadmin.Phone = this.Phone;
        }
        public static SuperAdminDTO FromSuperAdmin(SuperAdmin superadmin)
        {
            return new SuperAdminDTO
            {
                Name = superadmin.Name,
                Surname = superadmin.Surname,
                Email = superadmin.Email,
                Password = superadmin.Password,
                Phone = superadmin.Phone,
            };

        }
    }
}
