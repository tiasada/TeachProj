using Microsoft.AspNetCore.Mvc;
using Domain.Auth;
using WebAPI.Token;

namespace WebAPI.Controllers.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        public readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var response = _authService.Login(request.Username, request.Password);

            if (!response.IsValid)
            {
                return BadRequest("Username ou senha inválido");
            }

            var token = TokenService.GenerateToken(response.User);
            var profile = response.User.Profile.ToString().ToLower();

            return Ok(new LoginResponse{Token = token, Profile = profile});
        }
    }
}