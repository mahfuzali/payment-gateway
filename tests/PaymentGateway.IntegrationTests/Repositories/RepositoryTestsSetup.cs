using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Infrastructure.Persistence;
using PaymentGateway.Infrastructure.Services;

namespace PaymentGateway.IntegrationTests.Repositories
{
    public abstract class RepositoryTestsSetup
    {
        protected PaymentDbContext _paymentDbContext;

        protected UserDbContext _userDbContext;

        protected static DbContextOptions<PaymentDbContext> CreatePaymentContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<PaymentDbContext>();
            builder.UseInMemoryDatabase("PaymentDb")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected static DbContextOptions<UserDbContext> CreateUserContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<UserDbContext>();
            builder.UseInMemoryDatabase("PaymentDb")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected RepositoryWrapper GetRepository(IHttpClientFactory httpClientFactory)
        {
            var paymentOptions = CreatePaymentContextOptions();

            var userOptions = CreateUserContextOptions();

            _paymentDbContext = new PaymentDbContext(paymentOptions);
            _userDbContext = new UserDbContext(userOptions);

            return new RepositoryWrapper(_paymentDbContext, _userDbContext, httpClientFactory);
        }
    }
}
