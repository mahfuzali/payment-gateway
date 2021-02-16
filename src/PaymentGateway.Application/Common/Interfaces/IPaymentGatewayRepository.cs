using System;
using System.Threading.Tasks;
using PaymentGateway.Application.Common.Models;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Application.Common.Interfaces
{
    public interface IPaymentGatewayRepository : IRepository<Payment>
    {
        Task<BankResponse> GetBankResponse(Card card, string bankEndPointUrl);

        Task<Payment> GetPayment(Guid paymentId);
    }
}
