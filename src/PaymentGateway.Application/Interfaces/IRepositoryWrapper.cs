using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Application.Interfaces
{
    public interface IRepositoryWrapper : IDisposable
    {
        public ICardRepository Cards { get; }
        public IPaymentGatewayRepository PaymentGateways { get; }
    }
}
