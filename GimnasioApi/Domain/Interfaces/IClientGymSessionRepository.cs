using Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IClientGymSessionRepository : IRepositoryBase<ClientGymSession>
    {
        ClientGymSession? GetClientGymSession(int clientId, int sessionId);
        void AddClientGymSession(ClientGymSession clientGymSession);
        void RemoveClientGymSession(ClientGymSession clientGymSession);

    }
}
