﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GymSession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public SessionType SessionType { get; set; }

        [ForeignKey("Trainer")]
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }

        public DateTime SessionDate { get; set; }

        // Nueva relación
        [ForeignKey("Routine")]
        public int? RoutineId { get; set; } // Puede ser nullable si querés permitir sesiones sin rutina asignada
        public Routine Routine { get; set; }

        public bool IsAvailable { get; set; } = true;

        // Relación muchos a muchos
        public ICollection<ClientGymSession> ClientGymSessions { get; set; }

        public GymSession()
        {
            ClientGymSessions = new List<ClientGymSession>();
        }
    }
}
