﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingGo.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }        
        public DateTime DateCreated { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalTax { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}