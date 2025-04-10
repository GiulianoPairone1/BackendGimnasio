using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Trainer : User
    {
        public Trainer()
        {
            UserType = UserType.Trainer;
        }

        public Speciality? TrainerSpeciality { get; set; } 

        public List<GymSession> GymSessions { get; set; }

        public ICollection<Routine> Routines { get; set; }

    }
}
