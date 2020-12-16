using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using PaymentGateway.API;
using Xunit;

namespace PaymentGateway.FunctionalTests.Controllers
{
    public class IntegrationTest : IClassFixture<ApiWebApplicationFactory<Startup>>
    {
        protected readonly ApiWebApplicationFactory<Startup> _factory;
        protected readonly HttpClient _client;

        public IntegrationTest(ApiWebApplicationFactory<Startup> fixture)
        {
            _factory = fixture;
            _client = fixture.CreateClient();
        }
    }
}
