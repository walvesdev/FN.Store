using FN.Store.Data.EF.TypeConfiguration;
using FN.Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FN.Store.Data.EF
{
    public class StoreDataContext : DbContext
    {
        private IConfiguration _config;

        public DbSet<Produto> Produto { get; set; }
        public DbSet<Categoria> Categoria { get; set; }

        public StoreDataContext(IConfiguration config)
        {
            _config = config;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("StoreDataContext"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Produto>( new ProdutoTypeConfiguration());
           
        }
    }
}
