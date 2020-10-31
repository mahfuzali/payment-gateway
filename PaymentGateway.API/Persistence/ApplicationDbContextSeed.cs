using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.API.Persistence
{
    public static class ApplicationDbContextSeed
    {

        public static async Task SeedSampleDataAsync(ApplicationDbContext context) {
            // Seed, if necessary
            if (!context.Cards.Any()) {
                context.Cards.Add(new Entities.Card {
                    Number = "12345",
                    ExpiryMonth = 01,
                    ExpiryYear = 2025,
                    Amount = 100,
                    Currency = "GBP",
                    CVV = 567
                });
            }

            await context.SaveChangesAsync();
        }
    }
}
