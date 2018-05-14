using ShoppingGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ShoppingGo.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private RepositoryContext context = new RepositoryContext();

        private CategoryRepository categoryRepository;
        public CategoryRepository CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new CategoryRepository(context);
                }
                return categoryRepository;
            }
        }

        private ProductRepository productRepository;
        public ProductRepository ProductRepository
        {
            get
            {
                if (this.productRepository == null)
                {
                    this.productRepository = new ProductRepository(context);
                }
                return productRepository;
            }
        }

        private CartRepository cartRepository;

        public CartRepository CartRepository
        {
            get
            {
                if (this.cartRepository == null)
                {
                    this.cartRepository = new CartRepository(context);
                }
                return cartRepository;
            }            
        }

        private OrderRepository orderRepository;
        public OrderRepository OrderRepository
        {
            get
            {
                if (this.orderRepository == null)
                {
                    this.orderRepository = new OrderRepository(context);
                }
                return orderRepository;
            }
        }

        private OrderDetailRepository orderDetailRepository;
        public OrderDetailRepository OrderDetailRepository
        {
            get
            {
                if (this.orderDetailRepository == null)
                {
                    this.orderDetailRepository = new OrderDetailRepository(context);
                }
                return orderDetailRepository;
            }
        }

        public Task<int> SaveAsync()
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