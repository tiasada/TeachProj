using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Users;
using Domain.Auth;

namespace WebAPI.Controllers.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        public readonly AuthService _authService;
        public AuthController()
        {
            _authService = new AuthService();
        }
        
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var response = _authService.Login(request.CPF, request.Password);

            if (!response.IsValid)
            {
                return BadRequest("CPF ou senha inválido");
            }

            return Ok(response.UserId);
        }
    }
}