using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioHair.Core.Entities;

namespace StudioHair.Infrascruture.Configuration
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.DataDeNascimento)
                .IsRequired();

            builder.Property(x => x.Rua)
                .IsRequired();

            builder.Property(x => x.Cep)
                .IsRequired();

            builder.Property(x => x.Cidade)
                .IsRequired();

            builder.Property(x => x.Bairro)
                .IsRequired();

            builder.Property(x => x.Número)
                .IsRequired();

            builder.HasIndex(x => x.Cpf)
                .IsUnique();

        }
    }
}
