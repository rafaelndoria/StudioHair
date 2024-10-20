using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioHair.Core.Entities;

namespace StudioHair.Infrascruture.Configuration
{
    public class ConfiguracaoSistemaConfiguration : IEntityTypeConfiguration<ConfiguracaoSistema>
    {
        public void Configure(EntityTypeBuilder<ConfiguracaoSistema> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CorPrimaria)
                .HasDefaultValue("#ffc0cb")
               .IsRequired(false);

            builder.Property(x => x.CorSecundaria)
                .HasDefaultValue("#f8f9fa")
                .IsRequired(false);

            builder.Property(x => x.CorFonte)
                .HasDefaultValue("#212529")
                .IsRequired(false);

            builder.Property(x => x.TamanhoFonte)
                .HasDefaultValue(16);

            builder.Property(x => x.TemaDark)
                .HasDefaultValue(false);

            builder.Property(x => x.UsuarioId)
                .IsRequired();

            builder.HasOne(x => x.Usuario)
                .WithOne(x => x.ConfiguracaoSistema)
                .HasForeignKey<ConfiguracaoSistema>(x => x.UsuarioId);
        }
    }
}
