using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioHair.Core.Entities;

namespace StudioHair.Infrascruture.Configuration
{
    public class ProdutosVendaConfiguration : IEntityTypeConfiguration<ProdutosVenda>
    {
        public void Configure(EntityTypeBuilder<ProdutosVenda> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProdutoId)
                .IsRequired();

            builder.Property(x => x.Valor)
                .IsRequired();

            builder.Property(x => x.VendaId)
                .IsRequired();

            builder.HasOne(x => x.Venda)
                .WithMany(x => x.ProdutosVendas)
                .HasForeignKey(x => x.VendaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Produto)
                .WithMany(x => x.ProdutosVendas)
                .HasForeignKey(x => x.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
