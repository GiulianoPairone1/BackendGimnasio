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
    public class TrainerDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        public Speciality? TrainerSpeciality { get; set; }

        //Metodo para crear Trainer
        public Trainer ToTrainer()
        {
            return new Trainer
            {
                Name = this.Name,
                Surname = this.Surname,
                Email = this.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(this.Password),
                Phone = this.Phone,
                TrainerSpeciality=this.TrainerSpeciality,
                UserType = UserType.Trainer,
                IsAvailable = true,
            };
        }

        //Metodo para actualizar
        public void UpdateTrainer(Trainer trainer)
        {
            trainer.Name = this.Name;
            trainer.Surname = this.Surname;
            trainer.Email = this.Email;
            if (this.Password is not null || this.Password != "")
                trainer.Password = BCrypt.Net.BCrypt.HashPassword(this.Password);
            trainer.Phone = this.Phone;
            trainer.TrainerSpeciality = this.TrainerSpeciality;
        }

        public static TrainerDTO FromTrainer(Trainer trainer)
        {
            return new TrainerDTO
            {
                Name = trainer.Name,
                Surname = trainer.Surname,
                Email = trainer.Email,
                Password = "",
                Phone = trainer.Phone,
                TrainerSpeciality = trainer.TrainerSpeciality,
            };
        }

    }
}
