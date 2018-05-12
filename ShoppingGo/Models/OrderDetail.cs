using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingGo.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxApplicable { get; set; }
    }
}