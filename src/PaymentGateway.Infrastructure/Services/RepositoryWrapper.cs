using PaymentGateway.Application.Interfaces;
using PaymentGateway.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace PaymentGateway.Infrastructure.Services
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private PaymentDbContext _context;
        private IHttpClientFactory _httpClientFactory;
        private CancellationTokenSource _cancellationTokenSource;

        public RepositoryWrapper(PaymentDbContext context, IHttpClientFactory httpClientFactory) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpClientFactory = httpClientFactory ??
                    throw new ArgumentNullException(nameof(httpClientFactory));

            Cards = new CardRepository(_context);
            PaymentGateways = new PaymentGatewayRepository(_context, _httpClientFactory);

        }

        public ICardRepository Cards { get; private set; }

        public IPaymentGatewayRepository PaymentGateways { get; private set; }

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
