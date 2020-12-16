

using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PaymentGateway.API;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Infrastructure.Persistence;
using PaymentGateway.Infrastructure.Services;

namespace PaymentGateway.FunctionalTests.Controllers
{
    public class ApiWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(async services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                // Add a database context (AppDbContext) using an in-memory
                // database for testing.
                services.AddDbContext<PaymentDbContext>(options =>
                {
                    options.UseInMemoryDatabase("PaymentDb");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                //services.AddTransient<IPaymentRepository, PaymentRepository>();
                services.AddTransient<IRepositoryWrapper, RepositoryWrapper>();

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context (AppDbContext).
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<PaymentDbContext>();

                    var logger = scopedServices
                        .GetRequiredService<ILogger<ApiWebApplicationFactory<TStartup>>>();

                    // Ensure the database is created.
                    //context.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with test data.
                        await DbContextSeed.SeedPaymentDataAsync(context);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            $"database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }
    }
}
