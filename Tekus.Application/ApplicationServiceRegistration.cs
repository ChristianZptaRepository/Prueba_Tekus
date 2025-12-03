using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Tekus.Application.Features.Dashboard;
using Tekus.Application.Features.Providers;
using Tekus.Application.Features.Services;
using Tekus.Application.Mappings;

namespace Tekus.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<MappingProfile>();
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<CreateProviderHandler>();
            services.AddScoped<CreateServiceHandler>();
            services.AddScoped<GetAllProvidersHandler>();
            services.AddScoped<GetAllServicesHandler>();
            services.AddScoped<GetDashboardStatsHandler>();

            return services;
        }
    }
}
