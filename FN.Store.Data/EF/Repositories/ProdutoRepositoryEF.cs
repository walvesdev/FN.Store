using FN.Store.Domain.Contracts.Repositories;
using FN.Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FN.Store.Data.EF.Repositories
{
    public class ProdutoRepositoryEF : RepositoryEF<Produto>, IProdutoRepository
    {
        private readonly DbSet<Produto> _dbset;

        public ProdutoRepositoryEF(StoreDataContext ctx) : base(ctx)
        {
            _dbset = ctx.Set<Produto>();
        }

        public async Task<IEnumerable<Produto>> GetAllWithCategoryAsync()
        {
            return await _db.Include(c => c.Categoria).ToListAsync();
        }

        public async Task<Produto> GetByIdWithCategoryAsync(int id)
        {
            return await _db.Include(c => c.Categoria).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Produto>> GetByName(string name)
        {
            return await _db.Where(p => p.Nome.Contains(name)).ToListAsync();
        }
    }
}
