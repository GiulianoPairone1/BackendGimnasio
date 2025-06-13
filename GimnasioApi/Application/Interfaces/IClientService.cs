using Application.Models.Dtos;


namespace Application.Interfaces
{
    public interface IClientService
    {
        List<ClientDTO> GetAll();
        ClientDTO GetByGetUserByEmail(string email);
        ClientDTO Create(ClientDTO clientDto);
        List<GymSessionDTO> GetMyGymSessions(int clientId);

        ClientDTO UpdateProfile(int clientId, ClientDTO clientDto);
        ClientDTO GetClientById(int clientId);

    } 
}
