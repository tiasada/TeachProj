using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Users;

namespace WebAPI.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        public readonly UsersService _usersService;
        public UsersController()
        {
            _usersService = new UsersService();
        }
        
        [HttpPost]
        public IActionResult Post(CreateUserRequest request)
        {
            if (request.Password != "admin123" && request.Profile == Profile.Admin)
            {
                return Unauthorized();
            }

            var response = _usersService.Create(request.Name, request.CPF, request.Profile);

            if (!response.IsValid)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Id);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {
            var user = _usersService.GetByID(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_usersService.GetAll());
        }
    }
}