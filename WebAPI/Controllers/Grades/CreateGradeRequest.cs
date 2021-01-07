using System;

namespace WebAPI.Controllers.Grades
{
    public class CreateGradeRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public Guid ClassroomId { get; set; }
        public DateTime Date { get; set; }
    }
}