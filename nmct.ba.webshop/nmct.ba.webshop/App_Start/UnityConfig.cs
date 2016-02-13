using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using nmct.ba.webshop.models;
using nmct.ba.webshop.Controllers;
using nmct.ba.webshop.Services;
using nmct.ba.webshop.repositories;
using nmct.ba.webshop.interfaces;

namespace nmct.ba.webshop
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IGenericRepository<Framework>, GenericRepository<Framework>>();

            container.RegisterType<IGenericRepository<OS>, GenericRepository<OS>>();
            container.RegisterType<IGenericRepository<User>, GenericRepository<User>>();
            container.RegisterType<IGenericRepository<FormPost>, GenericRepository<FormPost>>();

            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IBasketService, BasketService>();

            container.RegisterType<IBasketRepository, BasketRepository>();
            container.RegisterType<IOrderQueueRepository, OrderQueueRepository>();

            container.RegisterType<IUserService, UserService>();
            container.RegisterType<ILanguageService, LanguageService>();

            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IServiceBusService, ServiceBusService>();
            container.RegisterType<IOrderRepository, OrderRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            
            container.RegisterType<AccountController>(new InjectionConstructor());
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}