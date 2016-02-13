using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nmct.ba.webshop;
using nmct.ba.webshop.Controllers;
using nmct.ba.webshop.models;
using nmct.ba.webshop.Tests.Database;
using nmct.ba.webshop.models.PresentationModel;
using nmct.ba.webshop.context;
using nmct.ba.webshop.Services;
using nmct.ba.webshop.repositories;
using nmct.ba.webshop.interfaces;

namespace nmct.ba.webshop.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private ProductController controller = null;
        private CataloogController catController = null;
        private ProductService productService = null;
        private BasketService basketService = null;
        private UserService userservice = null;
        private LanguageService languageservice = null;
        private IGenericRepository<OS> repoOs = null;
        private IGenericRepository<Framework> repoFm = null;
        private UserRepository repoUser = null;
        private IProductRepository repoProduct = null;
        private IBasketRepository repoBasket = null;
        private IOrderQueueRepository repoOrderQueue = null;

        [TestInitialize]
        public void Setup()
        {
            new SetupDatabase().InitializeDatabase(new ApplicationDbContext());
            repoProduct = new ProductRepository();
            repoBasket = new BasketRepository();
            repoOrderQueue = new OrderQueueRepository();
            repoFm = new GenericRepository<Framework>();
            repoOs = new GenericRepository<OS>();
            repoUser = new UserRepository();
            productService = new ProductService(repoFm, repoOs, repoProduct, repoBasket);
            basketService = new BasketService(repoBasket);
            userservice = new UserService(repoUser);
            languageservice = new LanguageService();
            controller = new ProductController(productService);
            catController = new CataloogController(productService, basketService, userservice, languageservice);
        }

        [TestMethod]
        public void Index_test()
        {
           //Act
            ViewResult result = (ViewResult)catController.Index();
            List<Product> producten = result.Model as List<Product>;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<Product>));
            Assert.IsTrue(producten.Count == 5);
        }

        //Opvragen van details methode in controller
        [TestMethod]
        [HttpGet]
        public void Details_test()
        {
            //Act
            ViewResult result = (ViewResult)catController.InfoPlus(1);
            Product prod = result.Model as Product;

            //Assert
            Assert.IsNotNull(prod);
            Assert.IsInstanceOfType(result.Model, typeof(Product));
            Assert.IsTrue(prod.ProductId == 1);
        }


        //Een test voor de http GET voor het toevoegen van een nieuwe device
        [TestMethod]
        [HttpGet]
        public void Toevoegen_test()
        {
            //Act
            ProductPM test = new ProductPM();
            ViewResult result = (ViewResult)controller.Toevoegen();
            ProductPM pm = result.Model as ProductPM;

            //Assert
            Assert.IsNotNull(pm);
            Assert.IsInstanceOfType(result.Model, typeof(ProductPM));
        }


        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
