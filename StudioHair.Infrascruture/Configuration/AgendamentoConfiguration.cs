using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StudioHair.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioHair.Infrascruture.Configuration
{
    public class AgendamentoConfiguration : IEntityTypeConfiguration<Agendamento>
    {
        public void Configure(EntityTypeBuilder<Agendamento> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired();

            builder.Property(x => x.Dia)
                .IsRequired();

            builder.Property(x => x.HoraInicial)
                 .IsRequired();

            builder.Property(x => x.HoraFinal)
                 .IsRequired();

            builder.Property(x => x.ValorProfissional)
                .IsRequired();

            builder.Property(x => x.ValorAgendamento);

            builder.Property(x => x.ValorDesconto);
              
            builder.Property(x => x.ClienteId)
               .IsRequired();

            builder.Property(x => x.Status)
              .IsRequired();
        }

    }
}
