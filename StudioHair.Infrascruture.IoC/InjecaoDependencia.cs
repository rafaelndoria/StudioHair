using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudioHair.Application.Services.Implementations;
using StudioHair.Application.Services.Interfaces;
using StudioHair.Infrascruture.Context;

namespace StudioHair.Infrascruture.IoC
{
    public static class InjecaoDependencia
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            // Services
            services.AddScoped<IAuthService, AuthService>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            return services;
        }
    }
}
