using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using NSubstitute;
using PaymentGateway.Application.Models;
using PaymentGateway.Domain.Entities;
using PaymentGateway.IntegrationTests.Tests;
using Xunit;

namespace PaymentGateway.IntegrationTests.Repositories
{
    public class PaymentRepositoryTests : RepositoryTestsSetup
    {
        [Fact]
        public async Task Add_CardAsync()
        {
            // Arrange
            Card card = new Card()
            {
                Number = "0123456789101112",
                ExpiryMonth = 01,
                ExpiryYear = 2025,
                Amount = 150.75m,
                Currency = "GBP",
                CVV = "765"
            };

            var mockHttpClient = new Mock<IHttpClientFactory>();
            var repository = GetRepository(mockHttpClient.Object);

            // Act
            //repository.StoreCard(card);
            //var fetchCard = await repository.GetCard(card.Number);
            repository.Cards.Add(card);
            var fetchCard = await repository.Cards.GetCard(card.Number);

            // Assert
            Assert.Equal(card, fetchCard);
        }

        [Fact]
        public async Task Add_PaymentAsync()
        {
            // Arrange
            Card card = new Card()
            {
                Number = "0123456789101112",
                ExpiryMonth = 01,
                ExpiryYear = 2025,
                Amount = 150.75m,
                Currency = "GBP",
                CVV = "765"
            };

            var mockHttpClient = new Mock<IHttpClientFactory>();
            var repository = GetRepository(mockHttpClient.Object);

            Payment payment = new Payment()
            {
                Id = Guid.Parse("8b51ea22-adbf-46f3-9f90-cf9d9d534d45"),
                Status = "successful",
                Card = card
            };

            // Act
            //repository.StorePayment(payment);
            //var fetchedPayment = await repository.GetPayment(payment.Id);
            repository.PaymentGateways.Add(payment);
            var fetchedPayment = await repository.PaymentGateways.GetPayment(payment.Id);

            // Assert
            Assert.Equal(payment, fetchedPayment);
        }

        [Fact]
        public async Task Get_BankResponse_With_Payment_Status()
        {
            // Arrange
            Card card = new Card()
            {
                Number = "0123456789101112",
                ExpiryMonth = 01,
                ExpiryYear = 2025,
                Amount = 150.75m,
                Currency = "GBP",
                CVV = "765"
            };

            var httpClientFactoryMock = Substitute.For<IHttpClientFactory>();
            var url = "http://localhost:5001/api/FakeBank";
            var fakeHttpMessageHandler = new FakeBankHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(card), Encoding.UTF8, "application/json")
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            httpClientFactoryMock.CreateClient().Returns(fakeHttpClient);

            // Act
            var service = GetRepository(httpClientFactoryMock);
            //var result = await service.GetBankResponse(card, url);
            var result = await service.PaymentGateways.GetBankResponse(card, url);

            // Assert
            result.Should().BeOfType<BankResponse>().And.NotBeNull();
        }
    }
}
