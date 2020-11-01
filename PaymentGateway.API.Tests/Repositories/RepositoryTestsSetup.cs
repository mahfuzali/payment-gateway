﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PaymentGateway.API.Persistence;
using PaymentGateway.API.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace PaymentGateway.API.Tests.Repositories
{
    public abstract class RepositoryTestsSetup
    {
        protected ApplicationDbContext _dbContext;

        protected static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase("PaymentDb")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        //protected PaymentRepository GetRepository()
        //{
        //    var options = CreateNewContextOptions();

        //    _dbContext = new ApplicationDbContext(options);

        //    var mockHttpClient = new Mock<IHttpClientFactory>();
        //    //var mockConfiguration = new Mock<IConfiguration>();

        //    return new PaymentRepository(_dbContext, mockHttpClient.Object /*, mockConfiguration.Object*/ );
        //}

        protected PaymentRepository GetRepository(IHttpClientFactory httpClientFactory)
        {
            var options = CreateNewContextOptions();

            _dbContext = new ApplicationDbContext(options);

            //var mockHttpClient = new Mock<IHttpClientFactory>();
            //var mockConfiguration = new Mock<IConfiguration>();

            return new PaymentRepository(_dbContext, httpClientFactory);
        }
    }
}
