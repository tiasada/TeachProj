using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain.Teachers;
using Domain.Users;
using Microsoft.Extensions.Primitives;

namespace WebAPI.Controllers.Teachers
{
    [ApiController]
    [Route("[controller]")]
    public class TeachersController : ControllerBase
    {
        public readonly TeachersService _teachersService;
        public readonly UsersService _usersService;
        public TeachersController()
        {
            _teachersService = new TeachersService();
            _usersService = new UsersService();
        }
        
        [HttpPost]
        public IActionResult Post(CreateTeacherRequest request)
        {
            StringValues headerId;
            var foundId = Request.Headers.TryGetValue("UserId", out headerId);
            if (!foundId) { return Unauthorized("User ID must be informed"); }

            var validId = Guid.TryParse(headerId, out var userId);
            if (!validId) { return Unauthorized("Invalid ID"); }
            
            var user = _usersService.GetByID(userId);

            if (user == null)
            {
                return Unauthorized("User does not exist");
            }
            
            if (user.Profile != Profile.Admin)
            {
                return StatusCode(403, "User is not Admin");
            }

            var response = _teachersService.Create(request.Name, request.CPF);

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

            var user = _usersService.GetByID(userId);

            if (user == null)
            {
                return Unauthorized();
            }

            if (user.Profile != Profile.Admin)
            {
                return StatusCode(403, "User is not Admin");
            }

            var teacherRemoved = _teachersService.Remove(id);

            if (teacherRemoved == null)
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

            var user = _usersService.GetByID(userId);

            if (user == null)
            {
                return Unauthorized();
            }

            if (user.Profile != Profile.Admin)
            {
                return StatusCode(403, "User is not Admin");
            }

            var teacherAdded = _teachersService.AddClass(id, classId);

            if (teacherAdded != null)
            {
                return BadRequest(teacherAdded);
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {
            var teacher = _teachersService.GetByID(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_teachersService.GetAll());
        }
    }
}