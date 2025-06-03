using Domain.Entities;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Dtos
{
    public class RoutineDTO
    {
        [Required]
        public string Name { get; set; }
        // public int TrainerId { get; set; }
        public bool IsAvailable { get; set; }

        // Método para crear Routine
        public Routine ToRoutine()
        {
            return new Routine
            {
                Name = this.Name,
                // TrainerId= this.TrainerId,
                IsAvailable = this.IsAvailable,
            };
        }

        // Método para actualizar Routine
        public void UpdateRoutine(Routine routine)
        {
            routine.Name = this.Name;
            // routine.TrainerId = this.TrainerId;
            routine.IsAvailable = this.IsAvailable;
        }

        public static RoutineDTO FromRoutine(Routine routine)
        {
            return new RoutineDTO
            {
                Name = routine.Name,
                //TrainerId= routine.TrainerId,
                IsAvailable = routine.IsAvailable,
            };
        }
    }

}
