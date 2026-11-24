using fuelstationAPI.Models;
using fuelstationAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace fuelstationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _service;

        public LoginController(ILoginService service)
        {
            _service = service;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            string? token = await _service.LoginAsync(request);
            if (token == null)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Invalid username or password"
                });
            }

            return Ok(new
            {
                success = true,
                token,
                message = "Login successful"
            });
        }
    }
}
