using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioHair.Core.Entities;

namespace StudioHair.Infrascruture.Configuration
{
    public class CarrinhoConfiguration : IEntityTypeConfiguration<Carrinho>
    {
        public void Configure(EntityTypeBuilder<Carrinho> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ClienteId)
                .IsRequired();

            builder.HasOne(x => x.Cliente)
                .WithOne(x => x.Carrinho)
                .HasForeignKey<Carrinho>(x => x.ClienteId);
        }
    }
}
