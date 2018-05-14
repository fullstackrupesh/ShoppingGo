using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingGo.Models 
{
    public class Order
    {
        [Key]
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }
        [ScaffoldColumn(false)]
        public decimal TotalAmount { get; set; }
        [ScaffoldColumn(false)]
        public decimal TotalTax { get; set; }
        [Required]
        [DisplayName("Mobile Number")]
        [StringLength(10)]
        public string MobileNo { get; set; }
        [ScaffoldColumn(false)]
        public DateTime DateCreated { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}