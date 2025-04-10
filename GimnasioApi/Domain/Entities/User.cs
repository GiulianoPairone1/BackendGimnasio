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
        public string Name { get; set; } 
        public string Surname { get; set; } 
        public UserType UserType { get; set; } 
        public string Email { get; set; } 
        public string Password { get; set; } 
        public int Phone { get; set; } 
        public bool IsAvailable { get; set; } = true;

    }
}
