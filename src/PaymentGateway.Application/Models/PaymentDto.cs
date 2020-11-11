using System;

namespace PaymentGateway.Application.Models
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public CardViewModel Card { get; set; }
    }
}
