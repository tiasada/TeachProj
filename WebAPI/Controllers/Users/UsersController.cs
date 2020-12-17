using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Users;

namespace WebAPI.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        public readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        
        [HttpPost]
        public IActionResult Post(CreateUserRequest request)
        {
            var response = _usersService.Create(request.Profile, request.Username, request.Password);

            if (!response.IsValid)
            {
                return BadRequest(response.Errors);
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {
            var user = _usersService.Get(x => x.Id == id);

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