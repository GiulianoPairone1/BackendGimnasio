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

        public List<GymSession> GetMyGymSessions(int clientId) {

            var client = _applicationDbContext.Clients
                .Include(c => c.ClientGymSessions)
                .ThenInclude(cgs => cgs.GymSession)
                .ThenInclude(gs => gs.Trainer)
                .FirstOrDefault(c => c.Id == clientId);

            if (client == null)
                throw new KeyNotFoundException("Cliente no encontrado.");

            if (client.ClientGymSessions == null || !client.ClientGymSessions.Any())
                return new List<GymSession>();

            return client.ClientGymSessions
                .Where(cgs => cgs.GymSession.IsAvailable)
                .Select(cgs => cgs.GymSession)
                .Where(gs => gs.IsAvailable) 
                .ToList();
        }

        public ClientGymSession? GetClientGymSession(int clientId, int sessionId)
        {
            return _applicationDbContext.ClientGymSessions
                .Include(cgs => cgs.GymSession)
                .FirstOrDefault(cgs => cgs.ClientId == clientId && cgs.GymSessionId == sessionId);
        }


        public bool EmailExists(string email, int excludedClientId = 0)
        {
            return _applicationDbContext.Clients.Any(c => c.Email == email && c.Id != excludedClientId);
        }

    }
}
