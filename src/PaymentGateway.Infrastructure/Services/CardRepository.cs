using Microsoft.EntityFrameworkCore;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Infrastructure.Services
{
    public class CardRepository : Repository<Card>, ICardRepository
    {
        public CardRepository(ApplicationDbContext context) 
            : base(context)
        { 
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return _context as ApplicationDbContext; }
        }

        public async Task<IEnumerable<Card>> GetAllCardPayments()
        {
            return await ApplicationDbContext.Cards.OrderBy(o => o.Number).ToListAsync();

        }

        public async Task<Card> GetCard(string cardNumber)
        {
            if (cardNumber.Length == 0 || cardNumber == null)
            {
                throw new ArgumentNullException(nameof(cardNumber));
            }

            return await ApplicationDbContext.Cards.Where(c => c.Number == cardNumber).FirstOrDefaultAsync();
        }
    }
}
