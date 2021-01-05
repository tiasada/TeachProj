using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain.Grades;
using Domain.Users;
using Microsoft.Extensions.Primitives;

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
        public IActionResult Post(CreateGradeRequest request)
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
            
            if (user.Profile != Profile.Teacher)
            {
                return StatusCode(403, "User is not Teacher");
            }

            var response = _gradesService.Create(request.Name, request.Description, DateTime.Now, request.ClassroomId);

            if (!response.IsValid)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Id);
        }

        [HttpPatch("{id}/setgrade/{studentId}")]
        public IActionResult SetGrade(Guid id, Guid studentId, [FromBody]double value)
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

            if (user.Profile != Profile.Teacher)
            {
                return StatusCode(403, "User is not Teacher");
            }

            var gradeSet = _gradesService.SetGrade(id, studentId, value);

            if (gradeSet != null)
            {
                return BadRequest(gradeSet);
            }

            return NoContent();
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

            if (user.Profile != Profile.Teacher)
            {
                return StatusCode(403, "User is not Teacher");
            }

            var gradeRemoved = _gradesService.Remove(id);

            if (gradeRemoved == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {
            var grade = _gradesService.Get(x => x.Id == id);

            if (grade == null)
            {
                return NotFound();
            }

            return Ok(grade);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_gradesService.GetAll());
        }
    }
}