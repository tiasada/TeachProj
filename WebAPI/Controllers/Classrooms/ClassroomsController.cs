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
        public readonly ClassroomsService _classroomsService;
        public readonly UsersService _usersService;
        public ClassroomsController()
        {
            _classroomsService = new ClassroomsService();
            _usersService = new UsersService();
        }
        
        [HttpPost]
        public IActionResult Post(CreateClassroomRequest request)
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

            var user = _usersService.GetByID(userId);

            if (user == null)
            {
                return Unauthorized();
            }

            if (user.Profile != Profile.Admin)
            {
                return StatusCode(403, "User is not Admin");
            }

            var classroomRemoved = _classroomsService.Remove(id);

            if (classroomRemoved == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {
            var classroom = _classroomsService.GetByID(id);

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