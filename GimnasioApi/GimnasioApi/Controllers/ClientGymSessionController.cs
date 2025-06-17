using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GimnasioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientGymSessionController : Controller
    {
        private readonly IClientGymSessionService _clientGymSessionService;
        public ClientGymSessionController(IClientGymSessionService clientGymSessionService)
        {
            _clientGymSessionService = clientGymSessionService;
        }


        [Authorize(Roles = "Client, SuperAdmin")]
        [HttpPost("RegisterToGymSession/{clientId}/{sessionId}")]
        public IActionResult RegisterToGymSession(int clientId, int sessionId)
        {
            try
            {
                _clientGymSessionService.RegisterToGymSession(clientId, sessionId);
                return Ok("Cliente registrado en la sesión.");
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

        [Authorize(Roles = "Client, SuperAdmin")]
        [HttpDelete("UnregisterToGymSession/{clientId}/{sessionId}")]
        public IActionResult UnregisterFromGymSession(int clientId, int sessionId)
        {
            try
            {
                _clientGymSessionService.UnregisterFromGymSession(clientId, sessionId);
                return Ok("Cliente desregistrado de la sesión.");
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
    }
}
