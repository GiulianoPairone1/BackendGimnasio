using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAdminRepository: IRepositoryBase<Admin>
    {
        User GetUserByEmail(string email);
        List<User> GetUsersAvailable();
        List<T> GetUsersAvailable<T>() where T : User;

        List<User> GetUsersNotAvailable();
    }
}
