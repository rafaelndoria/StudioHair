using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioHair.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace StudioHair.Infrascruture.Configuration
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired ();

            builder.Property(x => x.Marca)
                .IsRequired();

            builder.Property(x => x.CodigoBarras)
                 .IsRequired();

            builder.Property(x => x.ValorPraticado)
                 .IsRequired();

            builder.Property(x => x.ProdutoParaVenda)
                .IsRequired();

            builder.Property(x => x.ControlaEstoque)
                .IsRequired();

            builder.Property(x => x.ControlaEstoque)
               .IsRequired();
        }
    }
}
