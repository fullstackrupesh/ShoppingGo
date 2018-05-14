using ShoppingGo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ShoppingGo.Repositories
{
    public class OrderDetailRepository : IRepository<OrderDetail>, IDisposable
    {
        private RepositoryContext context;
        private DbSet<OrderDetail> dbSet;

        public OrderDetailRepository(RepositoryContext context)
        {
            this.context = context;
            dbSet = context.OrderDetails;
        }

        public Task<List<OrderDetail>> GetAsync()
        {
            return dbSet.ToListAsync();
        }

        public Task<OrderDetail> GetAsync(int? id)
        {
            return dbSet.FindAsync(id);
        }

        public Task<int> InsertAsync(OrderDetail entity)
        {
            dbSet.Add(entity);
            return context.SaveChangesAsync();
        }

        public Task<int> UpdateAsync(OrderDetail entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return context.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(OrderDetail entity)
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