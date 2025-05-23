using Application.Interfaces;
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

        [HttpPost("RegisterToGymSession/{clientId}/{sessionId}")]
        public IActionResult RegisterToGymSession(int clientId, int sessionId)
        {
            _clientGymSessionService.RegisterToGymSession(clientId, sessionId);
            return Ok("Cliente registrado en la sesión.");
        }


        [HttpDelete("UnregisterToGymSession/{clientId}/{sessionId}")]
        public IActionResult UnregisterFromGymSession(int clientId, int sessionId)
        {
            _clientGymSessionService.UnregisterFromGymSession(clientId, sessionId);
            return Ok("Cliente desregistrado de la sesión.");
        }
    }
}
