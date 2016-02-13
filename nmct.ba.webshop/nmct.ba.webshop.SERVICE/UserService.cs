using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using nmct.ba.webshop.Cache;
using nmct.ba.webshop.context;
using nmct.ba.webshop.interfaces;
using nmct.ba.webshop.models;
using nmct.ba.webshop.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;


namespace nmct.ba.webshop.Services
{
    public class UserService : IUserService
    {
        private IUserRepository repoUser = null;

        public UserService(IUserRepository repoUser)
        {
            this.repoUser = repoUser;
        }

        public User GetUserInformation(string userId)
        {
            return repoUser.GetByID(userId);
        }

        public ApplicationUser getAppUser(IPrincipal User)
        {
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);
            ApplicationUser user = userManager.FindByNameAsync(User.Identity.Name).Result;

            return user;
        }

        public ApplicationUser getAppUser(string userId)
        {
            return repoUser.getUser(userId);
        }
    }
}
