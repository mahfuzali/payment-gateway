using PaymentGateway.API.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PaymentGateway.API.Tests.Entities
{
    public class CardTests
    {
        [Fact]
        public void Is_Card_NotNull()
        {
            // Arrange
            var card = new Card();

            // Act
            card.Number = "0123456789101112";
            card.ExpiryMonth = 01;
            card.ExpiryYear = 2025;
            card.Amount = 150.75m;
            card.Currency = "GBP";
            card.CVV = "765";

            // Assert
            Assert.NotNull(card);
        }

        [Fact]
        public void Is_Card_Values_Expected_Type()
        {
            // Arrange
            var card = new Card();

            // Act
            card.Number = "0123456789101112";
            card.ExpiryMonth = 01;
            card.ExpiryYear = 2025;
            card.Amount = 150.75m;
            card.Currency = "GBP";
            card.CVV = "765";

            // Assert
            Assert.IsType<string>(card.Number);
            Assert.IsType<int>(card.ExpiryMonth);
            Assert.IsType<int>(card.ExpiryYear);
            Assert.IsType<decimal>(card.Amount);
            Assert.IsType<string>(card.Currency);
            Assert.IsType<string>(card.CVV);

        }

    }
}
