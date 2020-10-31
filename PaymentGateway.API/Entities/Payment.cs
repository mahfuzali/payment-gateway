using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.API.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public Card Card { get; set; }
        
    }
}
