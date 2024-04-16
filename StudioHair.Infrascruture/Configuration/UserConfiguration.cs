using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioHair.Core.Entities;

namespace StudioHair.Infrascruture.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Papel)
                .IsRequired();

            builder.Property(x => x.Senha)
                .IsRequired()
                .HasMaxLength(300);

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.HasIndex(x => x.Nome)
                .IsUnique();
        }
    }
}
