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

        [Authorize(Roles = "Trainer, SuperAdmin, Admin")]
        [HttpPut("UpdateProfile/{trainerId}")]
        public IActionResult UpdateProfile(int trainerId, TrainerDTO trainerDto)
        {
            try
            {
                var updatedTrainer = _trainerService.UpdateProfile(trainerId, trainerDto);
                return Ok(updatedTrainer);
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
