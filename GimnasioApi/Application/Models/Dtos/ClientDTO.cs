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
    public class ClientDTO
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
        public string Phone {  get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El peso debe ser un valor positivo.")]
        public double Weight { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La altura debe ser un valor positivo.")]
        public double Height { get; set; }


        //Metodo para crear un Cliente
        public Client ToClient()
        {
            return new Client
            {
                Name = this.Name,
                Surname = this.Surname,
                Email = this.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(this.Password),
                Phone = this.Phone,
                Weight = this.Weight,
                Height = this.Height,
                IsAvailable = true,
                UserType = UserType.Client
            };
        }

        //Metodo para actualizar
        public void UpdateClient(Client client)
        {
            client.Name = this.Name;
            client.Surname = this.Surname;
            client.Email = this.Email;
            client.Password = BCrypt.Net.BCrypt.HashPassword(this.Password);
            client.Phone = this.Phone;
            client.Weight = this.Weight;
            client.Height = this.Height;
        }
        public static ClientDTO FromClient( Client client )
        {
            return new ClientDTO
            {
                Name = client.Name,
                Surname = client.Surname,
                Email = client.Email,
                Password = "",
                Phone = client.Phone,
                Weight = client.Weight,
                Height = client.Height,
            };
        }

        //Metodo para actualizar UserType


    }
}
