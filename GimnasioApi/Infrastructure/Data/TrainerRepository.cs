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



        public async Task<List<Exercise>> GetExercisesAvaiable()
        {
            return await _applicationDbContext.Exercises
                .Where(exercise => exercise.IsAvailable == true)
                .ToListAsync();
        }


    }
}
