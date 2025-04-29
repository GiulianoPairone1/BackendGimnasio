using Domain.Entities;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Dtos
{
    public class ExerciseDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Sets { get; set; }
        [Required]
        public int Reps { get; set; }
        [Required]
        public int RestTime { get; set; }
        public bool IsAvailable { get; set; }

        // Método para crear Exercise
        public Exercise ToExercise()
        {
            return new Exercise
            {
                Name = this.Name,
                Sets = this.Sets,
                Reps = this.Reps,
                RestTime = this.RestTime,
                IsAvailable = this.IsAvailable,
            };
        }

        // Método para actualizar Exercise
        public void UpdateExercise(Exercise exercise)
        {
            exercise.Name = this.Name;
            exercise.Sets = this.Sets;
            exercise.Reps = this.Reps;
            exercise.RestTime = this.RestTime;
            exercise.IsAvailable = this.IsAvailable;
        }

        public static ExerciseDTO FromExercise(Exercise exercise)
        {
            return new ExerciseDTO
            {
                Name = exercise.Name,
                Sets = exercise.Sets,
                Reps = exercise.Reps,
                RestTime = exercise.RestTime,
                IsAvailable = exercise.IsAvailable,
            };
        }
    }

}
