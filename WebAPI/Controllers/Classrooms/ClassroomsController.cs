using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain.Classrooms;
using Domain.Users;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers.Classrooms
{
    [ApiController]
    [Route("[controller]")]
    public class ClassroomsController : ControllerBase
    {
        public readonly IClassroomsService _classroomsService;
        public readonly IUsersService _usersService;
        public ClassroomsController(IUsersService usersService, IClassroomsService classroomsService)
        {
            _classroomsService = classroomsService;
            _usersService = usersService;
        }
        
        [HttpPost]
        [Authorize(Roles = "School")]
        public IActionResult Post(CreateClassroomRequest request)
        {
            var response = _classroomsService.Create(request.Name);

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
            var classroomRemoved = _classroomsService.Remove(id);

            if (classroomRemoved == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPatch("{id}/addstudent/{studentId}")]
        [Authorize(Roles = "School")]
        public IActionResult AddStudent(Guid id, Guid studentId)
        {
            var studentAdded = _classroomsService.AddStudent(studentId, id);

            if (studentAdded != null)
            {
                return BadRequest(studentAdded);
            }

            return NoContent();
        }

        [HttpPatch("{id}/addteacher/{teacherId}")]
        [Authorize(Roles = "School")]

        public IActionResult AddTeacher(Guid id, Guid teacherId)
        {
            var studentAdded = _classroomsService.AddTeacher(teacherId, id);

            if (studentAdded != null)
            {
                return BadRequest(studentAdded);
            }

            return NoContent();
        }

        [HttpPatch("{id}/addsubject")]
        [Authorize(Roles = "School")]
        public IActionResult AddSubject(Guid id, [FromBody]string subjects)
        {
            var subjectAdded = _classroomsService.AddSubjects(id, subjects);

            if (subjectAdded != null)
            {
                return BadRequest(subjectAdded);
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]

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
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            return Ok(_classroomsService.GetAll());
        }
    }
}