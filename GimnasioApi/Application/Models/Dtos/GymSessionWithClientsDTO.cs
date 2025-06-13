using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Dtos
{
    public class GymSessionWithClientsDTO
    {
        [Required]
        public DateTime SessionDate { get; set; }
        [Required]
        public int TrainerId { get; set; }

        public int? RoutineId { get; set; }
        public string? RoutineName { get; set; }
        public int Id { get; set; }
        public SessionType SessionType { get; set; }
        public bool IsCancelled { get; set; }
        public List<ClientInfoDTO> Clients { get; set; }
        public int ReservedPlaces { get; set; }

        public static GymSessionWithClientsDTO FromGymSession(GymSession gymSession)
        {
            return new GymSessionWithClientsDTO
            {
                Id = gymSession.Id,
                SessionDate = gymSession.SessionDate,
                TrainerId = gymSession.TrainerId,
                RoutineName = gymSession.Routine.Name,
                RoutineId = gymSession.RoutineId,
                SessionType = gymSession.SessionType,
                Clients = gymSession.ClientGymSessions
                                           .Select(cgs => new ClientInfoDTO
                                           {
                                               Name = cgs.Client?.Name,
                                               Email = cgs.Client?.Email
                                           })
                                           .ToList(),
                ReservedPlaces = gymSession.ClientGymSessions.Count()
            };
        }
    }
}
