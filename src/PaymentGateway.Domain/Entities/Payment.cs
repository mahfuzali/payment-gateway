using System;

namespace PaymentGateway.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public Card Card { get; set; }
        
    }
}
