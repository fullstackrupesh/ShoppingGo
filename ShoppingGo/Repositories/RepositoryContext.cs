using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShoppingGo.Repositories
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext() : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<ShoppingGo.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<ShoppingGo.Models.Product> Products { get; set; }
    }
}