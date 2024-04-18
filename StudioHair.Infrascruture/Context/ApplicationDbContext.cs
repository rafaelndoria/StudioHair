using Microsoft.EntityFrameworkCore;
using StudioHair.Core.Entities;

namespace StudioHair.Infrascruture.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<AgendamentoServicos> AgendamentoServicos { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<HistoricoCliente> HistoricoClientes { get; set; }
        public DbSet<HistoricoComprasClientes> HistoricoComprasClientes { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<ProdutosServico> ProdutosServicos { get; set; }
        public DbSet<ProdutosVenda> ProdutosVendas { get; set; }
        public DbSet<ProdutoUnidade> ProdutoUnidades { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Venda> Vendas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
