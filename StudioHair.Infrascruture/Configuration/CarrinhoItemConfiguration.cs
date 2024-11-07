using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioHair.Core.Entities;

namespace StudioHair.Infrascruture.Configuration
{
    internal class CarrinhoItemConfiguration : IEntityTypeConfiguration<CarrinhoItem>
    {
        public void Configure(EntityTypeBuilder<CarrinhoItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Quantidade)
                .IsRequired();

            builder.Property(x => x.Valor)
                .IsRequired();

            builder.Property(x => x.ProdutoId)
                .IsRequired();

            builder.Property(x => x.CarrinhoId)
                .IsRequired();
            
            builder.HasOne(x => x.Carrinho)
                .WithMany(x => x.CarrinhoItems)
                .HasForeignKey(x => x.CarrinhoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Produto)
                .WithMany(x => x.CarrinhoItems)
                .HasForeignKey(x => x.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
