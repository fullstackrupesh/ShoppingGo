using ShoppingGo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingGo.ViewModels
{
    public class ProductViewModel
    {        
        public ProductViewModel()
        {

        }

        public ProductViewModel(Product product)
        {
            this.ProductId = product.ProductId;
            this.Name = product.Name;
            this.Price = product.Price;
            this.CategoryId = product.CategoryId;            
        }

        public int ProductId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(0.0, 500.0)]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
    
        public List<Category> Categories { get; set; }
    }
}