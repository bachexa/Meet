using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Auth;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("forgot-password")]
        public IActionResult Forgot([FromBody] ForgotPasswordViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.PhoneNumber))
                return BadRequest("Phone is required");

            // later send sms code…

            return Ok(new { message = "Code sent" });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
                return BadRequest("Passwords mismatch");

            // later add DB logic…

            return Ok(new { message = "Registered successfully" });
        }
    }
}