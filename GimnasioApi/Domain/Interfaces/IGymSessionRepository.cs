using Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGymSessionRepository: IRepositoryBase<GymSession>
    {
        List<GymSession> GetGymSessionAvaiable();
        List<GymSession> GetMyGymSessions(int trainerId);

        GymSession? GetGymSessionWithClients(int sessionId);

        Task<IEnumerable<GymSession>> GetSessionsByDateAsync(DateTime date);
    }
}
