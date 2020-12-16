using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Infrastructure.Persistence;

namespace PaymentGateway.Infrastructure.Services
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private PaymentDbContext _paymentContext;
        private UserDbContext _userDbContext;

        private IHttpClientFactory _httpClientFactory;
        private CancellationTokenSource _cancellationTokenSource;

        public RepositoryWrapper(PaymentDbContext paymentContext, UserDbContext userDbContext, IHttpClientFactory httpClientFactory)
        {
            _paymentContext = paymentContext ?? throw new ArgumentNullException(nameof(paymentContext));
            _userDbContext = userDbContext ?? throw new ArgumentNullException(nameof(userDbContext));

            _httpClientFactory = httpClientFactory ??
                    throw new ArgumentNullException(nameof(httpClientFactory));

            Users = new UserRepository(_userDbContext);

            Cards = new CardRepository(_paymentContext);

            PaymentGateways = new PaymentGatewayRepository(_paymentContext, _httpClientFactory);
        }

        public ICardRepository Cards { get; private set; }

        public IPaymentGatewayRepository PaymentGateways { get; private set; }

        public IUserRepository Users { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_paymentContext != null)
                {
                    _paymentContext.Dispose();
                    _paymentContext = null;
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
