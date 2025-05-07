using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ExerciseRepository:EfRepository<Exercise>, IExerciseRepository
    {

        public ExerciseRepository(ApplicationDbContext context):base(context) { }

        public List<Exercise> GetExercisesAvaiable()
        {
            return _applicationDbContext.Exercises
                .Where(exercise => exercise.IsAvailable)
                .ToList();
        }

    }
}
