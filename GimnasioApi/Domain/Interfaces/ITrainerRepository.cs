using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITrainerRepository : IRepositoryBase<Trainer>
    {
        Task<List<GymSession>> GetMyGymSessionsAsync(int userId);

        Task<List<Exercise>> GetExercisesAvaiable();

        Task<List<Routine>> GetMyRoutinesAsync();
    }
}
