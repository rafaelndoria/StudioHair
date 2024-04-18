using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioHair.Core.Entities;

namespace StudioHair.Infrascruture.Configuration
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.TelefoneCelular)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(x => x.Whatsapp);

            builder.Property(x => x.Facebook);

            builder.Property(x => x.FrequenciaSalaoPorMes);

            builder.Property(x => x.DataCadastro)
                .IsRequired();

            builder.Property(x => x.Observacao);

            builder.Property(x => x.Ativo)
                .IsRequired();

            builder.Property(x => x.PessoaId)
                .IsRequired();

            builder.HasOne(x => x.Pessoa)
                .WithOne(x => x.Cliente)
                .HasForeignKey<Cliente>(x => x.PessoaId);

        }
    }
}
