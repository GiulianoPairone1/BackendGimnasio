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
        List<GymSession> GetMyGymSessions(int clientId);
        void AddClientGymSession(ClientGymSession clientGymSession);
        ClientGymSession? GetClientGymSession(int clientId, int sessionId);
        void RemoveClientGymSession(ClientGymSession clientGymSession);
        public bool EmailExists(string email, int excludedClientId = 0);
    }
}
