using Application.Interfaces;
using Application.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GimnasioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var clients = _clientService.GetAll();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }
        }

   
        [HttpPost]
        public IActionResult Add([FromBody] ClientDTO clientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var addedclient = _clientService.Create(clientDto);
                return Ok(addedclient);
            }
            catch (InvalidOperationException invOpEx)
            {
                return BadRequest(invOpEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }
        }


        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet("GetByMail/{email}")]
        public IActionResult GetByEmail(string email)
        {
            try
            {
                var client = _clientService.GetByGetUserByEmail(email);
                if (client == null)
                    return NotFound("Cliente no encontrado.");

                return Ok(client);
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }
        }


        [Authorize(Roles = "Client, SuperAdmin, Admin")]
        [HttpGet("GetMyClientSessions/{clientId}")]
        public IActionResult GetMyGymSessions(int clientId)
        {
            try
            {
                var sessions = _clientService.GetMyGymSessions(clientId);
                return Ok(sessions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }
        }


        [Authorize(Roles = "Client, SuperAdmin, Admin")]
        [HttpPut("UpdateProfile/{clientId}")]
        public IActionResult UpdateProfile(int clientId, ClientDTO clientDto)
        {
            try
            {
                var updatedClient = _clientService.UpdateProfile(clientId, clientDto);
                return Ok(updatedClient);
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (InvalidOperationException invOpEx)
            {
                return BadRequest(invOpEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }
        }


        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet("ById/{clientId}")]
        public IActionResult GetClientById(int clientId)
        {
            try
            {
                var client = _clientService.GetClientById(clientId);
                if (client == null)
                    return NotFound("Cliente no encontrado.");

                return Ok(client);
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }
        }

    }
}
