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
    public class AdminDTO
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
        public int Phone { get; set; }

        //Metodo para crear un Admin
        public Admin ToAdmin()
        {
            return new Admin
            {
                Name = this.Name,
                Surname = this.Surname,
                Email = this.Email,
                Password = this.Password,
                Phone = this.Phone,
                UserType = UserType.Admin,
                IsAvailable = true
            };
        }

        //Metodo para actualizar
        public void UpdateAdmin(Admin admin)
        {
            admin.Name = this.Name;
            admin.Surname = this.Surname;
            admin.Email = this.Email;
            admin.Password = this.Password;
            admin.Phone = this.Phone;
        }
        public static AdminDTO FromAdmin(Admin admin)
        {
            return new AdminDTO
            {
                Name = admin.Name,
                Surname = admin.Surname,
                Email = admin.Email,
                Password = admin.Password,
                Phone = admin.Phone,
            };

        }
    }
}
