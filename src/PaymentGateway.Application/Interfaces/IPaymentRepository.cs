using PaymentGateway.Application.Models;
using PaymentGateway.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Interfaces
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
