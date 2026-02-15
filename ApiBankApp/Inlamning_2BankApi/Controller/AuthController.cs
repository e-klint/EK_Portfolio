using ApiBankApp.Core.Interfaces;
using ApiBankApp.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBankApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;
        
        public AuthController(IUserService service)
        {
            _service = service;
        }

        //Loginmetod + JWT 
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var (isValid, user) = await _service.ValidateUserAsync(request.Username, request.Password);

            if (!isValid) return Unauthorized("Incorrect username or password");

            var token = _service.GenerateToken(user);
            return Ok(new TokenDTO { JwtToken = token });
        }
    }
}

