using PaymentGateway.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.API.Persistence
{
    public static class ApplicationDbContextSeed
    {

        public static async Task SeedSampleDataAsync(ApplicationDbContext context) {

            Card card = new Card() {
                Number = "0123456789101112",
                ExpiryMonth = 01,
                ExpiryYear = 2025,
                Amount = 150.75m,
                Currency = "GBP",
                CVV = "765"
            };

            Payment payment = new Payment()
            {
                Id = Guid.Parse("8b51ea22-adbf-46f3-9f90-cf9d9d534d45"),
                Status = "successful",
                Card = card
            };

            if (!context.Cards.Any()) 
            {
                context.Cards.Add(card);
            }

            if (!context.Payments.Any())
            {
                context.Payments.Add(payment);
            }

            await context.SaveChangesAsync();
        }
    }
}
