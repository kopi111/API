using Application.interfaces;
using Application.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.serviceInjection
{
    public static class InfrastructureServicesInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ICrimeAPIService, CrimeAPIService>();
        

            return services;
        }
    }
}
