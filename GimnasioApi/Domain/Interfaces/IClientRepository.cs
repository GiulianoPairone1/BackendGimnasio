using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IClientRepository : IRepositoryBase<Client>
    {
        Task<List<GymSession>> GetGymSessionAvaiableAsync();
        //Task<List<GymSession>> GetMyGymSessionsAsync(int userId);
    }
}
