using Application.Interfaces;
using Application.Models.Dtos;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GimnasioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperAdminController : ControllerBase
    {
        private readonly ISuperAdminService _superadminservice;

        public SuperAdminController (ISuperAdminService superadminservice)
        {
            _superadminservice = superadminservice;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public IActionResult Get()
        {
            var superadminds = _superadminservice.GetAll();
            return Ok(superadminds);
        }


        [HttpPost]
        public IActionResult Add([FromBody] SuperAdminDTO superadminDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addedsuperadmin = _superadminservice.Create(superadminDto);
            return Ok(addedsuperadmin);
        }
    }
}
