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
    public class ClientGymSessionRepository : EfRepository<ClientGymSession> ,IClientGymSessionRepository
    { 

        public ClientGymSessionRepository(ApplicationDbContext context):base(context) 
        {  }

        public ClientGymSession? GetClientGymSession(int clientId, int sessionId)
        {
            return _applicationDbContext.ClientGymSessions
                .Include(cgs => cgs.GymSession)
                .FirstOrDefault(cgs => cgs.ClientId == clientId && cgs.GymSessionId == sessionId);
        }

        public void AddClientGymSession(ClientGymSession clientGymSession)
        {
            _applicationDbContext.ClientGymSessions.Add(clientGymSession);
            _applicationDbContext.SaveChanges();
        }

        public void RemoveClientGymSession(ClientGymSession clientGymSession)
        {
            _applicationDbContext.ClientGymSessions.Remove(clientGymSession);
            _applicationDbContext.SaveChanges();
        }

        public bool ClientHasClassThatDay(int clientId, DateTime date)
        {
            return _applicationDbContext.ClientGymSessions
                .Include(r => r.GymSession)
                .Any(r => r.ClientId == clientId && r.GymSession.SessionDate.Date == date.Date);
        }

    }
}
