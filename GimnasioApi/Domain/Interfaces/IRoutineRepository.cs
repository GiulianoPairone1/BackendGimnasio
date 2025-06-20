using Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRoutineRepository:IRepositoryBase<Routine>
    {
        // List<Routine> GetMyRoutines(int id);

        List<Routine> GetRoutineAvaiable();

        List<Routine> GetRoutinesByTrainer(int trainerId);

        Routine addRange(Routine routine, List<Exercise> exercises,List<RoutineExercise> routineExercise);
    }
}
