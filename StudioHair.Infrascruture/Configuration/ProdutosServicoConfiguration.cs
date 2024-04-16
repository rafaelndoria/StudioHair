using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudioHair.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioHair.Infrascruture.Configuration
{
    public class ProdutosServicoConfiguration : IEntityTypeConfiguration<ProdutosServico>
    {
        public void Configure(EntityTypeBuilder<ProdutosServico> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.QuantidadeUtilizada)
                .IsRequired();

            builder.Property(x => x.ValorProduto)
                .IsRequired();
        }
    }
}
