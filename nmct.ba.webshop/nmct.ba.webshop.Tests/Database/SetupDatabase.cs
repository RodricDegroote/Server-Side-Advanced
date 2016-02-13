using Microsoft.AspNet.Identity;
using nmct.ba.webshop.context;
using nmct.ba.webshop.models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.webshop.Tests.Database
{
    public class SetupDatabase : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        public override void InitializeDatabase(ApplicationDbContext context)
        {
            base.InitializeDatabase(context);
            FillData(context);
        }

        private void FillData(ApplicationDbContext context)
        {
            List<Product> Producten = new List<Product>();
            List<OS> OSn = new List<OS>();
            List<Framework> Frameworks = new List<Framework>();

            string sPath = AppDomain.CurrentDomain.BaseDirectory + "..\\App_Data\\Devices.txt";
            string sPath2 = AppDomain.CurrentDomain.BaseDirectory + "..\\App_Data\\Os.txt";
            string sPath3 = AppDomain.CurrentDomain.BaseDirectory + "..\\App_Data\\ProgrammingFramework.txt";


            //inladen en toevoegen van OS en Frameworks
            #region Inladen OS en Frameworks
            StreamReader reader2 = new StreamReader(sPath2);
            string sLijn2 = reader2.ReadLine();

            while (sLijn2 != null)
            {
                OS os = MaakOS(sLijn2, context);

                if (os != null)
                {
                    OSn.Add(os);
                }

                sLijn2 = reader2.ReadLine();
            }

            StreamReader reader3 = new StreamReader(sPath3);
            string sLijn3 = reader3.ReadLine();

            while (sLijn3 != null)
            {
                Framework framework = MakeFramework(sLijn3, context);

                if (framework != null)
                {
                    Frameworks.Add(framework);
                }

                sLijn3 = reader3.ReadLine();
            }
            #endregion

            #region Toevoegen OS en Frameworks aan de database via EF
            foreach (OS os in OSn)
                context.OS.AddOrUpdate(os);

            foreach (Framework framew in Frameworks)
                context.Framework.AddOrUpdate(framew);

            context.SaveChanges();
            #endregion

            //devices inladen
            #region Inladen Devices (Producten)
            StreamReader reader = new StreamReader(sPath);
            string sLijn = reader.ReadLine();
            sLijn = reader.ReadLine();

            while (sLijn != null)
            {
                Product prod = MaakProduct(sLijn, context);

                if (prod != null)
                {
                    Producten.Add(prod);
                }

                sLijn = reader.ReadLine();
            }
            #endregion

            #region Toevoegen Devices (Producten) aan de database
            foreach (Product prod in Producten)
                context.Product.AddOrUpdate(prod);

            context.SaveChanges();
            #endregion

        }

        private Framework MakeFramework(string sLijn3, ApplicationDbContext context)
        {
            string[] stukken = sLijn3.Split(';');

            if (stukken.Length > 0)
            {
                Framework nieuw = new Framework()
                {
                    FrameworkId = Convert.ToInt32(stukken[0]),
                    Naam = stukken[1].ToString()
                };

                return nieuw;
            }
            return null;
        }

        private OS MaakOS(string sLijn2, ApplicationDbContext context)
        {
            string[] stukken = sLijn2.Split(';');

            if (stukken.Length > 0)
            {
                OS nieuw = new OS()
                {
                    OSId = Convert.ToInt32(stukken[0]),
                    Naam = stukken[1].ToString()
                };

                return nieuw;
            }
            return null;
        }

        private Product MaakProduct(string sLijn, ApplicationDbContext context)
        {
            string[] stukken = sLijn.Split(';');

            if (stukken.Length > 0)
            {
                Product nieuw = new Product()
                {
                    //ID;Name;Price;RentPrice;Stock;Picture;OS;Framework;Description
                    ProductId = Convert.ToInt32(stukken[0]),
                    Naam = stukken[1].ToString(),
                    Aankoopprijs = Convert.ToDouble(stukken[2]),
                    Huurprijs = Convert.ToDouble(stukken[3]),
                    Aantal = Convert.ToInt32(stukken[4]),
                    Image = stukken[5].ToString(),
                    Description = stukken[8].ToString()
                };

                nieuw.OperatingSystems = new List<OS>();

                string[] stukkenOs = stukken[6].Split('-');
                if (stukkenOs.Length > 0)
                {
                    foreach (string stuk in stukkenOs)
                    {
                        int id2 = Int32.Parse(stuk);
                        var query = (from o in context.OS
                                     where o.OSId == id2
                                     select o);
                        nieuw.OperatingSystems.Add(query.SingleOrDefault<OS>());
                    }
                }

                nieuw.Frameworks = new List<Framework>();
                string[] stukkenFrameW = stukken[7].Split('-');
                if (stukkenFrameW.Length > 0)
                {
                    foreach (string stuk in stukkenFrameW)
                    {
                        int id = Int32.Parse(stuk);
                        var query2 = (from f in context.Framework
                                      where f.FrameworkId == id
                                      select f);
                        nieuw.Frameworks.Add(query2.SingleOrDefault<Framework>());
                    }
                }

                return nieuw;
            }
            return null;
        }
    }
}
