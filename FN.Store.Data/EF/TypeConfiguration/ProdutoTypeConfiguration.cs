using FN.Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FN.Store.Data.EF.TypeConfiguration
{
    public class ProdutoTypeConfiguration : IEntityTypeConfiguration<Produto>
    {
        public ProdutoTypeConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable(nameof(Produto));
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Nome).HasColumnType("varchar(100)").IsRequired();
            builder.Property(p => p.Preco).HasColumnType("money").IsRequired();
            builder.Property(p => p.DataCriacao);
            builder.Property(p => p.Preco);
            builder.HasOne(p => p.Categoria).WithMany(c => c.Produtos);
        }
    }
}
