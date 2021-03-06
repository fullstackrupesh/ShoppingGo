﻿using ShoppingGo.Business;
using ShoppingGo.Models;
using ShoppingGo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpPost]
        public async Task<ActionResult> ContactAndPayment(FormCollection values)
        {
            var order = new Order();

            TryUpdateModel(order);

            try
            {
                order.DateCreated = DateTime.Now;

                await unitOfWork.OrderRepository.InsertAsync(order);

                var cart = ShoppingCart.GetShoppingCart(this, unitOfWork);
                int? status = await cart.CreateOrderAsync(order);

                await cart.EmptyCartAsync();

                return RedirectToAction("Complete", new { id = order.OrderId });
            }
            catch (Exception ex)
            {
                return View(order);
            }
        }

        public ActionResult Complete(int id)
        {
            bool isValid = unitOfWork.OrderRepository.Get()
                .Any(o => o.OrderId == id);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }

    }
}