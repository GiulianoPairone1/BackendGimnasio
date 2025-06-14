using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ClientGymSessionService : IClientGymSessionService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IGymSessionRepository _gymSessionRepository;
        private readonly IClientGymSessionRepository _clientGymSessionRepository;

        public ClientGymSessionService(IClientRepository clientRepository, IClientGymSessionRepository clientGymSessionRepository, IGymSessionRepository gymSessionRepository)
        {
            _clientRepository = clientRepository;
            _clientGymSessionRepository = clientGymSessionRepository;
            _gymSessionRepository = gymSessionRepository;
        }

        public bool RegisterToGymSession(int clientId, int sessionId)
        {
            var client = _clientRepository.GetById(clientId)
                ?? throw new KeyNotFoundException("Cliente no encontrado.");

            var session = _gymSessionRepository.GetGymSessionWithClients(sessionId)
                ?? throw new KeyNotFoundException("Sesión no encontrada.");

            if (!session.IsAvailable)
                throw new InvalidOperationException("La sesión no está disponible.");

       

            if (session.ClientGymSessions.Any(cgs => cgs.ClientId == clientId))
                throw new InvalidOperationException("Usted ya está anotado a esta sesión.");


            const int maxCapacity = 20;
            if (session.ClientGymSessions.Count >= maxCapacity)
                throw new InvalidOperationException($"La sesión ya alcanzó su capacidad máxima de {maxCapacity} clientes.");

            int totalReservations = _clientGymSessionRepository
                             .GetAll()
                             .Count(r => r.ClientId == clientId);
            if (totalReservations >= 5)
            {
                throw new Exception("No puede inscribir más de 5 clases.");
            }

            if (_clientGymSessionRepository.ClientHasClassThatDay(clientId, session.SessionDate))
            {
                throw new Exception("No se puede inscribir a más de 1 clase por día.");
            }

            var clientGymSession = new ClientGymSession
            {
                ClientId = clientId,
                GymSessionId = sessionId
            };

            client.ClientGymSessions.Add(clientGymSession);
            session.ClientGymSessions.Add(clientGymSession);

            _clientGymSessionRepository.AddClientGymSession(clientGymSession);
            return true;
        }

        public bool UnregisterFromGymSession(int clientId, int sessionId)
        {

            var clientGymSession = _clientRepository.GetClientGymSession(clientId, sessionId)
                ?? throw new InvalidOperationException("El cliente no está registrado en esta sesión.");


            if (!clientGymSession.GymSession.IsAvailable)
                throw new InvalidOperationException("La sesión ya no está disponible.");


            _clientGymSessionRepository.RemoveClientGymSession(clientGymSession);

            return true;
        }

    }
}
