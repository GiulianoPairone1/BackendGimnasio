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
    public class ClientRepository : EfRepository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<GymSession>> GetGymSessionAvaiableAsync()
        {
            return await _applicationDbContext.GymSessions
                .Where(gymSession => gymSession.IsAvailable == true)
                .ToListAsync();
        }

        public async Task<List<GymSession>> GetMyGymSessionsAsync(int userId)
        {
            return await _applicationDbContext.GymSessions
                .Where(session => session.Clients.Any(c => c.Id == userId))
                .Where(session => session.IsAvailable == true)
                .ToListAsync();
        }
    }
}
