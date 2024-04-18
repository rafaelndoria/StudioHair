using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioHair.Core.Entities;

namespace StudioHair.Infrascruture.Configuration
{
    internal class HistoricoComprasClienteConfiguration : IEntityTypeConfiguration<HistoricoComprasClientes>
    {
        public void Configure(EntityTypeBuilder<HistoricoComprasClientes> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Dia)
                .IsRequired();

            builder.Property(x => x.ClienteId)
                .IsRequired();

            builder.Property(x => x.Valor)
                .IsRequired();
        }
    }
}
