using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudioHair.Application.Services.Implementations;
using StudioHair.Application.Services.Interfaces;

namespace StudioHair.Infrascruture.IoC
{
    public static class InjecaoDependencia
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            // Services
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
