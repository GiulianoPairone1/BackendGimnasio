using Application.Interfaces;
using Application.Models.Dtos;

using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult GetAll()
        {
            var relations = _routineExerciseService.GetAll();
            return Ok(relations); // Aquí devolverías la lista con los nombres incluidos
        }

        [HttpPost]
        public IActionResult Add([FromBody] RoutineExerciseDTO dto)
        {
            try
            {
                _routineExerciseService.Add(dto);
                return Ok("Ejercicio agregado correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] RoutineExerciseDTO dto)
        {
            try
            {
                _routineExerciseService.Update(id, dto);
                return Ok("Relación actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
