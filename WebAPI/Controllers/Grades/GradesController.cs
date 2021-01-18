using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Grades;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers.Grades
{
    [ApiController]
    [Route("[controller]")]
    public class GradesController : ControllerBase
    {
        public readonly IGradesService _gradesService;
        public readonly IUsersService _usersService;
        public GradesController(IUsersService usersService, IGradesService gradesService)
        {
            _gradesService = gradesService;
            _usersService = usersService;
        }
        
        [HttpPost]
        [Authorize(Roles = "School,Teacher")]

        public IActionResult Post(CreateGradeRequest request)
        {

            var response = _gradesService.Create(request.Name, request.Description, request.Subject, request.Date, request.ClassroomId);

            if (!response.IsValid)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Id);
        }

        [HttpPatch("{id}/setgrade/{studentId}")]
        [Authorize(Roles = "School,Teacher")]
        public IActionResult SetGrade(Guid id, Guid studentId, [FromBody]double value)
        {

            var gradeSet = _gradesService.SetGrade(id, studentId, value);

            if (gradeSet != null)
            {
                return BadRequest(gradeSet);
            }

            return NoContent();
        }

        [HttpPatch("{id}/close")]
        [Authorize(Roles = "School,Teacher")]
        public IActionResult CloseGrade(Guid id)
        {

            var gradeClosed = _gradesService.CloseGrade(id);

            if (gradeClosed != null)
            {
                return BadRequest(gradeClosed);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "School,Teacher")]

        public IActionResult Remove(Guid id)
        {
            var gradeRemoved = _gradesService.Remove(id);

            if (!gradeRemoved)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]

        public IActionResult GetByID(Guid id)
        {
            var grade = _gradesService.Get(id);

            if (grade == null)
            {
                return NotFound();
            }

            return Ok(grade);
        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult GetAll()
        {
            return Ok(_gradesService.GetAll());
        }
    }
}