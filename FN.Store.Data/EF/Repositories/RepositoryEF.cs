﻿using FN.Store.Domain.Contracts.Repositories;
using FN.Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FN.Store.Data.EF.Repositories
{
    public class RepositoryEF<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly StoreDataContext _ctx;
        public readonly DbSet<TEntity> _db;

        public RepositoryEF(StoreDataContext ctx)
        {
            _ctx = ctx;
            _db = _ctx.Set<TEntity>();
        }
        public async Task<TEntity> Get(object id)
        {
            return await _db.FindAsync(id);
        }
        public async Task<List<TEntity>> GetAsync()
        {
            return await _db.ToListAsync();
        }

        public void Add(TEntity entity)
        {
            _db.Add(entity);
            _ctx.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _ctx.Update(entity);
            _ctx.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _ctx.Remove(entity);
            _ctx.SaveChanges();
        }

    }
}
