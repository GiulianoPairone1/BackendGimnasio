using Application.Models.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGymSessionService
    {
        ICollection<GymSessionDTO> GetAllGymSessions();
        ICollection<GymSessionDTO> GetAllAvailable();
        ICollection<GymSessionDTO> GetMySessions(int trainerId);
        GymSessionDTO CreateGymSession(GymSessionDTO newSessionDto);
        GymSessionDTO UpdateGymSession(int id, GymSessionDTO updatedData);
        bool DeleteGymSession(int sessionId);

        Task<IEnumerable<GymSession>> GetSessionsByDateAsync(DateTime date);

    }
}
