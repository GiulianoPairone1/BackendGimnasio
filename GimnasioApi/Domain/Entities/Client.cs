using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Client : User 
    {
        public Client()
        {
            UserType = UserType.Client;
        }

        public double Weight { get; set; }  
        public double Height { get; set; }  
        public ICollection<GymSession> GymSessions { get; set; }

        
    }
}
