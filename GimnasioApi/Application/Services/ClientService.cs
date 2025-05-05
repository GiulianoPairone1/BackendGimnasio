using Application.Interfaces;
using Application.Models.Dtos;
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

    }
}
