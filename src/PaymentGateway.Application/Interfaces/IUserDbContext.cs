using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Interfaces
{
    public interface IUserDbContext
    {
        public DbSet<Login> Logins { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
