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

        void MakeCardPayment(Card card);

        Task<BankResponse> GetBankResponse(string number, int expiryMonth, int expiryYear, double amount, string currency, int cvv);

        Task<Payment> GetPayment(Guid paymentId);

        void StorePayment(Payment payment);

        Task<bool> SaveChangesAsync();
    }
}
