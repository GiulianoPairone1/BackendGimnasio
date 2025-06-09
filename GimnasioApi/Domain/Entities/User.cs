using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public UserType UserType { get; set; }
        [Required]
        [MaxLength(150)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; } 
        public bool IsAvailable { get; set; } = true;

        //Recuperar contraseña
        public string? PasswordResetToken { get; set; }
        public DateTime? TokenExpiration { get; set; }
    }
}
