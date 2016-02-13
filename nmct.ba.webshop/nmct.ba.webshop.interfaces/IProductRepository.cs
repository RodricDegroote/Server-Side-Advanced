using nmct.ba.webshop.models;
using System;
using System.Web;
namespace nmct.ba.webshop.interfaces
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        void SaveProduct(nmct.ba.webshop.models.Product nieuw);
    }
}
