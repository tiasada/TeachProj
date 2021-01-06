using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain.ClassDays;
using Domain.Users;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "Teacher")]
        public IActionResult Post(CreateClassDayRequest request)
        {
            var response = _classDaysService.Create(request.Date, request.ClassroomId, request.Notes);

            if (!response.IsValid)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Id);
        }

        [HttpPatch("{id}/setpresence/{studentId}")]
        [Authorize(Roles = "Teacher")]
        public IActionResult SetPresence(Guid id, Guid studentId, SetPresenceRequest request)
        {
            var presenceSet = _classDaysService.SetPresence(id, studentId, request.IsPresent, request.Reason);

            if (presenceSet != null)
            {
                return BadRequest(presenceSet);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Teacher")]
        public IActionResult Remove(Guid id)
        {
            var classDayRemoved = _classDaysService.Remove(id);

            if (classDayRemoved == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            return Ok(_classDaysService.GetAll());
        }
    }
}