using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AdminRepository : EfRepository<Admin>, IAdminRepository
    {
        public AdminRepository(ApplicationDbContext context) : base(context) { }

        public User? GetUserByEmail(string? email)
        {
            return _applicationDbContext.Users.SingleOrDefault(p => p.Email == email);
        }

        public async Task<List<User>> GetUsersAvaiableAsync()
        {
            return await _applicationDbContext.Users
                .Where(u => u.IsAvailable == true)
                .ToListAsync();
        }
    }
}
