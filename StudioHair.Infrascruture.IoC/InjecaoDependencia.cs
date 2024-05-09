using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudioHair.Application.Services.Implementations;
using StudioHair.Application.Services.Interfaces;
using StudioHair.Core.Interfaces;
using StudioHair.Infrascruture.Context;
using StudioHair.Infrascruture.Repositories;

namespace StudioHair.Infrascruture.IoC
{
    public static class InjecaoDependencia
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IServicoService, ServicoService>();
            services.AddScoped<IVendaService, VendaService>();
            services.AddScoped<IAgendamentoService, AgendamentoService>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // Repositories
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IServicoRepository, ServicoRepository>();
            services.AddScoped<IVendaRepository, VendaRepository>();
            services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();

            return services;
        }
    }
}
