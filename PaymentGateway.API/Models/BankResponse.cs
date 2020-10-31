using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.API.Models
{
    public class BankResponse
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
    }
}
