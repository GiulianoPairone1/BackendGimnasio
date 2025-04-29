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
    public class TrainerRepository : EfRepository<Trainer>, ITrainerRepository
    {
        public TrainerRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<GymSession>> GetMyGymSessionsAsync(int userId)
        {
            return await _applicationDbContext.GymSessions
                .Where(item => item.Trainer.Id == userId)
                .Where(session => session.IsAvailable == true)
                .ToListAsync();
        }

        public async Task<List<Exercise>> GetExercisesAvaiable()
        {
            return await _applicationDbContext.Exercises
                .Where(exercise => exercise.IsAvailable == true)
                .ToListAsync();
        }

        public Task<List<Routine>> GetMyRoutinesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
