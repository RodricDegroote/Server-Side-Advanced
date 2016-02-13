using Calculater;
using nmct.ba.webshop.context;
using nmct.ba.webshop.interfaces;
using nmct.ba.webshop.models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.webshop.repositories
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        public ApplicationUser getUser(string userId)
        {
            ApplicationUser currentUser = context.Users.FirstOrDefault(x => x.Id == userId);
            return currentUser;
        }
    }
}
