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
    public class GymSessionRepository : EfRepository<GymSession>, IGymSessionRepository
    {
        public GymSessionRepository(ApplicationDbContext context) : base(context) { }

        public List<GymSession> GetGymSessionAvaiable()
        {
            return _applicationDbContext.GymSessions
                .Where(gymSession => gymSession.IsAvailable == true)
                .ToList();
        }

        public List<GymSession> GetMyGymSessions(int trainerId)
        {
            return _applicationDbContext.GymSessions
                .Where(user => user.TrainerId == trainerId && user.IsAvailable)
                .ToList();
        }

        public GymSession? GetGymSessionWithClients(int sessionId)
        {
            return _applicationDbContext.GymSessions
                .Include(gs => gs.ClientGymSessions)
                .ThenInclude(cgs => cgs.Client)
                .FirstOrDefault(gs => gs.Id == sessionId);
        }


        public async Task<IEnumerable<GymSession>> GetSessionsByDateAsync(DateTime date)
        {
            return await _applicationDbContext.GymSessions
                .Include(g => g.Trainer)
                .Include(g => g.Routine)
                .Include(g => g.ClientGymSessions)
                .ThenInclude(cgs => cgs.Client)
                .Where(g =>
                g.SessionDate >= date.Date &&
                g.SessionDate < date.Date.AddDays(1) &&
                g.IsAvailable == true)
                .ToListAsync();
        }

    }
}