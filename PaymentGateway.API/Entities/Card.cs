using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.API.Entities
{
    public class Card
    {
        //[Required]
        //[RegularExpression(@"^\d{8,19}$",
        //    ErrorMessage = "Characters are not allowed.")]
        public string Number { get; set; }

        [Range(1, 12)]
        [Required]
        public int ExpiryMonth { get; set; }

        [Range(2000, 9999)]
        [Required]
        public int ExpiryYear { get; set; }

        //[DataType(DataType.Currency)]
        //[Required]
        public decimal Amount { get; set; }

        //[Required]
        //[RegularExpression(@"^[A-Z]{3}$")]
        public string Currency { get; set; }


        //[Required]
        //[RegularExpression(@"^\d{3}$")]
        public string CVV { get; set; }

    }
}
