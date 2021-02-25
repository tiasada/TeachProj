using Microsoft.AspNetCore.Mvc;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "Admin")]
        public IActionResult Post(CreateUserRequest request)
        {
            var response = _usersService.Create(request.Profile, request.Username, request.Password);

            if (!response.IsValid)
            {
                return BadRequest(response.Errors);
            }

            return NoContent();
        }

        [HttpGet("{username}")]
        [AllowAnonymous]
        public IActionResult GetByUsername(string username)
        {
            var user = _usersService.Get(x => x.Username == username);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            return Ok(_usersService.GetAll());
        }
    }
}