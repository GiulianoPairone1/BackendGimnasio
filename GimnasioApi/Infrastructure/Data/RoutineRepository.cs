using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RoutineRepository: EfRepository<Routine>, IRoutineRepository
    {
        public RoutineRepository(ApplicationDbContext context) : base(context) { }

        // public List<Routine> GetMyRoutines(int id)
        // {
        //    return _applicationDbContext.Routines
        //        .Where(routine=>routine.TrainerId == id && routine.IsAvailable)
        //        .ToList();
        //}

        public List<Routine> GetRoutineAvaiable()
        {
            return _applicationDbContext.Routines
                .Where(routine => routine.IsAvailable)
                .ToList();
        }

        public List<Routine> GetRoutinesByTrainer(int trainerId) {
            return _applicationDbContext.GymSessions
                .Where(s => s.TrainerId == trainerId)
                .Include(s => s.Routine)
                .ThenInclude(r => r.RoutineExercises)
                .ThenInclude(re => re.Exercise)
                .Select(s => s.Routine)
                .Distinct()
                .ToList();
        }
    }
}
