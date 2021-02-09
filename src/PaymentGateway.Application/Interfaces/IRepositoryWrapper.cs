using System;

namespace PaymentGateway.Application.Interfaces
{
    public interface IRepositoryWrapper : IDisposable
    {
        public ICardRepository Cards { get; }
        public IPaymentGatewayRepository PaymentGateways { get; }
        public IUserRepository Users { get; }
    }
}
