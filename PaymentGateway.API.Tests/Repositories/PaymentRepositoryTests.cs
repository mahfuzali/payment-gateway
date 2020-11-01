using PaymentGateway.API.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PaymentGateway.API.Tests.Repositories
{
    public class PaymentRepositoryTests: RepositoryTestsSetup
    {
        [Fact]
        public async Task Add_CardAsync() {
            var repository = GetRepository();
            
            Card card = new Card()
            {
                Number = "0123456789101112",
                ExpiryMonth = 01,
                ExpiryYear = 2025,
                Amount = 150.75m,
                Currency = "GBP",
                CVV = "765"
            };

            repository.StoreCard(card);
            var fetchCard = await repository.GetCard(card.Number); 

            Assert.Equal(card, fetchCard);
        }

        [Fact]
        public async Task Add_PaymentAsync()
        {
            var repository = GetRepository();

            Card card = new Card()
            {
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

            repository.StorePayment(payment);
            var fetchpayment = await repository.GetPayment(payment.Id);

            Assert.Equal(payment, fetchpayment);
        }

    }
}
