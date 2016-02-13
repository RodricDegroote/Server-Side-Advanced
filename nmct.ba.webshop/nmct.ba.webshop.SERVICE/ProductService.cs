using CloudService;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Blob;
using nmct.ba.webshop.Cache;
using nmct.ba.webshop.interfaces;
using nmct.ba.webshop.models;
using nmct.ba.webshop.models.PresentationModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ba.webshop.Services
{
    public class ProductService : IProductService
    {
        private IGenericRepository<Framework> repoFm = null;
        private IGenericRepository<OS> repoOs = null;

        private IProductRepository repoProduct = null;


        public ProductService(IGenericRepository<Framework> repoFM,
             IGenericRepository<OS> repoOs,
             IProductRepository repoProduct, IBasketRepository repoBasket)
        {
            this.repoOs = repoOs;
            this.repoFm = repoFM;
            this.repoProduct = repoProduct;
        }

        public List<OS> getOperatingSystems() 
        {
            //return repoOs.All().ToList();
            return WebshopCache.GetOperatingSystemsFromCache("OperatingSystems");
        }

        public List<Framework> getFrameworks()
        {
            //return repoFm.All().ToList();
            return WebshopCache.GetFrameworksFromCache("Frameworks");
        }

        public List<Product> getProducten()
        {
            //return repoProduct.All().ToList();
            return WebshopCache.GetProductenFromCache("Producten");
        }

        public Product getProductById(int id)
        {
            return repoProduct.GetByID(id);
        }

        public void SaveProduct(ProductPM PM, HttpPostedFileBase file)
        {
            PM.product.Image = file.FileName;
            repoProduct.SaveProduct(PM.product);
            WebshopCache.RefreshCash("Producten");
        }

        public List<Framework> getGekozenFW(string[] stukken)
        {
            List<Framework> fm = new List<Framework>();

            if(stukken.Length < 1)
            {
                return fm;
            }
            
            foreach (string stuk in stukken)
                if (stuk != "")
                    fm.Add(repoFm.GetByID(Convert.ToInt32(stuk)));
            return fm;
        }

        public List<OS> getGekozenOSn(string[] stukken)
        {
            List<OS> os = new List<OS>();

            if (stukken.Length < 1)
            {
                return os;
            }


            foreach (string stuk in stukken)
                if (stuk != "")
                    //os.Add(CataloogRepository.getOS(Convert.ToInt32(stuk)));
                    os.Add(repoOs.GetByID(Convert.ToInt32(stuk)));
            
            return os;
        }

        public void SplitGekozenItems(ProductPM PM)
        {
            if (PM.ids == null && PM.idsF == null)
            {
                PM.GekozenFMs = new SelectList(new List<Framework>(), "FrameworkId", "Naam");
                PM.GekozenOSn = new SelectList(new List<OS>(), "OSId", "Naam");
                return;
            }
            else
            {
                string[] stukken = PM.ids.Split(';');
                PM.product.OperatingSystems = getGekozenOSn(stukken);
                PM.GekozenOSn = new SelectList(PM.product.OperatingSystems, "OSId", "Naam");

                stukken = PM.idsF.Split(';');
                PM.product.Frameworks = getGekozenFW(stukken);
                PM.GekozenFMs = new SelectList(PM.product.Frameworks, "FrameworkId", "Naam");
            }

        }

        public void ControleButton(ProductPM PM, string btnToevoegenOS, string btnToevoegenFW)
        {
            if (btnToevoegenOS == "Toevoegen")
            {
                PM.ids += PM.SelectedOS.ToString() + ";";
                PM.idsF += "";
                SplitGekozenItems(PM);
            }
            else if (btnToevoegenFW == "Toevoegen")
            {
                PM.idsF += PM.SelectedFrameWork.ToString() + ";";
                PM.ids += "";
                SplitGekozenItems(PM);
            }
            else
            {
                SplitGekozenItems(PM);
            }
        }

        public void saveFile(HttpPostedFileBase file)
        {
            CloudBlockBlob blob = AzureStorage.getBlobReference(file.FileName, "StorageConnectionString", "images");
            blob.UploadFromStream(file.InputStream);
        }

        public ProductPM GetPM(List<OS> Osn, List<Framework> Frameworks)
        {
            ProductPM PM = new ProductPM();
            SelectList GekozenOSn = new SelectList(new List<OS>(), "OSId", "Naam");
            SelectList GekozenFMs = new SelectList(new List<Framework>(), "FrameworkId", "Naam");

            PM.OSsn = getSelectListOS(Osn);
            PM.FrameWorks = getSelectListFW(Frameworks);

            PM.GekozenOSn = GekozenOSn;
            PM.GekozenFMs = GekozenOSn;
            PM.product = new Product();

            return PM;
        }

        public SelectList getSelectListOS(List<OS> Osn)
        {
 	        return new SelectList(Osn, "OSId", "Naam");
        }

        public SelectList getSelectListFW(List<Framework> Fws)
        {
            return new SelectList(Fws, "FrameworkId", "Naam");
        }
    }
}