using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Infrastructure.Persistence;
using PaymentGateway.Infrastructure.Services;
using System.Net.Http;

namespace PaymentGateway.IntegrationTests.Repositories
{
    public abstract class RepositoryTestsSetup
    {
        protected ApplicationDbContext _dbContext;

        protected static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase("PaymentDb")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected PaymentRepository GetRepository(IHttpClientFactory httpClientFactory)
        {
            var options = CreateNewContextOptions();

            _dbContext = new ApplicationDbContext(options);

            return new PaymentRepository(_dbContext, httpClientFactory);
        }
    }
}
