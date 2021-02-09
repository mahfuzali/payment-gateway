using System;

namespace PaymentGateway.Domain.Entities
{
    public class Login
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
