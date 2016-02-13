using nmct.ba.webshop.models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace nmct.ba.webshop.context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<OS> OS { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Framework> Framework { get; set; }
        public DbSet<Basket> BasketItems { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<FormPost> FormPost { get; set; }

        public ApplicationDbContext()
            : base("ApplicationDbContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
