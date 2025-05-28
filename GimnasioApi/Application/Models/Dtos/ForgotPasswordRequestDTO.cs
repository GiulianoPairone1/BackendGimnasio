using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Dtos
{
    public class ForgotPasswordRequestDTO
    {
        [Required]
        public string Email { get; set; }
    }
}
