using Application.Interfaces;
using Application.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GimnasioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutineExerciseServiceController : ControllerBase
    {
        private readonly IRoutineExerciseService _routineExerciseService;

        public RoutineExerciseServiceController(IRoutineExerciseService routexerciseService)
        {
            _routineExerciseService = routexerciseService;
        }

        [Authorize(Roles = "Trainer,Admin,SuperAdmin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var relations = _routineExerciseService.GetAll();
                return Ok(relations);
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


        // Hola 

        [Authorize(Roles = "Trainer, SuperAdmin")]
        [HttpPost]
        public IActionResult Add([FromBody] RoutineExerciseDTO dto)
        {
            try
            {
                _routineExerciseService.Add(dto);
                return Ok("Ejercicio agregado correctamente.");
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx.Message);
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

        [Authorize(Roles = "Trainer, SuperAdmin")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] RoutineExerciseDTO dto)
        {
            try
            {
                _routineExerciseService.Update(id, dto);
                return Ok("Relación actualizada correctamente.");
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx.Message);
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

        [Authorize(Roles = "Trainer, SuperAdmin")]
        [HttpDelete("RoutineExercise/{routineId}/{exerciseId}")]
        public IActionResult UnregisterFromGymSession(int routineId, int exerciseId)
        {
            try
            {
                _routineExerciseService.DeleteRoutineExercise(routineId, exerciseId);
                return Ok("Se ha eliminado el ejercicio de la rutina correctamente.");
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
