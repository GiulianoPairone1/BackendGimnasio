﻿using Application.Interfaces;
using Application.Models.Dtos;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GimnasioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }


        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            var admins = _adminService.GetAll();
            return Ok(admins);
        }

        [HttpPost("Create")]
        public IActionResult Add([FromBody] AdminDTO adminDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addedadmin = _adminService.Create(adminDto);
            return Ok(addedadmin);
        }

        [HttpGet("GetUserByEmail")]
        public IActionResult GetUserByEmail(string email)
        {
            var user = _adminService.GetUserByEmail(email);
            return Ok(user);
        }

        [HttpGet("GetUsersAvailable")]
        public IActionResult GetUsersAvailable()
        {
            var users = _adminService.GetUsersAvailable();
            return Ok(users);
        }

        [HttpGet("GetUsersAvailableByType")]
        public IActionResult GetUsersAvailableByType<T>() where T : User
        {
            var users = _adminService.GetUsersAvailable<T>();
            return Ok(users);
        }


        [HttpDelete("DisableUser")]
        public IActionResult DisableUser(string mail)
        {
            _adminService.DisableUser(mail);
            return NoContent();
        }

        [HttpDelete("DeleteUser")]
        public IActionResult Delete(string mail)
        {
            _adminService.HardDeleteUser(mail);
            return NoContent();
        }

        [HttpPut("activate-user/{email}")]
        public IActionResult ActivateUser(string email)
        {
            try
            {
                _adminService.ActivateUser(email);
                return Ok($"Usuario activado con éxito.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

