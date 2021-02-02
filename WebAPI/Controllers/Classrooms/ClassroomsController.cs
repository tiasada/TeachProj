using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Classrooms;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Domain.Teachers;
using System.Linq;

namespace WebAPI.Controllers.Classrooms
{
    [ApiController]
    [Route("[controller]")]
    public class ClassroomsController : ControllerBase
    {
        public readonly IClassroomsService _classroomsService;
        public readonly IUsersService _usersService;
        public readonly ITeachersService _teachersService;
        public ClassroomsController(IUsersService usersService, IClassroomsService classroomsService, ITeachersService teachersService)
        {
            _classroomsService = classroomsService;
            _usersService = usersService;
            _teachersService = teachersService;
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

            if (!classroomRemoved)
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
        public IActionResult AddSubject(Guid id, [FromBody]string subject)
        {
            var subjectAdded = _classroomsService.AddSubject(id, subject);

            if (subjectAdded != null)
            {
                return BadRequest(subjectAdded);
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        [Authorize (Roles = "School, Teacher")]
        public IActionResult GetByID(Guid id)
        {
            var classroom = _classroomsService.Get(id);

            if (classroom == null)
            {
                return NotFound();
            }

            return Ok(classroom);
        }

        [HttpGet]
        [Authorize (Roles = "School,Teacher")]
        public IActionResult GetAll()
        {
            if (HttpContext.User.IsInRole("Teacher"))
            {
                var username = HttpContext.User.Claims.ElementAt(0).Value;
                var teacher = _teachersService.Get(x => x.CPF == username);
                return Ok(_classroomsService.GetByTeacher(teacher.Id));
            }
            
            return Ok(_classroomsService.GetAll());
        }
    }
}