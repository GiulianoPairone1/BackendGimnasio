using Application.Interfaces;
using Application.Models.Dtos;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GimnasioApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ISendEmailService _sendEmailService;

        public AccountController(IAccountService accountService,ISendEmailService sendEmailService)
        {
            _accountService = accountService;
            _sendEmailService = sendEmailService;
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDTO request)
        {
            var token = await _accountService.GeneratePasswordResetTokenAsync(request.Email);
            // Enviar por email, o devolver si es entorno de pruebas.
            _sendEmailService.SendEmail(
                request.Email,
                "Token",
                $"Token: {token}"
                );
            return Ok(new { Token = token });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO dto)
        {
            var success = await _accountService.ResetPasswordAsync(dto.Token, dto.NewPassword);
            return success ? Ok("Contraseña actualizada") : BadRequest("Error al actualizar la contraseña");
        }
    }

}
