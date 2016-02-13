using nmct.ba.webshop.models;
using System;
namespace nmct.ba.webshop.interfaces
{
    public interface IUserRepository: IGenericRepository<User>
    {
        nmct.ba.webshop.context.ApplicationUser getUser(string userId);
    }
}
