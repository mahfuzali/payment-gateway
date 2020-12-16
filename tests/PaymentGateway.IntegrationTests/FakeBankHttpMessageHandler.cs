using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.IntegrationTests.Tests
{
    public class FakeBankHttpMessageHandler : DelegatingHandler
    {
        private HttpResponseMessage _fakeResponse;

        public FakeBankHttpMessageHandler(HttpResponseMessage responseMessage)
        {
            _fakeResponse = responseMessage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_fakeResponse);
        }
    }
}
