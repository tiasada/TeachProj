using System;
using Microsoft.AspNetCore.Mvc;
using Domain.Teachers;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.IO;

namespace WebAPI.Controllers.Teachers
{
    [ApiController]
    [Route("[controller]")]
    public class TeachersController : ControllerBase
    {
        public readonly ITeachersService _teachersService;
        public readonly IUsersService _usersService;
        public TeachersController(IUsersService usersService, ITeachersService teachersService)
        {
            _teachersService = teachersService;
            _usersService = usersService;
        }
        
        [HttpPost]
        [Authorize(Roles = "School")]
        public IActionResult Post(CreateTeacherRequest request)
        {
            var ms = new MemoryStream();
            request.Picture.CopyTo(ms);
            var picture = ms.ToArray();
            ms.Dispose();
            
            var response = _teachersService.Create(request.Name, request.CPF, request.PhoneNumber, request.BirthDate, picture);

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
            var teacherRemoved = _teachersService.Remove(id);

            if (!teacherRemoved)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]

        public IActionResult GetByID(Guid id)
        {
            var teacher = _teachersService.Get(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult GetAll(string name)
        {
            var teachers = _teachersService.GetAll();
            
            if (!string.IsNullOrWhiteSpace(name))
            {
                var transformedName = name.ToLower().Trim();
                teachers = teachers.Where(x => x.Name.ToLower().Contains(transformedName));
            }
            
            return Ok(teachers.OrderBy(x => x.Name));
        }
    }
}