using Application.Models.Dtos;

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
        AdminDTO Create(AdminDTO clientDto);
    }
}
