using PaymentGateway.API;
using PaymentGateway.FunctionalTests.Controllers;

namespace PaymentGateway.FunctionalTests.Tests
{
    public class PaymentControllersTests : IntegrationTest
    {
        public PaymentControllersTests(ApiWebApplicationFactory<Startup> fixture)
            : base(fixture)
        {
        }

        //[Fact]
        //public async Task Get_Should_Retrieve_Payment()
        //{
        //    // Arrange
        //    var guid = Guid.Parse("8b51ea22-adbf-46f3-9f90-cf9d9d534d45");

        //    // Act
        //    var response = await _client.GetAsync($"/api/payment/{guid}");
        //    response.StatusCode.Should().Be(HttpStatusCode.OK);

        //    var payment = JsonConvert.DeserializeObject<PaymentDto>(
        //      await response.Content.ReadAsStringAsync()
        //    );

        //    // Assert
        //    payment.Should().NotBeNull();
        //    payment.Id.Should().Be(guid);
        //    payment.Status.Should().Be("successful");
        //}
    }
}
