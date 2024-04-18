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

            builder.HasOne(x => x.Produto)
                .WithMany(x => x.ProdutosServicos)
                .HasForeignKey(x => x.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Servico)
                .WithMany(x => x.ProdutosServicos)
                .HasForeignKey(x => x.ServicoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
