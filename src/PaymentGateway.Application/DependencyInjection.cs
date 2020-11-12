﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace PaymentGateway.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) {

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
