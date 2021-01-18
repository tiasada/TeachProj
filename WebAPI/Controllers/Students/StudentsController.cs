using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Students;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers.Students
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        public readonly IStudentsService _studentsService;
        public readonly IUsersService _usersService;
        public StudentsController(IUsersService usersService, IStudentsService studentsService)
        {
            _studentsService = studentsService;
            _usersService = usersService;
        }
        
        [HttpPost]
        [Authorize(Roles = "School")]
        public IActionResult Create(CreateStudentRequest request)
        {
            var response = _studentsService.Create(request.Name, request.CPF, request.Registration);

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
            var studentRemoved = _studentsService.Remove(id);

            if (!studentRemoved)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetByID(Guid id)
        {
            var student = _studentsService.Get(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            return Ok(_studentsService.GetAll());
        }
    }
}