using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.API.Entities
{
    public class Card
    {
        public string Number { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }

        public double Amount { get; set; }
        public string Currency { get; set; }
        public int CVV { get; set; }

    }
}
