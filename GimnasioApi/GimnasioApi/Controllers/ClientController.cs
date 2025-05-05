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
    }
}
