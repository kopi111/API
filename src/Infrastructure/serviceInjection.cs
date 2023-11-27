
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utils;
using Application.dbConnection;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.serviceInjection
{
    public static class InfrastructureServicesInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
    
         var connectionStringsConfigSection = configuration.GetSection(StringConstants.CONNECTIONSTRINGSCONFIGSECTION);

            var connectionStringOptions = new connectionStringDetails();
            connectionStringsConfigSection.Bind(connectionStringOptions);

            services.Configure<connectionStringDetails>(connectionStringsConfigSection);

            return services;
        }
    }
}
