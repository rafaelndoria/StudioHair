using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioHair.Core.Entities;

namespace StudioHair.Infrascruture.Configuration
{
    public class VendaConfiguration : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DataDaVenda)
                .IsRequired();

            builder.Property(x => x.TipoPagamento)
                .IsRequired();

            builder.Property(x => x.ValorDesconto);

            builder.Property(x => x.Total);

            builder.Property(x => x.ClienteId)
                .IsRequired();

            builder.HasOne(x => x.Cliente)
                .WithMany(x => x.Vendas)
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
