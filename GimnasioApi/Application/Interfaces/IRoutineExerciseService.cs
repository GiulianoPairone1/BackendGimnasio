using Application.Models.Dtos;

using Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRoutineExerciseService
    {
        List<RoutineExercise> GetAll();
        void Add(RoutineExerciseDTO dto);
        void Update(int routineExerciseId, RoutineExerciseDTO dto);
        bool DeleteRoutineExercise(int routineId, int exerciseId);
    }
}
