using Application.Interfaces;
using Application.Models.Dtos;
using Application.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GimnasioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var admins = _adminService.GetAll();
            return Ok(admins);
        }

        [HttpPost]
        public IActionResult Add([FromBody] AdminDTO adminDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addedadmin = _adminService.Create(adminDto);
            return Ok(addedadmin);
        }
    }
}
