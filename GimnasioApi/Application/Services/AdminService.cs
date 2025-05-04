using Application.Interfaces;
using Domain.Interfaces;
using Application.Models.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository) 
        {
            _adminRepository = adminRepository;
        }

        public List<AdminDTO> GetAll()
        {
            return _adminRepository.GetAll()
                .Select(admin => new AdminDTO
                {
                    Name = admin.Name,
                    Surname = admin.Surname,
                    Email = admin.Email,
                })
                .ToList();
        }

        public AdminDTO Create(AdminDTO clientDto)
        {
            var admin = clientDto.ToAdmin();
            var addAdmin = _adminRepository.add(admin);
            return AdminDTO.FromAdmin(addAdmin);
        }
    }
}
