using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Application.Interfaces
{
    public interface IUserDbContext
    {
        public DbSet<Login> Logins { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
