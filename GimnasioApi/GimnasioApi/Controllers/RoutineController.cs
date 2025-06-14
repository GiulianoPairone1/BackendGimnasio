using Application.Interfaces;
using Application.Models.Dtos;
using Application.Services;

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

        [HttpGet]
        public IActionResult Get()
        {
            var routines = _routineService.GetAll();
            return Ok(routines);
        }

        [HttpPost]
        public IActionResult Add([FromBody] RoutineDTO routineDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addedroutine = _routineService.CreateRoutine(routineDTO);
            return Ok(addedroutine);
        }

        [HttpGet("routines/{trainerId}")]
        public ActionResult<List<RoutineWithExercisesDTO>> GetRoutines(int trainerId)
        {
            var routines = _routineService.GetRoutinesByTrainerId(trainerId);
            if (routines == null || routines.Count == 0)
            {
                return NotFound("Usted no tiene rutinas creadas.");
            }
            return Ok(routines);
        }

        [HttpPut("routine/{routineId}")]
        public IActionResult UpdateRoutine(int routineId, [FromBody] RoutineDTO updatedData)
        {
            var updateRoutine = _routineService.UpdateRoutine(routineId, updatedData);
            return Ok();
        }

        [HttpDelete("routine/{routineId}")]
        public IActionResult Delete(int routineId)
        {
            _routineService.DeleteRoutine(routineId);
            return NoContent();
        }


    }
}
