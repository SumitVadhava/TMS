using TMS.Application.DTOs.Auth;
using TMS.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Services;

namespace TMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);

            if(!result.Success)
            {
                return BadRequest(new
                {
                    success = false,
                    message = result.Message
                });
            }

            return Ok(new { success = true, data = result.Data, message = "User Login Successfully" });
        }

    }
}