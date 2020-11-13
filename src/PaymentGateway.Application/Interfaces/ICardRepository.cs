using PaymentGateway.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Interfaces
{
    public interface ICardRepository: IRepository<Card>
    {
        Task<IEnumerable<Card>> GetAllCardPayments();

        Task<Card> GetCard(string cardNumber);
    }
}
