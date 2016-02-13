using nmct.ba.webshop.interfaces;
using nmct.ba.webshop.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ba.webshop.repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public void SaveProduct(Product nieuw)
        {
            foreach (OS os in nieuw.OperatingSystems)
                context.Entry<OS>(os).State = EntityState.Unchanged;

            foreach (Framework framework in nieuw.Frameworks)
                context.Entry<Framework>(framework).State = EntityState.Unchanged;

            context.Product.Add(nieuw);
            context.SaveChanges();
        }
    }
}