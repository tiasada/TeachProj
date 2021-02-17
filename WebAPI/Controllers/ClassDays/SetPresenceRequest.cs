using System;

namespace WebAPI.Controllers.ClassDays
{
    public class SetPresenceRequest
    {
        public Guid StudentId { get; set; }
        public bool IsPresent { get; set; }
        public string Reason { get; set; }
    }
}