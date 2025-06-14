using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Dtos
{
    public class RoutineWithExercisesDTO
    {
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public List<ExerciseDTO> Exercises { get; set; }

        public static RoutineWithExercisesDTO FromRoutine(Routine routine)
        {
            return new RoutineWithExercisesDTO
            {
                Name = routine.Name,
                IsAvailable = routine.IsAvailable,
                Exercises = routine.RoutineExercises?.Select(re => new ExerciseDTO
                {
                    Name = re.Exercise?.Name,
                    Sets = re.Exercise?.Sets ?? 0,
                    Reps = re.Exercise?.Reps ?? 0
                }).ToList()
            };
        }
    }
}
