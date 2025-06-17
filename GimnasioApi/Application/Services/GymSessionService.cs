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
using static System.Collections.Specialized.BitVector32;

namespace Application.Services
{
    public class GymSessionService : IGymSessionService
    {
        private readonly IGymSessionRepository _gymSessionRepository;
        private readonly IRoutineService _routineService;
        private readonly IClientGymSessionRepository _clientGymSessionRepository;
        private readonly ISendEmailService _sendEmailService;
        private readonly IClientRepository _clientRepository;

        public GymSessionService(IGymSessionRepository gymSessionRepository, IRoutineService routineService, IClientGymSessionRepository clientGymSessionRepository, ISendEmailService sendEmailService, IClientRepository clientRepository)
        {
            _gymSessionRepository = gymSessionRepository;
            _routineService = routineService;
            _clientGymSessionRepository = clientGymSessionRepository;
            _sendEmailService = sendEmailService;
            _clientRepository = clientRepository;
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
            var existingSession = _gymSessionRepository.GetGymSessionWithClients(sessionId)
                                  ?? throw new KeyNotFoundException("No se encontró la sesión");



            if (!existingSession.IsAvailable)
            {
                throw new InvalidOperationException("La sesión ya ha sido cancelada.");
            }

            string subject = "Cancelación de tu clase programada del – " + existingSession.SessionDate.ToString("dd/MM/yyyy");

            string body = $"Hola,\n\n" +
                          $"Lamentamos informarte que la clase programada para el día {existingSession.SessionDate:dd/MM/yyyy} ha sido cancelada debido a circunstancias imprevistas.\n\n" +
                          $"Si ya habías reservado tu lugar, esta cancelación se aplica a tu reserva y no tendrás ningún cargo por esta sesión.\n\n" +
                          $"Por favor, mantente atento/a a nuestras próximas clases y no dudes en contactarnos si tienes alguna consulta o deseas reprogramar.\n\n" +
                          $"Gracias por tu comprensión y te esperamos pronto.\n\n" +
                          $"Saludos cordiales,\n" +
                          $"El equipo de Utnenegger Gym";

            foreach (var clientGymSession in existingSession.ClientGymSessions)
            {
                var client = clientGymSession.Client;

                if (client != null && !string.IsNullOrEmpty(client.Email))
                {
                    _sendEmailService.SendEmail(
                        client.Email,
                        subject,
                        body);
                }
            }

            foreach (var clientGymSession in existingSession.ClientGymSessions.ToList())
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

