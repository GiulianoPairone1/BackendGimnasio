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
        private readonly IRoutineService _routineService;
        private readonly IClientGymSessionRepository _clientGymSessionRepository;

        public GymSessionService(IGymSessionRepository gymSessionRepository, IRoutineService routineService, IClientGymSessionRepository clientGymSessionRepository)
        {
            _gymSessionRepository = gymSessionRepository;
            _routineService = routineService;
            _clientGymSessionRepository = clientGymSessionRepository
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

        public ICollection<GymSessionWithClientsDTO> GetMySessions(int trainerId)
        {

            var gymSessions = _gymSessionRepository.GetMyGymSessions(trainerId)
                               ?? throw new KeyNotFoundException("No se encontraron sesiones para este entrenador");
            

            return gymSessions.Select(GymSessionWithClientsDTO.FromGymSession).ToList();
        }

        public GymSessionDTO CreateGymSession(GymSessionDTO newSessionDto)
        {

            if (newSessionDto.SessionDate < DateTime.Now)
            {
                throw new ArgumentException("La fecha de la sesión no puede estar en el pasado.");
            }


            if (newSessionDto.SessionDate < DateTime.Now.AddHours(3))
            {
                throw new ArgumentException("La sesión debe crearse con al menos 3 horas de anticipado.");
            }

            var sessionsCount = _gymSessionRepository.GetGymSessionAvaiable()
                .Count(s => s.TrainerId == newSessionDto.TrainerId &&
                           s.SessionDate.Date == newSessionDto.SessionDate.Date);

            if (sessionsCount >= 3)
            {
                throw new InvalidOperationException("Ya tienes 3 clases programadas para ese día.");
            }

            // Verificar que no se solape con otra en el mismos horario
            DateTime newStart = newSessionDto.SessionDate;
            DateTime newEnd = newStart.AddHours(1);
            var overlapping = _gymSessionRepository.GetGymSessionAvaiable()
                .Any(s =>
                {
                    DateTime existingStart = s.SessionDate;
                    DateTime existingEnd = existingStart.AddHours(1);
                    return s.TrainerId == newSessionDto.TrainerId &&
                           existingStart < newEnd && newStart < existingEnd;
                });

            if (overlapping)
            {
                throw new InvalidOperationException("Ya tienes una sesión en ese horario.");
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

        public bool CancelGymSession(int sessionId)
        {
            var existingSession = _gymSessionRepository.GetById(sessionId)
                                  ?? throw new KeyNotFoundException("No se encontró la sesión");


            if (!existingSession.IsAvailable)
            {
                throw new InvalidOperationException("La sesión ya ha sido cancelada.");
            }

            foreach (var clientGymSession in existingSession.ClientGymSessions)
            {
                _clientGymSessionRepository.RemoveClientGymSession(clientGymSession);
            }

            existingSession.IsCancelled = true;
            _routineService.DeleteRoutine(existingSession.RoutineId);
            _gymSessionRepository.update(existingSession);

            return true;
        }

        public bool DeleteGymSession(int sessionId)
        {
            var existingSession = _gymSessionRepository.GetById(sessionId)
                                  ?? throw new KeyNotFoundException("No se encontró la sesión");

            foreach (var clientGymSession in existingSession.ClientGymSessions)
            {
                _clientGymSessionRepository.RemoveClientGymSession(clientGymSession);
            }

            _routineService.DeleteRoutine(existingSession.RoutineId);
            _gymSessionRepository.delete(existingSession);

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

