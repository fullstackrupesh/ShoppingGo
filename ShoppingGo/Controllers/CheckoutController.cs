using ShoppingGo.Business;
using ShoppingGo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingGo.Controllers
{
    public class CheckoutController : Controller
    {
        private UnitOfWork unitOfWork;

        public CheckoutController()
        {
            this.unitOfWork = new UnitOfWork();
        }

        public CheckoutController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public ActionResult ContactAndPayment()
        {
            var cart = ShoppingCart.GetShoppingCart(this, unitOfWork);

            var order = cart.GetOrder();

            return View(order);
        }
    }
}