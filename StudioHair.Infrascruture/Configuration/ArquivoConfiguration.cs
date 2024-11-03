using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioHair.Core.Entities;

namespace StudioHair.Infrascruture.Configuration
{
    public class ArquivoConfiguration : IEntityTypeConfiguration<Arquivo>
    {
        public void Configure(EntityTypeBuilder<Arquivo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.NomeArquivo)
                .IsRequired();

            builder.Property(x => x.Caminho)
                .IsRequired();

            builder.Property(x => x.DataUpload)
                .IsRequired();

            builder.HasOne(x => x.Produto)
                .WithMany(x => x.Arquivos)
                .HasForeignKey(x => x.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
