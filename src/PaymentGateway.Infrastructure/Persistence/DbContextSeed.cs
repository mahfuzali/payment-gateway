using System;
using System.Linq;
using System.Threading.Tasks;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Infrastructure.Persistence
{
    public static class DbContextSeed
    {
        public static async Task SeedPaymentDataAsync(PaymentDbContext context)
        {
            var card = new Card()
            {
                Number = "0123456789101112",
                ExpiryMonth = 01,
                ExpiryYear = 2025,
                Amount = 150.75m,
                Currency = "GBP",
                CVV = "765"
            };

            var payment = new Payment()
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

        public static async Task SeedUserDataAsync(UserDbContext context)
        {
            Login login = new Login()
            {
                Id = Guid.Parse("0c86a30a-394f-49a5-85ed-3b8cbe835b7c"),
                Username = "mahfuzali",
                Password = "password123"
            };

            if (!context.Logins.Any())
            {
                context.Logins.Add(login);
            }

            await context.SaveChangesAsync();
        }
    }
}
