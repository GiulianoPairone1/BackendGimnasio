using Application.Interfaces;
using Application.Models.Dtos;
using Application.Services;

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
        public IActionResult Add([FromBody] ExerciseDTO exerciseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addedexercise = _exerciseService.Create(exerciseDTO);
            return Ok(addedexercise);
        }

    }
}
