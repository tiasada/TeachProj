using System;

namespace Domain.Auth
{
    public class AuthResponse
    {
        public Guid UserId { get; set; }
        public bool IsValid { get; set; } = true;

        public AuthResponse(Guid id)
        {
            UserId = id;
        }

        public AuthResponse()
        {
            IsValid = false;
        }
    }
}