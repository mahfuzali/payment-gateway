﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.API.Entities
{
    public class Card
    {
        public string Number { get; set; }

        [Range(1, 12)]
        [Required]
        public int ExpiryMonth { get; set; }

        [Range(2000, 9999)]
        [Required]
        public int ExpiryYear { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string CVV { get; set; }

    }
}
