using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SuperAdminRepository : EfRepository<Trainer>, ISuperAdminRepository
    {
        public SuperAdminRepository(ApplicationDbContext context) : base(context) { }
    }
}
