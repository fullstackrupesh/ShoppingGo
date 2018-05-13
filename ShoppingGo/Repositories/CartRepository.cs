using ShoppingGo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ShoppingGo.Repositories
{
    public class CartRepository : IRepository<Cart>
    {
        private RepositoryContext context;
        private DbSet<Cart> dbSet;

        public CartRepository(RepositoryContext context)
        {
            this.context = context;
            dbSet = context.Carts;
        }

        public Task<List<Cart>> GetAsync()
        {
            return dbSet.ToListAsync();
        }

        public Task<Cart> GetAsync(int? id)
        {
            return dbSet.FindAsync(id);
        }

        public Task<int> InsertAsync(Cart entity)
        {
            dbSet.Add(entity);
            return context.SaveChangesAsync();

        }

        public Task<int> UpdateAsync(Cart entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return context.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(Cart entity)
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