using Application.Interfaces;
using Application.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GimnasioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymSessionController : ControllerBase
    {
        private readonly IGymSessionService _gymSessionService;

        public GymSessionController(IGymSessionService gymSessionService)
        {
            _gymSessionService = gymSessionService;
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var sessions = _gymSessionService.GetAllGymSessions();
                return Ok(sessions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }
        }


        [HttpGet("GetAllGymSessionsAvailable")]
        public IActionResult GetAllAvailable()
        {
            try
            {
                var sessions = _gymSessionService.GetAllAvailable();
                return Ok(sessions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }
        }

        [Authorize(Roles = "Trainer,SuperAdmin, Admin")]
        [HttpGet("GetMyTrainerSessions/{trainerId}")]
        public IActionResult GetMySessions(int trainerId)
        {
            try
            {
                var sessions = _gymSessionService.GetMySessions(trainerId);
                return Ok(sessions);
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

        [Authorize(Roles = "Trainer,SuperAdmin, Admin")]
        [HttpPost]
        public IActionResult Create([FromBody] GymSessionDTO newSessionDto)
        {
            try
            {
                var createdSession = _gymSessionService.CreateGymSession(newSessionDto);
                return CreatedAtAction(nameof(GetAll), new { id = createdSession.Id }, createdSession);
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


        [Authorize(Roles = "Trainer,SuperAdmin, Admin")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] GymSessionDTO updatedData)
        {
            try
            {
                var updatedSession = _gymSessionService.UpdateGymSession(id, updatedData);
                return Ok(updatedSession);
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
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


        [Authorize(Roles = "Trainer,Admin,SuperAdmin")]
        [HttpDelete("Cancel/{sessionId}")]
        public IActionResult Cancel(int sessionId)
        {
            try
            {
                _gymSessionService.CancelGymSession(sessionId);
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

        [HttpGet("byDate")]
        public async Task<IActionResult> GetSessionsByDate([FromQuery] DateTime date)
        {
            try
            {
                var sessions = await _gymSessionService.GetSessionsByDateAsync(date);
                return Ok(sessions);
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete("{sessionId}")]
        public IActionResult DeleteSession(int sessionId)
        {
            try
            {
                _gymSessionService.DeleteGymSession(sessionId);
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
