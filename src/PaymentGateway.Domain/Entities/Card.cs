using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Domain.Entities
{
    public class Card
    {
        [Required]
        public string Number { get; set; }

        [Range(1, 12)]
        [Required]
        public int ExpiryMonth { get; set; }

        [Range(2000, 9999)]
        [Required]
        public int ExpiryYear { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public string CVV { get; set; }

    }
}
