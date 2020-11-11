using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Application.Models
{
    public class CardDto
    {
        [Required]
        [RegularExpression(@"^\d{8,19}$", ErrorMessage = "Characters are not allowed.")]
        public string Number { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{1,2}\/[0-9]{4}$", ErrorMessage = "Invalid expiry data. Should be in MM/YYYY format")]
        public string Expiry { get; set; }

        [DataType(DataType.Currency)]
        [Range(0, Double.PositiveInfinity, ErrorMessage = "Amount cannot be negative")]
        [Required]
        public decimal Amount { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]{3}$", ErrorMessage = "Should be three character currency code, for example, GBP")]
        public string Currency { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "CVV should be three numeric characters")]
        public string CVV { get; set; }
    }
}
