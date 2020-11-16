using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain.Entities;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Threading;
using PaymentGateway.Application.Interfaces;

namespace PaymentGateway.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Card>().HasKey(c => c.Number);
            
            modelBuilder.Entity<Payment>().HasKey(c => c.Id);

            modelBuilder.Entity<Payment>().HasOne(c => c.Card);

            base.OnModelCreating(modelBuilder);
        }

    }
}
