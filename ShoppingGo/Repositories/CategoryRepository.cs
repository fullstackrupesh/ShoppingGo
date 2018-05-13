using ShoppingGo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ShoppingGo.Repositories
{
    public class CategoryRepository : IRepository<Category>, IDisposable
    {
        private RepositoryContext context;
        private DbSet<Category> dbSet;

        public CategoryRepository(RepositoryContext context)
        {
            this.context = context;
            dbSet = context.Categories;
        }

        public Task<List<Category>> GetAsync()
        {
            return dbSet.ToListAsync();
        }

        public Task<Category> GetAsync(int? id)
        {
            return dbSet.FindAsync(id);
        }

        public Task<int> InsertAsync(Category entity)
        {
            dbSet.Add(entity);
            return context.SaveChangesAsync();
        }

        public Task<int> UpdateAsync(Category entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return context.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(Category entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            return context.SaveChangesAsync();
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