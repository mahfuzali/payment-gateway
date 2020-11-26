using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Interfaces
{
    public interface IPaymentDbContext
    {
        public DbSet<Card> Cards { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
