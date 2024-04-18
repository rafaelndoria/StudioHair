using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioHair.Core.Entities;

namespace StudioHair.Infrascruture.Configuration
{
    public class ServicoConfiguration : IEntityTypeConfiguration<Servico>
    {
        public void Configure(EntityTypeBuilder<Servico> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.DuracaoEmMinutos)
                .IsRequired();

            builder.Property(x => x.ValorServico)
                .IsRequired();

            builder.Property(x => x.ValorDosProdutos);

            builder.Property(x => x.ValorTotal);
        }
    }
}
