using Application.Models.Dtos;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        List<AdminDTO> GetAll();
        AdminDTO Create(AdminDTO adminDTO);

        UserDTO GetUserByEmail(string email);

        List<UserDTO> GetUsersAvailable();
        List<UserDTO> GetUsersAvailable<T>() where T : User;
        bool DisableUser(string mail);

        bool HardDeleteUser(string mail);
    }
}
