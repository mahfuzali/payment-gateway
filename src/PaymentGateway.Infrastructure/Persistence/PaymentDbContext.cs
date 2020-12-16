using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Infrastructure.Persistence
{
    public class PaymentDbContext : DbContext, IPaymentDbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Card>().HasKey(c => c.Number);

            modelBuilder.Entity<Payment>().HasKey(c => c.Id);

            modelBuilder.Entity<Payment>().HasOne(c => c.Card);

            base.OnModelCreating(modelBuilder);
        }

    }
}
