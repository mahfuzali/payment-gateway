using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PaymentGateway.API.Entities;
using PaymentGateway.API.Models;
using PaymentGateway.API.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.API.Services
{
    public class CardRepository : ICardRepository, IDisposable
    {
        private ApplicationDbContext _context;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly IHttpClientFactory _httpClientFactory;

        public CardRepository(ApplicationDbContext context /*, IHttpClientFactory httpClientFactory*/) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            //_httpClientFactory = httpClientFactory ??
            //    throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<IEnumerable<Card>> GetAllCardPayments()
        {
            return await _context.Cards.OrderBy(o => o.Number).ToListAsync();
        }

        public async Task<Card> GetPayment(string cardNumber)
        {
            if (cardNumber.Length == 0 || cardNumber == null)
            {
                throw new ArgumentNullException(nameof(cardNumber));
            }

            return await _context.Cards.Where(c => c.Number == cardNumber).FirstOrDefaultAsync();
        }

        public void MakeCardPayment(Card card)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card));
            }

            _context.Cards.Add(card);
        }

        //public async Task<BankResponse> GetBankResponse(string number, int expiryMonth, int expiryYear, double amount, string currency, int cvv)
        //{
        //    var httpClient = _httpClientFactory.CreateClient();

        //    var response = await httpClient
        //                .GetAsync($"http://localhost:5001/api/FakeBank?number={number}&expiryMonth={expiryMonth}&expiryYear={expiryYear}&amount={amount}&currency={currency}&cvv={cvv}");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return JsonConvert.DeserializeObject<BankResponse>(
        //            await response.Content.ReadAsStringAsync());
        //    }

        //    return null;
        //}

        //public void StorePayment(Payment payment)
        //{
        //    throw new NotImplementedException();
        //}

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
