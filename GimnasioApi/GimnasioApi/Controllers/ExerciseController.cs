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
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var exercises = _exerciseService.GetAll();
            return Ok(exercises);
        }

        [HttpPost]
        [Authorize(Roles = "Trainer")]
        public IActionResult Add([FromBody] ExerciseDTO exerciseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addedexercise = _exerciseService.CreateExercise(exerciseDTO);
            return Ok(addedexercise);
        }

        [HttpPut("{id}")]
        public ActionResult<ExerciseDTO> UpdateExercise(int id, [FromBody] ExerciseDTO updatedExercise)
        {
            try
            {
                var result = _exerciseService.UpdateExercise(id, updatedExercise);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteExercise(int id)
        {
            try
            {
                var success = _exerciseService.DeleteExercise(id);
                if (success)
                    return NoContent();

                return StatusCode(500, "No se pudo eliminar el ejercicio.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}
