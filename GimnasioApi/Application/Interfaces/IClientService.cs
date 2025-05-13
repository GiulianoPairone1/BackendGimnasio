using Application.Models.Dtos;


namespace Application.Interfaces
{
    public interface IClientService
    {
        List<ClientDTO> GetAll();
        ClientDTO GetByGetUserByEmail(string email);
        ClientDTO Create(ClientDTO clientDto);
        List<GymSessionDTO> GetMyGymSessions(int clientId);
        bool RegisterToGymSession(int clientId, int sessionId);
        bool UnregisterFromGymSession(int clientId, int sessionId);

        ClientDTO UpdateProfile(int clientId, ClientDTO clientDto);

    } 
}
