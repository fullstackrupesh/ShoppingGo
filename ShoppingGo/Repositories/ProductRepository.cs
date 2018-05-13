using ShoppingGo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ShoppingGo.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private RepositoryContext context;
        private DbSet<Product> dbSet;
        
        public ProductRepository(RepositoryContext context)
        {
            this.context = context;
            dbSet = context.Products;
        }

        public Task<List<Product>> GetAsync()
        {
            return dbSet.Include(p => p.Category).ToListAsync();
        }

        public Task<Product> GetAsync(int? id)
        {
            return dbSet.FindAsync(id);
        }
        
        public Task<int> InsertAsync(Product entity)
        {
            dbSet.Add(entity);
            return context.SaveChangesAsync();
        }

        public Task<int> UpdateAsync(Product entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return context.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(Product entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            return context.SaveChangesAsync();
        }
    }
}