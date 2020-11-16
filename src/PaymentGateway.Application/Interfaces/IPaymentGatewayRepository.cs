using PaymentGateway.Application.Models;
using PaymentGateway.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Interfaces
{
    public interface IPaymentGatewayRepository : IRepository<Payment>
    {
        Task<BankResponse> GetBankResponse(Card card, string bankEndPointUrl);

        Task<Payment> GetPayment(Guid paymentId);
    }
}
