using Microsoft.EntityFrameworkCore;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Infrastructure.Persistence
{
    public class UserDbContext: DbContext, IUserDbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
                : base(options)
        {
        }

        public DbSet<Login> Logins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Login>().HasKey(l => l.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
