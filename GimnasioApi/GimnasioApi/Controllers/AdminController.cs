using Application.Interfaces;
using Application.Models.Dtos;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            try
            {
                var admins = _adminService.GetAll();
                return Ok(admins);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost("Create")]
        public IActionResult Add([FromBody] AdminDTO adminDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var addedadmin = _adminService.Create(adminDto);
                return Ok(addedadmin);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("GetUserByEmail")]
        public IActionResult GetUserByEmail(string email)
        {
            try
            {
                var user = _adminService.GetUserByEmail(email);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("GetUsersAvailable")]
        public IActionResult GetUsersAvailable()
        {
            try
            {
                var users = _adminService.GetUsersAvailable();
                return Ok(users);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("GetUsersAvailableByType")]
        public IActionResult GetUsersAvailableByType<T>() where T : User
        {
            try
            {
                var users = _adminService.GetUsersAvailable<T>();
                return Ok(users);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete("DisableUser")]
        public IActionResult DisableUser(string mail)
        {
            try
            {
                _adminService.DisableUser(mail);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete("DeleteUser")]
        public IActionResult Delete(string mail)
        {
            try
            {
                _adminService.HardDeleteUser(mail);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPut("activate-user/{email}")]
        public IActionResult ActivateUser(string email)
        {
            try
            {
                _adminService.ActivateUser(email);
                return Ok($"Usuario activado con éxito.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

