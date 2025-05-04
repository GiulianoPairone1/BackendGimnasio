using Application.Interfaces;
using Application.Models.Dtos;

using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SuperAdminService : ISuperAdminService
    {
        private readonly ISuperAdminRepository _superAdminRepository;

        public SuperAdminService(ISuperAdminRepository superAdminRepository)
        {
            _superAdminRepository = superAdminRepository;
        }


        public List<SuperAdminDTO> GetAll()
        {
            return _superAdminRepository.GetAll()
                .Select(superAdmin => new SuperAdminDTO
                {
                    Name = superAdmin.Name,
                    Surname = superAdmin.Surname,
                    Email = superAdmin.Email,
                })
                .ToList();
        }

        public SuperAdminDTO Create(SuperAdminDTO superadminDto)
        {
            var superadmin = superadminDto.ToSuperAdmin();
            var addsuperadmin = _superAdminRepository.add(superadmin);
            return SuperAdminDTO.FromSuperAdmin(addsuperadmin);
        }
    }
}
