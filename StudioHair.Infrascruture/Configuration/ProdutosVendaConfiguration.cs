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

            builder.Property(x => x.ProdutoId);
                .IsRequired()

            builder.Property(x => x.Valor);
                .IsRequired()

            builder.Property(x => x.VendaId);
                .IsRequired()
        }
    }
}
