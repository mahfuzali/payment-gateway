using System;

namespace PaymentGateway.Application.Common.Models
{
    public class BankResponse
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
    }
}
