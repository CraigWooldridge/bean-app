using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeanApp.Domain.Repositories;
using BeanApp.Infrastructure.Repositories;
using BeanApp.Services;
using BeanApp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BeanApp.API.Configuration
{
    public static class Dependencies
    {
        public static IServiceCollection SetupDependencies(this IServiceCollection services)
        {
            services.AddScoped<IBeanService, BeanService>();
            services.AddScoped<IBeanRepository, BeanRepository>();
            services.AddScoped<IBeanImageService, BeanImageService>();
            services.AddScoped<IBeanImageRepository, BeanImageRepository>();

            return services;
        }
    }
}
