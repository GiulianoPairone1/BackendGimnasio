using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Entities
{
    public class SuperAdmin : User
    {
        public SuperAdmin()
        {
            UserType = Enums.UserType.SuperAdmin;
        }
    }
}
