using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Classrooms;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Domain.Teachers;
using System.Linq;
using System.Collections.Generic;
using Domain.ClassDays;

namespace WebAPI.Controllers.Classrooms
{
    [ApiController]
    [Route("[controller]")]
    public class ClassroomsController : ControllerBase
    {
        public readonly IClassroomsService _classroomsService;
        public readonly IUsersService _usersService;
        public readonly IClassDaysService _classDaysService;
        public readonly ITeachersService _teachersService;
        public ClassroomsController(IUsersService usersService, IClassroomsService classroomsService, IClassDaysService classDaysService, ITeachersService teachersService)
        {
            _classroomsService = classroomsService;
            _usersService = usersService;
            _classDaysService = classDaysService;
            _teachersService = teachersService;
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin,School")]
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
        [Authorize(Roles = "Admin,School")]
        public IActionResult Remove(Guid id)
        {
            var classroomRemoved = _classroomsService.Remove(id);

            if (!classroomRemoved)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("{classId}/students")]
        [Authorize(Roles = "Admin,School")]
        public IActionResult AddStudent(Guid classId, AddToClassroomRequest request)
        {
            var studentAdded = _classroomsService.AddStudent(request.Id, classId);

            if (studentAdded != null)
            {
                return BadRequest(studentAdded);
            }

            return NoContent();
        }

        [HttpPost("{classId}/teachers")]
        [Authorize(Roles = "Admin,School")]

        public IActionResult AddTeacher(Guid classId, AddToClassroomRequest request)
        {
            var teacherAdded = _classroomsService.AddTeacher(request.Id, classId);

            if (teacherAdded != null)
            {
                return BadRequest(teacherAdded);
            }

            return NoContent();
        }

        [HttpPost("{id}/subjects")]
        [Authorize(Roles = "Admin,School")]
        public IActionResult AddSubject(Guid id, [FromBody]string subject)
        {
            var subjectAdded = _classroomsService.AddSubject(id, subject);

            if (subjectAdded != null)
            {
                return BadRequest(subjectAdded);
            }

            return NoContent();
        }

        [HttpPost("{id}/presences")]
        [Authorize(Roles = "Teacher")]
        public IActionResult SetPresences(Guid id, List<SetPresenceRequest> request)
        {
            var createdDay = _classDaysService.Create(DateTime.Now.Date, id, "");
            
            if (!createdDay.IsValid)
            {
                return BadRequest(createdDay.Errors);
            }

            foreach (var item in request){
                var presenceSet = _classDaysService.SetPresence(createdDay.Id, item.StudentId, item.IsPresent, item.Reason);

                if (presenceSet != null)
                {
                    return BadRequest(presenceSet);
                }
            }

            return NoContent();
        }

        [HttpGet("{classId}/students")]
        [Authorize (Roles = "Admin,School,Teacher")]
        public IActionResult GetStudents(Guid classId)
        {
            if (HttpContext.User.IsInRole("Teacher"))
            {
                var username = HttpContext.User.Claims.ElementAt(0).Value;
                var teacher = _teachersService.Get(x => x.CPF == username);
                if (_classroomsService.GetTeacher(classId, teacher.Id) == null)
                { return Unauthorized("Teacher not assigned to classroom"); }
            }
            
            return Ok(_classroomsService.GetStudents(classId).OrderBy(x => x.Name));
        }

        [HttpGet("{classId}/teachers")]
        [Authorize (Roles = "Admin,School")]
        public IActionResult GetTeachers(Guid classId)
        {
            return Ok(_classroomsService.GetTeachers(classId).OrderBy(x => x.Name));
        }

        [HttpGet("{classId}/grades")]
        [Authorize (Roles = "Admin,School,Teacher")]
        public IActionResult GetGrades(Guid classId)
        {
            if (HttpContext.User.IsInRole("Teacher"))
            {
                var username = HttpContext.User.Claims.ElementAt(0).Value;
                var teacher = _teachersService.Get(x => x.CPF == username);
                if (_classroomsService.GetTeacher(classId, teacher.Id) == null)
                { return Unauthorized("Teacher not assigned to classroom"); }
            }
            
            return Ok(_classroomsService.GetGrades(classId));
        }
        
        [HttpGet("{id}")]
        [Authorize (Roles = "Admin,School,Teacher")]
        public IActionResult GetByID(Guid id)
        {
            if (HttpContext.User.IsInRole("Teacher"))
            {
                var username = HttpContext.User.Claims.ElementAt(0).Value;
                var teacher = _teachersService.Get(x => x.CPF == username);
                if (_classroomsService.GetTeacher(id, teacher.Id) == null)
                { return Unauthorized("Teacher not assigned to classroom"); }
            }
            
            var classroom = _classroomsService.Get(id);

            if (classroom == null)
            {
                return NotFound();
            }

            return Ok(classroom);
        }

        [HttpGet]
        [Authorize (Roles = "Admin,School,Teacher")]
        public IActionResult GetAll(string name)
        {
            IEnumerable<Classroom> classrooms = new List<Classroom>();
            
            if (HttpContext.User.IsInRole("Teacher"))
            {
                var username = HttpContext.User.Claims.ElementAt(0).Value;
                var teacher = _teachersService.Get(x => x.CPF == username);
                classrooms = _classroomsService.GetByTeacher(teacher.Id);
            }
            else
            {
                classrooms = _classroomsService.GetAll();
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                var transformedName = name.ToLower().Trim();
                classrooms = classrooms.Where(x => x.Name.ToLower().Contains(transformedName));
            }

            return Ok(classrooms.OrderBy(x => x.Name));
        }
    }
}