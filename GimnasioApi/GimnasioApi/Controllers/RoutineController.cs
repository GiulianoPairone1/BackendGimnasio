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
    }
}
