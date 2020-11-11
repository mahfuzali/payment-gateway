using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain.Entities;
using System.Reflection;

namespace PaymentGateway.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Card>().HasKey(c => c.Number);
            
            modelBuilder.Entity<Payment>().HasKey(c => c.Id);

            modelBuilder.Entity<Payment>().HasOne(c => c.Card);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
