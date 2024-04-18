using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioHair.Core.Entities;

namespace StudioHair.Infrascruture.Configuration
{
    public class ProdutoUnidadeConfiguration : IEntityTypeConfiguration<ProdutoUnidade>
    {
        public void Configure(EntityTypeBuilder<ProdutoUnidade> builder)
        {
            builder.Property(x => x.Unidade)
                .IsRequired();

            builder.Property(x => x.Quantidade)
                .IsRequired();

            builder.Property(x => x.ProdutoId)
                .IsRequired();

            builder.HasOne(x => x.Produto)
                .WithMany(x => x.ProdutoUnidades)
                .HasForeignKey(x => x.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
