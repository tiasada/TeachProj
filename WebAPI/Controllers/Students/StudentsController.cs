using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain.Students;
using Domain.Users;
using Microsoft.Extensions.Primitives;

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
        public IActionResult Post(CreateStudentRequest request)
        {
            StringValues headerId;
            var foundId = Request.Headers.TryGetValue("UserId", out headerId);
            if (!foundId) { return Unauthorized("User ID must be informed"); }

            var validId = Guid.TryParse(headerId, out var userId);
            if (!validId) { return Unauthorized("Invalid ID"); }
            
            var user = _usersService.Get(x => x.Id == userId);

            if (user == null)
            {
                return Unauthorized("User does not exist");
            }
            
            if (user.Profile != Profile.School)
            {
                return StatusCode(403, "User is not School");
            }

            var response = _studentsService.Create(request.Name, request.CPF, request.Registration);

            if (!response.IsValid)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Id);
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(Guid id)
        {
            StringValues headerId;
            var foundId = Request.Headers.TryGetValue("UserId", out headerId);
            if (!foundId) { return Unauthorized("User ID must be informed"); }

            var validId = Guid.TryParse(headerId, out var userId);
            if (!validId) { return Unauthorized("Invalid ID"); }

            var user = _usersService.Get(x => x.Id == userId);

            if (user == null)
            {
                return Unauthorized();
            }

            if (user.Profile != Profile.School)
            {
                return StatusCode(403, "User is not School");
            }

            var studentRemoved = _studentsService.Remove(id);

            if (studentRemoved == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPatch("{id}/add/{classId}")]
        public IActionResult AddClass(Guid id, Guid classId)
        {
            StringValues headerId;
            var foundId = Request.Headers.TryGetValue("UserId", out headerId);
            if (!foundId) { return Unauthorized("User ID must be informed"); }

            var validId = Guid.TryParse(headerId, out var userId);
            if (!validId) { return Unauthorized("Invalid ID"); }

            var user = _usersService.Get(x => x.Id == userId);

            if (user == null)
            {
                return Unauthorized();
            }

            if (user.Profile != Profile.School)
            {
                return StatusCode(403, "User is not School");
            }

            var studentAdded = _studentsService.AddClass(id, classId);

            if (studentAdded != null)
            {
                return BadRequest(studentAdded);
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {
            var student = _studentsService.Get(x => x.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_studentsService.GetAll());
        }
    }
}