using Application.Interfaces;
using Application.Models;
using Application.Models.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GymSessionService : IGymSessionService
    {
        private readonly IGymSessionRepository _gymSessionRepository;

        public GymSessionService(IGymSessionRepository gymSessionRepository)
        {
            _gymSessionRepository = gymSessionRepository;
        }

        public ICollection<GymSessionDTO> GetAllGymSessions()
        {
            var gymSessions = _gymSessionRepository.GetAll()
                              ?? throw new KeyNotFoundException("No se encontraron sesiones");
            return gymSessions.Select(GymSessionDTO.FromGymSession).ToList();
        }

        public ICollection<GymSessionDTO> GetAllAvailable()
        {
            var gymSessions = _gymSessionRepository.GetGymSessionAvaiable()
                              ?? throw new KeyNotFoundException("No se encontraron sesiones disponibles");

            return gymSessions.Select(GymSessionDTO.FromGymSession).ToList();
        }

        public ICollection<GymSessionDTO> GetMySessions(int trainerId)
        {

            var gymSessions = _gymSessionRepository.GetMyGymSessions(trainerId)
                               ?? throw new KeyNotFoundException("No se encontraron sesiones para este entrenador");

            return gymSessions.Select(GymSessionDTO.FromGymSession).ToList();
        }

        public GymSessionDTO CreateGymSession(GymSessionDTO newSessionDto)
        {

            if (newSessionDto.SessionDate < DateTime.Now)
            {
                throw new ArgumentException("La fecha de la sesión no puede estar en el pasado.");
            }


            var existingSession = _gymSessionRepository.GetGymSessionAvaiable()
                                 .FirstOrDefault(session => session.TrainerId == newSessionDto.TrainerId &&
                                                            session.SessionDate.Date == newSessionDto.SessionDate.Date);

            if (existingSession != null)
            {
                throw new InvalidOperationException("Ya existe una sesión programada para este entrenador en la misma fecha.");
            }

            var gymSession = newSessionDto.ToGymSession();
            var created = _gymSessionRepository.add(gymSession);

            return GymSessionDTO.FromGymSession(created);
        }

        public GymSessionDTO UpdateGymSession(int id, GymSessionDTO updatedData)
        {
            var existingSession = _gymSessionRepository.GetById(id)
                                   ?? throw new KeyNotFoundException("No se encontró la sesión");


            if (updatedData.SessionDate < DateTime.Now)
            {
                throw new ArgumentException("La fecha de la sesión no puede estar en el pasado.");
            }


            var conflictingSession = _gymSessionRepository.GetGymSessionAvaiable()
                                           .FirstOrDefault(session => session.TrainerId == updatedData.TrainerId &&
                                                                      session.SessionDate.Date == updatedData.SessionDate.Date);

            if (conflictingSession != null && conflictingSession.Id != id)
            {
                throw new InvalidOperationException("Ya existe una sesión programada para este entrenador en la misma fecha.");
            }

            updatedData.UpdateGymSession(existingSession);
            _gymSessionRepository.update(existingSession);

            return GymSessionDTO.FromGymSession(existingSession);
        }

        public bool DeleteGymSession(int sessionId)
        {
            var existingSession = _gymSessionRepository.GetById(sessionId)
                                  ?? throw new KeyNotFoundException("No se encontró la sesión");


            if (!existingSession.IsAvailable)
            {
                throw new InvalidOperationException("La sesión ya ha sido cancelada.");
            }


            existingSession.IsCancelled = true;
            _gymSessionRepository.update(existingSession);

            return true;
        }

        public async Task<IEnumerable<GymSession>> GetSessionsByDateAsync(DateTime date)
        {
            if (date == default)
                throw new ArgumentException("La fecha es inválida.");

            if (date.Date < DateTime.Today)
                throw new ArgumentException("No se pueden consultar sesiones de fechas pasadas.");

            return await _gymSessionRepository.GetSessionsByDateAsync(date);
        }
    }
}

