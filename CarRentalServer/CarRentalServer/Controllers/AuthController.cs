using CarRentalServer.API.ViewModels;
using CarRentalServer.Service.Services.AuthService;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CarRentalServer.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        //POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            try
            {
                await _authService.Register(register.Email, register.Password, register.ConfirmPassword);
                return Ok("User registered successfully");
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }

        //POST: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginViewModel login)
        {
            try
            {
                var accessToken = await _authService.Login(login.Email, login.Password);
                return Ok(accessToken);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }
    }
}
