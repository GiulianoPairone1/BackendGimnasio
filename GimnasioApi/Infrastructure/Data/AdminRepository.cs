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

        public List<User> GetUsersAvailable()
        {
            return _applicationDbContext.Users
                           .Where(u => u.IsAvailable)
                           .ToList();
        }

        // Obtener usuarios disponibles de un tipo específico (Trainers, Clients, etc)
        public List<T> GetUsersAvailable<T>() where T : User
        {
            return _applicationDbContext.Users
                           .OfType<T>()
                           .Where(u => u.IsAvailable)
                           .ToList();
        }
    }
}
