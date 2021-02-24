using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PaymentGateway.Infrastructure.Persistence;

namespace PaymentGateway.API
{
#pragma warning disable CS1591

    public class Program
    {
        public async static Task Main(string[] args)
        {
            // throttle the thread pool (set available threads to amount of processors)
            ThreadPool.SetMaxThreads(Environment.ProcessorCount, Environment.ProcessorCount);

            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var appDbContext = services.GetRequiredService<PaymentDbContext>();

                    if (appDbContext.Database.IsNpgsql())
                    {
                        appDbContext.Database.EnsureDeleted();
                        appDbContext.Database.Migrate();
                    }

                    await DbContextSeed.SeedPaymentDataAsync(appDbContext);

                    var userDbContext = services.GetRequiredService<UserDbContext>();

                    if (appDbContext.Database.IsNpgsql())
                    {
                        userDbContext.Database.EnsureDeleted();
                        userDbContext.Database.Migrate();
                    }

                    await DbContextSeed.SeedUserDataAsync(userDbContext);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");

                    throw;
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

#pragma warning restore CS1591
}
