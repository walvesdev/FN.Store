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
    public class CategoriaTypeConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public CategoriaTypeConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Nome).HasColumnType("varchar(100)").IsRequired();
            builder.Property(c => c.Descricao).HasColumnType("varchar(500)").IsRequired();
            builder.Property(c => c.DataCriacao);
            builder.Property(c => c.DataAlteracao);
            builder.HasMany(c => c.Produtos).WithOne(p => p.Categoria);



        }
    }
}
