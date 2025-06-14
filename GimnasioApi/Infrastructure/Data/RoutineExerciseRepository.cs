using Domain.Entities;
using Domain.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RoutineExerciseRepository:EfRepository<RoutineExercise> , IRoutineExerciseRepository
    {

        public RoutineExerciseRepository(ApplicationDbContext context):base(context) 
        { }

        public RoutineExercise? GetRoutineExercise(int routineId, int exerciseId)
        {
            return _applicationDbContext.RoutineExercises
                .FirstOrDefault(cgs => cgs.RoutineId == routineId && cgs.ExerciseId == exerciseId);
        }

        public void RemoveRoutineExercise(RoutineExercise routineExercise)
        {
            _applicationDbContext.RoutineExercises.Remove(routineExercise);
            _applicationDbContext.SaveChanges();
        }

    }
}
