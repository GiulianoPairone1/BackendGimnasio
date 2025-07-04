﻿using Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRoutineExerciseRepository:IRepositoryBase<RoutineExercise>
    {
        void RemoveRoutineExercise(RoutineExercise routineExercise);
        RoutineExercise? GetRoutineExercise(int routineId, int exerciseId);
    }
}
