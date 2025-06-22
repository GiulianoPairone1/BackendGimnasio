using Application.Interfaces;
using Application.Models.Dtos;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace GimnasioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ISendEmailService _sendEmailService;

        public AdminController(IAdminService adminService, ISendEmailService sendEmailService)
        {
            _adminService = adminService;
            _sendEmailService = sendEmailService;
        }

        [HttpPost("enviar-consulta")]
        public IActionResult EnviarConsulta([FromBody] InfoEmailDTO infoMailDTO)
        {

            if (infoMailDTO == null)
            {
                return BadRequest("No se proporcionó información.");
            }
            if (string.IsNullOrWhiteSpace(infoMailDTO.Name))
            {
                return BadRequest("El nombre es requerido.");
            }
            if (string.IsNullOrWhiteSpace(infoMailDTO.Surname))
            {
                return BadRequest("El apellido es requerido.");
            }
            if (string.IsNullOrWhiteSpace(infoMailDTO.Email))
            {
                return BadRequest("El correo electrónico es requerido.");
            }
            if (string.IsNullOrWhiteSpace(infoMailDTO.Phone))
            {
                return BadRequest("El teléfono es requerido.");
            }
            if (!new EmailAddressAttribute().IsValid(infoMailDTO.Email))
            {
                return BadRequest("El correo electrónico no tiene un formato válido.");
            }
            if (string.IsNullOrWhiteSpace(infoMailDTO.Message))
            {
                return BadRequest("El mensaje de consulta es requerido.");
            }


            try
            {

                string MailGymUTN = "utneggergym@gmail.com";
                string subject = $"Nueva consulta de {infoMailDTO.Name} {infoMailDTO.Surname}";
                string body = $"Solicitante: {infoMailDTO.Name} {infoMailDTO.Surname}\n" +
                               $"Email: {infoMailDTO.Email}\n\n" +
                               $"Mensaje:\n{infoMailDTO.Message}";


                _sendEmailService.SendEmail(MailGymUTN, subject, body);
                return Ok("Consulta enviada.");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error interno al intentar enviar la consulta: {ex.Message}");

            }
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
        [HttpDelete("DisableUser/{mail}")]
        public IActionResult DisableUser([FromRoute] string mail)
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

