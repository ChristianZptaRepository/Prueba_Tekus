using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tekus.Application.Interfaces;
using Tekus.Domain.Interfaces;
using Tekus.Infrastructure.ExternalServices;
using Tekus.Infrastructure.Persistence;
using Tekus.Infrastructure.Repositories;

namespace Tekus.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();

            services.AddHttpClient<ICountryService, CountryService>();
            services.AddMemoryCache();

            return services;
        }
    }
}
