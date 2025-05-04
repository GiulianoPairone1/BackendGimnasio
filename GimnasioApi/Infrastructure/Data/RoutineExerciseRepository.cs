using Domain.Entities;
using Domain.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RoutineExerciseRepository:RepositoryBase<RoutineExercise> , IRoutineExerciseRepository
    {
        private readonly ApplicationDbContext _context;

        public RoutineExerciseRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
    }
}
