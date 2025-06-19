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
    public class RoutineController : ControllerBase
    {

        private readonly IRoutineService _routineService;

        public RoutineController(IRoutineService routineService)
        {
            _routineService = routineService;
        }


        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var routines = _routineService.GetAll();
                return Ok(routines);
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


        [Authorize(Roles = "Trainer, SuperAdmin")]
        [HttpPost]
        public IActionResult Add([FromBody] RoutineDTO routineDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var addedRoutine = _routineService.CreateRoutine(routineDTO);
                return Ok(addedRoutine);
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
        [HttpPost("AddRange")]
        public IActionResult AddRange([FromBody] RoutineWithExercisesDTO routineDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var addedRoutine = _routineService.RangeCreateRoutine(routineDTO);
                return Ok(addedRoutine);
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



        [Authorize(Roles = "Trainer, Admin, SuperAdmin")]
        [HttpGet("routines/{trainerId}")]
        public ActionResult<List<RoutineWithExercisesDTO>> GetRoutines(int trainerId)
        {
            try
            {
                var routines = _routineService.GetRoutinesByTrainerId(trainerId);
                if (routines == null || routines.Count == 0)
                    return NotFound("Usted no tiene rutinas creadas.");

                return Ok(routines);
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


        [Authorize(Roles = "Trainer, SuperAdmin")]
        [HttpPut("routine/{routineId}")]
        public IActionResult UpdateRoutine(int routineId, [FromBody] RoutineDTO updatedData)
        {
            try
            {
                _routineService.UpdateRoutine(routineId, updatedData);
                return Ok("Rutina actualizada correctamente.");
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
        [HttpDelete("routine/{routineId}")]
        public IActionResult Delete(int routineId)
        {
            try
            {
                _routineService.DeleteRoutine(routineId);
                return NoContent();
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
