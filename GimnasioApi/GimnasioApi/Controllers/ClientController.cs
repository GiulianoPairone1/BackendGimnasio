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

        //Este lo usa el admin no mas
        [HttpGet("Email")]
        public IActionResult GetUserByEmail([FromQuery]string email)
        { 
            var client=_clientService.GetByGetUserByEmail(email);

            if (client == null)
            {
                return NotFound("No se encontro el cliente");
            }
            return Ok(client);
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

        [HttpGet]
        public IActionResult GetAll()
        {
            var clients = _clientService.GetAll();
            return Ok(clients);
        }

     
        [HttpGet("{email}")]
        public IActionResult GetByEmail(string email)
        {
            var client = _clientService.GetByGetUserByEmail(email);
            if (client == null)
                return NotFound("Cliente no encontrado.");

            return Ok(client);
        }


        [HttpPost]
        public IActionResult Create(ClientDTO clientDto)
        {
            var client = _clientService.Create(clientDto);
            return CreatedAtAction(nameof(GetByEmail), new { email = client.Email }, client);
        }


        [HttpGet("{clientId}/sessions")]
        public IActionResult GetMyGymSessions(int clientId)
        {
            var sessions = _clientService.GetMyGymSessions(clientId);
            return Ok(sessions);
        }


        [HttpPost("{clientId}/sessions/{sessionId}")]
        public IActionResult RegisterToGymSession(int clientId, int sessionId)
        {
            _clientService.RegisterToGymSession(clientId, sessionId);
            return Ok("Cliente registrado en la sesión.");
        }


        [HttpDelete("{clientId}/sessions/{sessionId}")]
        public IActionResult UnregisterFromGymSession(int clientId, int sessionId)
        {
            _clientService.UnregisterFromGymSession(clientId, sessionId);
            return Ok("Cliente desregistrado de la sesión.");
        }


        [HttpPut("{clientId}")]
        public IActionResult UpdateProfile(int clientId, ClientDTO clientDto)
        {
            var updatedClient = _clientService.UpdateProfile(clientId, clientDto);
            return Ok(updatedClient);
        }
    }
}
