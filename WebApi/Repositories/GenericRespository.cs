using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Prysm.Monitoring.WebApi.Repositories
{
    public class GenericRepository<TEntity> where TEntity: class
    {
        private readonly DbSet<TEntity> _dbSet;
        private readonly DatabaseContext _context;

        public GenericRepository(DatabaseContext context)
        {
            this._context = context;
            this._dbSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Load()
        {
            return _dbSet;
        }

        public virtual void Insert(TEntity entity)
        {
           _dbSet.Add(entity);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}