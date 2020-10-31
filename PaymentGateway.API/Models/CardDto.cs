using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.API.Models
{
    public class CardDto
    {
        public string Number { get; set; }
        public string Expiry { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public int CVV { get; set; }
    }
}
