using ShoppingGo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ShoppingGo.Repositories
{
    public class OrderRepository : IRepository<Order>, IDisposable
    {
        private RepositoryContext context;
        private DbSet<Order> dbSet;

        public OrderRepository(RepositoryContext context)
        {
            this.context = context;
            dbSet = context.Orders;
        }

        public List<Order> Get()
        {
            return dbSet.Include("OrderDetails").ToList();
        }

        public Task<List<Order>> GetAsync()
        {
            return dbSet.Include("OrderDetails").ToListAsync();
        }

        public Task<Order> GetAsync(int? id)
        {
            return dbSet.FindAsync(id);
        }

        public Task<int> InsertAsync(Order entity)
        {
            dbSet.Add(entity);
            return context.SaveChangesAsync();
        }

        public Task<int> UpdateAsync(Order entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return context.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(Order entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
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