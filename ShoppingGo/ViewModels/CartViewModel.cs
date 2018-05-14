using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoppingGo.Models;

namespace ShoppingGo.ViewModels
{
    public class CartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotalAmount { get; set; }
        public decimal CartTotalTax { get; set; }
    }
}