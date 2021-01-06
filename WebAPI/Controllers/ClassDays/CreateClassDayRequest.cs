using System;

namespace WebAPI.Controllers.ClassDays
{
    public class CreateClassDayRequest
    {
        public DateTime Date { get; set; }
        public Guid ClassroomId { get; set; }
        public string Notes { get; set; }
    }
}