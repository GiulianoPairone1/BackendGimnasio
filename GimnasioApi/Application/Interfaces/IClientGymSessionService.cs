using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IClientGymSessionService
    {
        bool RegisterToGymSession(int clientId, int sessionId);

        bool UnregisterFromGymSession(int clientId, int sessionId);
    }
}
