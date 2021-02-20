using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Classrooms;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Domain.Teachers;
using System.Linq;
using System.Collections.Generic;
using Domain.StudentPresences;
using Domain.Students;

namespace WebAPI.Controllers.Classrooms
{
    [ApiController]
    [Route("[controller]")]
    public class ClassroomsController : ControllerBase
    {
        public readonly IClassroomsService _classroomsService;
        public readonly IUsersService _usersService;
        public readonly ITeachersService _teachersService;
        public readonly IStudentsService _studentsService;
        public ClassroomsController(IUsersService usersService, IClassroomsService classroomsService, IStudentsService studentsService, ITeachersService teachersService)
        {
            _classroomsService = classroomsService;
            _usersService = usersService;
            _teachersService = teachersService;
            _studentsService = studentsService;
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
        [Authorize(Roles = "Admin, School, Teacher")]
        public IActionResult SetPresences(Guid id, List<SetPresenceRequest> request)
        {
            var presences = new List<StudentPresence>();
            var classroom = _classroomsService.Get(id);

            foreach (var item in request)
            {
                var student = _studentsService.Get(item.StudentId);
                if (student == null)
                {
                    return BadRequest("Student not found");
                }

                presences.Add(new StudentPresence(classroom, student, item.IsPresent, item.Reason));
            }
            
            var presenceSet = _classroomsService.SetPresences(id, presences);

            if (presenceSet != null)
            {
                return BadRequest(presenceSet);
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
            var classroom = _classroomsService.Get(id);

            if (classroom == null)
            {
                return NotFound();
            }
            
            if (HttpContext.User.IsInRole("Teacher"))
            {
                var username = HttpContext.User.Claims.ElementAt(0).Value;
                if (!classroom.Teachers.Any(x => x.Teacher.CPF == username))
                { return Unauthorized("Teacher not assigned to classroom"); }
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