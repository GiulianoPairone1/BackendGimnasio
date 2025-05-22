using Application.Interfaces;
using Application.Models.Dtos;

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

        [HttpGet]
        public IActionResult Get()
        {
            var clients = _clientService.GetAll();
            return Ok(clients);
        }


        [HttpPost]
        public IActionResult Add([FromBody] ClientDTO clientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addedclient = _clientService.Create(clientDto);
            return Ok(addedclient);
        }

     
        [HttpGet("GetByMail/{email}")]
        public IActionResult GetByEmail(string email)
        {
            var client = _clientService.GetByGetUserByEmail(email);
            if (client == null)
                return NotFound("Cliente no encontrado.");

            return Ok(client);
        }


        [HttpGet("GetMyClientSessions/{clientId}")]
        public IActionResult GetMyGymSessions(int clientId)
        {
            var sessions = _clientService.GetMyGymSessions(clientId);
            return Ok(sessions);
        }


        [HttpPost("RegisterToGymSession/{clientId}/{sessionId}")]
        public IActionResult RegisterToGymSession(int clientId, int sessionId)
        {
            _clientService.RegisterToGymSession(clientId, sessionId);
            return Ok("Cliente registrado en la sesión.");
        }


        [HttpDelete("UnregisterToGymSession/{clientId}/{sessionId}")]
        public IActionResult UnregisterFromGymSession(int clientId, int sessionId)
        {
            _clientService.UnregisterFromGymSession(clientId, sessionId);
            return Ok("Cliente desregistrado de la sesión.");
        }


        [HttpPut("UpdateProfile/{clientId}")]
        public IActionResult UpdateProfile(int clientId, ClientDTO clientDto)
        {
            var updatedClient = _clientService.UpdateProfile(clientId, clientDto);
            return Ok(updatedClient);
        }
    }
}
