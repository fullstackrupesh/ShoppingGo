using ShoppingGo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}