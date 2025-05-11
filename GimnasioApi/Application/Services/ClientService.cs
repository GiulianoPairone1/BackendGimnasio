using Application.Interfaces;
using Application.Models.Dtos;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public List<ClientDTO> GetAll()
        {
            return _clientRepository.GetAll()
                .Select(client => new ClientDTO
                {
                    Name = client.Name,
                    Surname = client.Surname,
                    Email = client.Email,
                })
                .ToList();
        }

        public ClientDTO GetByGetUserByEmail(string email)
        {
            var client = _clientRepository.FindByCondition(p=>p.Email == email);
            
            if (client == null)
            {
                return null;
            }
            return new ClientDTO
            {
                Name = client.Name,
                Surname = client.Surname,
                Email = client.Email,
            };
        }

        public ClientDTO Create(ClientDTO clientDto)
        {
            var client = clientDto.ToClient();
            var addClient=_clientRepository.add(client);
            return ClientDTO.FromClient(addClient);
        }

        public List<GymSessionDTO> GetMyGymSessions(int clientId)
        {
            var gymSessions = _clientRepository.GetMyGymSessions(clientId);

            return gymSessions.Select(gs => new GymSessionDTO
            {
                TrainerId = gs.Id,
                RoutineId = gs.RoutineId,
                SessionDate = gs.SessionDate,
                IsAvailable = gs.IsAvailable
            }).ToList();
        }

        public bool RegisterToGymSession(int clientId, int sessionId)
        {
            var client = _clientRepository.GetById(clientId)
                ?? throw new KeyNotFoundException("Cliente no encontrado.");

            var session = _clientRepository.GetById(sessionId)
                ?? throw new KeyNotFoundException("Sesión no encontrada.");

            if (!session.IsAvailable)
                throw new InvalidOperationException("La sesión no está disponible.");

            if (session.ClientGymSessions.Any(cgs => cgs.ClientId == clientId))
                throw new InvalidOperationException("El cliente ya está anotado a esta sesión.");


            const int maxCapacity = 15; 
            if (session.ClientGymSessions.Count >= maxCapacity)
                throw new InvalidOperationException($"La sesión ya alcanzó su capacidad máxima de {maxCapacity}.");

            var clientGymSession = new ClientGymSession
            {
                ClientId = clientId,
                GymSessionId = sessionId
            };

            client.ClientGymSessions.Add(clientGymSession);
            session.ClientGymSessions.Add(clientGymSession);

            _clientRepository.AddClientGymSession(clientGymSession);
            return true;
        }

        public bool UnregisterFromGymSession(int clientId, int sessionId)
        {

            var clientGymSession = _clientRepository.GetClientGymSession(clientId, sessionId)
                ?? throw new InvalidOperationException("El cliente no está registrado en esta sesión.");


            if (!clientGymSession.GymSession.IsAvailable)
                throw new InvalidOperationException("La sesión ya no está disponible.");


            _clientRepository.RemoveClientGymSession(clientGymSession);

            return true;
        }

        public ClientDTO UpdateProfile(int clientId, ClientDTO clientDto)
        {

            var client = _clientRepository.GetById(clientId)
                         ?? throw new KeyNotFoundException("Cliente no encontrado.");


            if (!client.IsAvailable)
                throw new InvalidOperationException("El cliente ha sido dado de baja y no puede ser actualizado.");
            if (_clientRepository.EmailExists(clientDto.Email, clientId))
                throw new InvalidOperationException("El correo ingresado ya está en uso.");
            if (clientDto.Phone <= 0)
                throw new InvalidOperationException("El número de teléfono ingresado no es válido.");
            if (clientDto.Weight < 0 || clientDto.Height < 0)
                throw new InvalidOperationException("El peso y la altura deben ser valores positivos.");

         
            clientDto.UpdateClient(client);
            _clientRepository.update(client);
            return ClientDTO.FromClient(client);
        }

    }
}
