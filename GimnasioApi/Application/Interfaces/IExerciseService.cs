using Application.Models.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IExerciseService
    {
        List<ExerciseDTO> GetAll();
        List<ExerciseDTO> GetExercisesAvailable();

        ExerciseDTO CreateExercise(ExerciseDTO exerciseDTO);

        ExerciseDTO UpdateExercise(int id, ExerciseDTO updatedData);
        bool DeleteExercise(int id);
    }
}
