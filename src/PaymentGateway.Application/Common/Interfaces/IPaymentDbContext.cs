using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Application.Common.Interfaces
{
    public interface IPaymentDbContext
    {
        public DbSet<Card> Cards { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
