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
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerService _trainerService;
        
        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet]
        public IActionResult Get()
        {
            var trainers = _trainerService.GetAll();
            return Ok(trainers);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        public IActionResult Add([FromBody] TrainerDTO trainerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addedtrainer = _trainerService.Create(trainerDto);
            return Ok(addedtrainer);
        }

    }
}
