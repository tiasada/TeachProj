using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain.Classrooms;
using Domain.Users;
using Microsoft.Extensions.Primitives;

namespace WebAPI.Controllers.Classrooms
{
    [ApiController]
    [Route("[controller]")]
    public class ClassroomsController : ControllerBase
    {
        public readonly IClassroomsService _classroomsService;
        public readonly IUsersService _usersService;
        public ClassroomsController(UsersService usersService, ClassroomsService classroomsService)
        {
            _classroomsService = classroomsService;
            _usersService = usersService;
        }
        
        [HttpPost]
        public IActionResult Post(CreateClassroomRequest request)
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

            var response = _classroomsService.Create(request.Name);

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

            var classroomRemoved = _classroomsService.Remove(id);

            if (classroomRemoved == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPatch("{id}/addstudent/{studentId}")]
        public IActionResult AddStudent(Guid id, Guid studentId)
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

            var studentAdded = _classroomsService.AddStudent(studentId, id);

            if (studentAdded != null)
            {
                return BadRequest(studentAdded);
            }

            return NoContent();
        }

        [HttpPatch("{id}/addteacher/{teacherId}")]
        public IActionResult AddTeacher(Guid id, Guid teacherId)
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

            var studentAdded = _classroomsService.AddTeacher(teacherId, id);

            if (studentAdded != null)
            {
                return BadRequest(studentAdded);
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {
            var classroom = _classroomsService.Get(x => x.Id == id);

            if (classroom == null)
            {
                return NotFound();
            }

            return Ok(classroom);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_classroomsService.GetAll());
        }
    }
}