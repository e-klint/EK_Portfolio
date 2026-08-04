using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TheBookParlour.Core.Interfaces;
using TheBookParlour.Data.DTO;
using TheBookParlour.Data.Entities;

namespace TheBookParlour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILoginService loginService, ITokenService tokenService, ILogger<AuthController> logger)
        {
            _loginService = loginService;
            _tokenService = tokenService;
            _logger = logger;
        }

        //Scalar - OK!
        [HttpPost("login")]
        [AllowAnonymous]

        public async Task<IActionResult> Login(Data.DTO.LoginRequest request)
        {
            try {
                //Kontrollera att fälten är korrekt ifyllda med modelstate
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                 //Verifiera att lösenordet är korrekt (med hash)
                var loggedinUser = await _loginService.Handle(request);

                //Generera tecken till användaren
                var token = _tokenService.GenerateToken(loggedinUser);

                //Returnera token
                _logger.LogInformation("User {Username} successfully logged in", request.UserName);
                return Ok(new TokenDTO { JwtToken = token});
                

            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning("Failed login for {Username}", request.UserName);
                _logger.LogWarning(ex.ToString());
                return Unauthorized("Invalid username or password.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Unexpected error during login");
                return NotFound("User not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(500, ex.Message);
                return StatusCode(500);
            }
        }
    }
}
