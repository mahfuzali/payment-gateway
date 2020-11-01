using PaymentGateway.API.Entities;
using PaymentGateway.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.API.Services
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Card>> GetAllCardPayments();

        Task<Card> GetCard(string cardNumber);

        void StoreCard(Card card);

        Task<BankResponse> GetBankResponse(Card card, string bankEndPointUrl);

        Task<Payment> GetPayment(Guid paymentId);

        void StorePayment(Payment payment);

        Task<bool> SaveChangesAsync();
    }
}
