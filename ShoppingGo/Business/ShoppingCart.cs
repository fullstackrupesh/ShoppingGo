using ShoppingGo.Models;
using ShoppingGo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingGo.Business
{
    public class ShoppingCart : IDisposable
    {
        public string ShoppingCartId { get; set; }
        public const string CartSessionKey = "ShoppingCartId";

        private UnitOfWork unitOfWork;

        private ShoppingCart(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public static ShoppingCart GetShoppingCart(Controller controller, UnitOfWork unitOfWork)
        {
            return GetShoppingCart(controller.HttpContext, unitOfWork);
        }

        public Order GetOrder()
        {
            var order = new Order();
            order.OrderDetails = new List<OrderDetail>();
            decimal orderTotalAmount = 0;
            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    OrderId = order.OrderId,
                    Amount = item.Product.Price,
                    Quantity = item.Quantity
                };

                orderTotalAmount += item.Quantity * (item.Product.Price + (item.Product.Category.Tax / 100 * item.Product.Price));
                order.OrderDetails.Add(orderDetail);
            }

            order.TotalAmount = orderTotalAmount;

            return order;
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;
            decimal orderTax = 0;
            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    OrderId = order.OrderId,
                    Amount = item.Product.Price,
                    Quantity = item.Quantity
                };

                orderTotal += item.Quantity * (item.Product.Price + (item.Product.Category.Tax / 100 * item.Product.Price));
                orderTax += item.Quantity * (item.Product.Category.Tax / 100 * item.Product.Price);
                unitOfWork.OrderDetailRepository.InsertAsync(orderDetail);
            }

            order.TotalAmount = orderTotal;
            order.TotalTax = orderTax;

            unitOfWork.OrderRepository.UpdateAsync(order);

            EmptyCart();

            return order.OrderId;

        }

        private static ShoppingCart GetShoppingCart(HttpContextBase httpContext, UnitOfWork unitOfWork)
        {
            var cart = new ShoppingCart(unitOfWork);
            cart.ShoppingCartId = cart.GetCartId(httpContext);
            return cart;
        }

        private string GetCartId(HttpContextBase httpContext)
        {
            if (httpContext.Session[CartSessionKey] == null)
            {
                Guid tempCartId = Guid.NewGuid();
                httpContext.Session[CartSessionKey] = tempCartId.ToString();
            }
            return httpContext.Session[CartSessionKey].ToString();
        }

        public void AddToCart(Product product)
        {
            var cartItems = unitOfWork.CartRepository.Get();

            var cartItem = cartItems
                .SingleOrDefault(
                    c => c.CartId == ShoppingCartId &&
                    c.ProductId == product.ProductId
                );

            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    ProductId = product.ProductId,
                    Product = product,
                    CartId = ShoppingCartId,
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };
                cartItem.CalculateAmountAndTax();
                
                unitOfWork.CartRepository.InsertAsync(cartItem);
            }
            else
            {                
                cartItem.Quantity++;
                cartItem.CalculateAmountAndTax();
                unitOfWork.CartRepository.UpdateAsync(cartItem);
            }
        }
        
        public int RemoveFromCart(int id)
        {
            var cartItem = unitOfWork.CartRepository.Get()
                .Single(
                    cart => cart.CartId == ShoppingCartId
                    && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    itemCount = cartItem.Quantity;
                    cartItem.CalculateAmountAndTax();
                    unitOfWork.CartRepository.UpdateAsync(cartItem);
                }
                else
                {
                    unitOfWork.CartRepository.DeleteAsync(cartItem);
                }                
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = unitOfWork.CartRepository.Get().Where(cart => cart.CartId == ShoppingCartId);

            foreach (var item in cartItems)
            {
                unitOfWork.CartRepository.DeleteAsync(item);
            }            
        }

        public List<Cart> GetCartItems()
        {
            return unitOfWork.CartRepository.Get().Where(cart => cart.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            int? count = (from cartItem in unitOfWork.CartRepository.Get()
                          where cartItem.CartId == ShoppingCartId
                          select (int?)cartItem.Quantity).Sum();

            return count ?? 0;
        }

        public decimal GetTotalAmount()
        {
            decimal? total = (from cartItem in unitOfWork.CartRepository.Get()
                              where cartItem.CartId == ShoppingCartId
                              select (cartItem.Product.Price
                              + (cartItem.Product.Category.Tax / 100 * cartItem.Product.Price))
                              * cartItem.Quantity)
                              .Sum();

            return total ?? decimal.Zero;
        }

        public decimal GetTotalTax()
        {
            decimal? total = (from cartItem in unitOfWork.CartRepository.Get()
                              where cartItem.CartId == ShoppingCartId
                              select (cartItem.Product.Category.Tax / 100 * cartItem.Product.Price)
                              * cartItem.Quantity)
                             .Sum();

            return total ?? decimal.Zero;
        }


        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                unitOfWork.Dispose();
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