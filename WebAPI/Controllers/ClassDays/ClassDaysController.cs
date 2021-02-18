using System;
using Microsoft.AspNetCore.Mvc;
using Domain.ClassDays;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

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

        [HttpDelete("{id}")]
        [Authorize(Roles = "Teacher")]
        public IActionResult Remove(Guid id)
        {
            var classDayRemoved = _classDaysService.Remove(id);

            if (!classDayRemoved)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetByID(Guid id)
        {
            var classDay = _classDaysService.Get(id);

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