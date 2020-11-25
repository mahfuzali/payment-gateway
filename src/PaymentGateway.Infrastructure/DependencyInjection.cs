using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Infrastructure.Persistence;
using PaymentGateway.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<PaymentDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("PaymentDbConnection"),
                        b => b.MigrationsAssembly(typeof(PaymentDbContext).Assembly.FullName)));

            services.AddDbContext<UserDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("UserDbConnection"),
                        b => b.MigrationsAssembly(typeof(UserDbContext).Assembly.FullName)));

            
            services.AddScoped<IPaymentDbContext>(provider => provider.GetService<PaymentDbContext>());
            services.AddScoped<IUserDbContext>(provider => provider.GetService<UserDbContext>());

            services.AddTransient<ITokenService, TokenService>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            return services;
        }
    }
}
