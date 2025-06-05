using Domain.Entities;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Dtos
{
    public class GymSessionDTO
    {
        [Required]
        public DateTime SessionDate { get; set; }
        [Required]
        public int TrainerId { get; set; }

        public int? Id { get; set; }
        public int? RoutineId { get; set; }
        public string? RoutineName { get; set; }
        public bool IsAvailable { get; set; }

        // Método para crear GymSession
        public GymSession ToGymSession()
        {
            return new GymSession
            {
                SessionDate = this.SessionDate,
                TrainerId = this.TrainerId,
                RoutineId = this.RoutineId,
                IsAvailable = this.IsAvailable,
            };
        }

        // Método para actualizar GymSession
        public void UpdateGymSession(GymSession gymSession)
        {
            gymSession.SessionDate = this.SessionDate;
            gymSession.TrainerId = this.TrainerId;
            gymSession.RoutineId = this.RoutineId;
            gymSession.IsAvailable = this.IsAvailable;
        }

        public static GymSessionDTO FromGymSession(GymSession gymSession)
        {
            return new GymSessionDTO
            {
                Id = gymSession.Id,
                SessionDate = gymSession.SessionDate,
                TrainerId = gymSession.TrainerId,
                RoutineName = gymSession.Routine.Name,
                RoutineId = gymSession.RoutineId,
                IsAvailable = gymSession.IsAvailable,
            };
            
        }
    }

}
