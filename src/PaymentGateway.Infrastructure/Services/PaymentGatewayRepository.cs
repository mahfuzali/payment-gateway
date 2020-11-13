using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Application.Models;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Infrastructure.Services
{
    public class PaymentGatewayRepository: Repository<Payment>, IPaymentGatewayRepository 
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PaymentGatewayRepository(ApplicationDbContext context, IHttpClientFactory httpClientFactory)
            : base(context)
        {
            _httpClientFactory = httpClientFactory ??
                    throw new ArgumentNullException(nameof(httpClientFactory));

        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return _context as ApplicationDbContext; }
        }

        public async Task<BankResponse> GetBankResponse(Card card, string bankEndPointUrl)
        {
            var cardJson = new StringContent(
                JsonConvert.SerializeObject(card),
                Encoding.UTF8,
                "application/json");

            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.PostAsync(bankEndPointUrl, cardJson);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BankResponse>(
                    await response.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task<Payment> GetPayment(Guid paymentId)
        {
            if (paymentId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(paymentId));
            }

            return await ApplicationDbContext.Payments.Include(c => c.Card).FirstOrDefaultAsync(p => p.Id == paymentId);

        }
    }
}
