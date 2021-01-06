using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain.ClassDays;
using Domain.Users;
using Microsoft.Extensions.Primitives;

namespace WebAPI.Controllers.ClassDays
{
    [ApiController]
    [Route("[controller]")]
    public class ClassDaysController : ControllerBase
    {
        public readonly IClassDaysService _classDaysService;
        public readonly IUsersService _usersService;
        public ClassDaysController(IUsersService usersService, IClassDaysService classDaysService)
        {
            _classDaysService = classDaysService;
            _usersService = usersService;
        }
        
        [HttpPost]
        public IActionResult Post(CreateClassDayRequest request)
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

            var response = _classDaysService.Create(request.Date, request.ClassroomId, request.Notes);

            if (!response.IsValid)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Id);
        }

        [HttpPatch("{id}/setpresence/{studentId}")]
        public IActionResult SetPresence(Guid id, Guid studentId, SetPresenceRequest request)
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

            var presenceSet = _classDaysService.SetPresence(id, studentId, request.IsPresent, request.Reason);

            if (presenceSet != null)
            {
                return BadRequest(presenceSet);
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

            var classDayRemoved = _classDaysService.Remove(id);

            if (classDayRemoved == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {
            var classDay = _classDaysService.Get(x => x.Id == id);

            if (classDay == null)
            {
                return NotFound();
            }

            return Ok(classDay);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_classDaysService.GetAll());
        }
    }
}