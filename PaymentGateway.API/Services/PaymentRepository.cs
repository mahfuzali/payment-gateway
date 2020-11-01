using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PaymentGateway.API.Entities;
using PaymentGateway.API.Models;
using PaymentGateway.API.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.API.Services
{
    public class PaymentRepository : IPaymentRepository, IDisposable
    {
        private ApplicationDbContext _context;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly IHttpClientFactory _httpClientFactory;

        public PaymentRepository(ApplicationDbContext context, IHttpClientFactory httpClientFactory) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpClientFactory = httpClientFactory ??
                throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<IEnumerable<Card>> GetAllCardPayments()
        {
            return await _context.Cards.OrderBy(o => o.Number).ToListAsync();
        }

        public async Task<Card> GetCard(string cardNumber)
        {
            if (cardNumber.Length == 0 || cardNumber == null)
            {
                throw new ArgumentNullException(nameof(cardNumber));
            }

            return await _context.Cards.Where(c => c.Number == cardNumber).FirstOrDefaultAsync();
        }

        public async Task<Payment> GetPayment(Guid paymentId)
        {
            if(paymentId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(paymentId));
            }

            return await _context.Payments.Include(c => c.Card).FirstOrDefaultAsync(p => p.Id == paymentId);
        }

        public void StoreCard(Card card)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card));
            }

            _context.Cards.Add(card);
            _context.SaveChangesAsync();
        }

        public async Task<BankResponse> GetBankResponse(Card card)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            var cardJson = new StringContent(
                JsonConvert.SerializeObject(card),
                Encoding.UTF8,
                "application/json");

            var httpClient = _httpClientFactory.CreateClient();

            var response =
                await httpClient.PostAsync("http://localhost:5001/api/FakeBank", cardJson);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BankResponse>(
                    await response.Content.ReadAsStringAsync());
            }

            return null;
        }

        public void StorePayment(Payment payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment));
            }

            _context.Payments.Add(payment);
            _context.SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
                if (_cancellationTokenSource != null)
                {
                    _cancellationTokenSource.Dispose();
                    _cancellationTokenSource = null;
                }
            }
        }

    }
}
