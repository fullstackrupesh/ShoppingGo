using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace ShoppingGo.Repositories
{
    public class GenericRepository<TEntity> : IDisposable
        where TEntity : class, new()
    {
        private RepositoryContext context;
        private DbSet<TEntity> dbSet;

        public GenericRepository(RepositoryContext context)
        {
            context = new RepositoryContext();
            dbSet = context.Set<TEntity>();
        }

        public virtual Task<List<TEntity>> GetAsync()
        {
            return dbSet.ToListAsync();            
        }

        public virtual Task<TEntity> GetAsync(int? id)
        {
            return dbSet.FindAsync(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }
        
        public Task<int> Save()
        {
            return context.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                context.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}