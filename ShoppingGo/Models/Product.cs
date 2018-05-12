using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingGo.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Range(0.0, 500.0)]
        public decimal Price { get; set; }
        public Category Category { get; set; }
    }
}