using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioHair.Core.Entities;

namespace StudioHair.Infrascruture.Configuration
{
    public class HistoricoClienteConfiguration : IEntityTypeConfiguration<HistoricoCliente>
    {
        public void Configure(EntityTypeBuilder<HistoricoCliente> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Dia)
                .IsRequired()

            builder.Property(x => x.NomeAgendamento)
                .IsRequired()

            builder.Property(x => x.ValorAgendamento)
                .IsRequired()

            builder.Property(x => x.StatusAgendamento)
                .IsRequired()
        }
    }
}
