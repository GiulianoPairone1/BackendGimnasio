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
