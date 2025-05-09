﻿using Application.Models.Dtos;


namespace Application.Interfaces
{
    public interface IClientService
    {
        List<ClientDTO> GetAll();
        ClientDTO GetByGetUserByEmail(string email);
        ClientDTO Create(ClientDTO clientDto);

    }
}
