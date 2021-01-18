using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Teachers;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers.Teachers
{
    [ApiController]
    [Route("[controller]")]
    public class TeachersController : ControllerBase
    {
        public readonly ITeachersService _teachersService;
        public readonly IUsersService _usersService;
        public TeachersController(IUsersService usersService, ITeachersService teachersService)
        {
            _teachersService = teachersService;
            _usersService = usersService;
        }
        
        [HttpPost]
        [Authorize(Roles = "School")]
        public IActionResult Post(CreateTeacherRequest request)
        {
            var response = _teachersService.Create(request.Name, request.CPF);

            if (!response.IsValid)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Id);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "School")]
        public IActionResult Remove(Guid id)
        {
            var teacherRemoved = _teachersService.Remove(id);

            if (!teacherRemoved)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]

        public IActionResult GetByID(Guid id)
        {
            var teacher = _teachersService.Get(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult GetAll()
        {
            return Ok(_teachersService.GetAll());
        }
    }
}