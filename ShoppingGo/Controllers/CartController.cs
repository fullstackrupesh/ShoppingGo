using ShoppingGo.Business;
using ShoppingGo.Repositories;
using ShoppingGo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShoppingGo.Controllers
{
    public class CartController : Controller
    {
        private UnitOfWork unitOfWork;        

        public CartController()
        {
            this.unitOfWork = new UnitOfWork();
        }

        public CartController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var cart = ShoppingCart.GetShoppingCart(this, unitOfWork);

            var cartViewModel = new CartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotalAmount = cart.GetTotalAmount(),
                CartTotalTax = cart.GetTotalTax()
            };

            return View(cartViewModel);
        }

        public async Task<ActionResult> AddToCart(int? productId)
        {
            var cart = ShoppingCart.GetShoppingCart(this, unitOfWork);

            var productToAdd = await unitOfWork.ProductRepository.GetAsync(productId);

            cart.AddToCart(productToAdd);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetShoppingCart(this, unitOfWork);

            var cartItems = unitOfWork.CartRepository.Get();

            string productName = cartItems
                .Single(item => item.RecordId == id).Product.Name;

            int itemCount = cart.RemoveFromCart(id);

            var removeViewModel = new CartRemoveViewModel
            {
                Message = Server.HtmlEncode(productName) + " has been removed from the cart.",
                CartTotalAmount = cart.GetTotalAmount(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeletedId = id
            };

            return Json(removeViewModel);
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetShoppingCart(this, unitOfWork);
            ViewData["CartCount"] = cart.GetCount();

            return PartialView("CartSummary");
        }
    }
}