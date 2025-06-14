﻿using Application.Interfaces;
using Application.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult GetAll()
        {
            var sessions = _gymSessionService.GetAllGymSessions();
            return Ok(sessions);
        }

        [HttpGet("GetAllGymSessionsAvailable")]
        public IActionResult GetAllAvailable()
        {
            var sessions = _gymSessionService.GetAllAvailable();
            return Ok(sessions);
        }

        [HttpGet("GetMyTrainerSessions/{trainerId}")]
        public IActionResult GetMySessions(int trainerId)
        {
            var sessions = _gymSessionService.GetMySessions(trainerId);
            return Ok(sessions);
        }

        [HttpPost]
        public IActionResult Create([FromBody] GymSessionDTO newSessionDto)
        {
            var createdSession = _gymSessionService.CreateGymSession(newSessionDto);
            //return CreatedAtAction(nameof(GetAll), new { id = createdSession.Id }, createdSession);
            return CreatedAtAction(nameof(GetAll),createdSession);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] GymSessionDTO updatedData)
        {
            var updatedSession = _gymSessionService.UpdateGymSession(id, updatedData);
            return Ok(updatedSession);
        }

        [HttpDelete("Cancel/{sessionId}")]
        public IActionResult Cancel(int sessionId)
        {
            _gymSessionService.CancelGymSession(sessionId);
            return NoContent();
        }

        [HttpGet("byDate")]
        public async Task<IActionResult> GetSessionsByDate([FromQuery] DateTime date)
        {
            var sessions = await _gymSessionService.GetSessionsByDateAsync(date);
            return Ok(sessions);
        }

        [HttpDelete("{sessionId}")]
        public IActionResult DeleteSession(int sessionId)
        {
            _gymSessionService.DeleteGymSession(sessionId);
            return NoContent();
        }
    }
}
