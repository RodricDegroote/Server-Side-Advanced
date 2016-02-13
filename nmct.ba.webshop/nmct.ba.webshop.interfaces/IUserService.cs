using nmct.ba.webshop.context;
using nmct.ba.webshop.models;
using nmct.ba.webshop.models.PresentationModel;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace nmct.ba.webshop.interfaces
{
    public interface IUserService
    {
        ApplicationUser getAppUser(string userId);
        ApplicationUser getAppUser(IPrincipal User);
        nmct.ba.webshop.models.User GetUserInformation(string userId);
    }
}
