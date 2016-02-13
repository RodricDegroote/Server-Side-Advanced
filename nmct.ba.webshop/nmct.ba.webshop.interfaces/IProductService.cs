using System;
namespace nmct.ba.webshop.interfaces
{
    public interface IProductService
    {
        void ControleButton(nmct.ba.webshop.models.PresentationModel.ProductPM PM, string btnToevoegenOS, string btnToevoegenFW);
        System.Collections.Generic.List<nmct.ba.webshop.models.Framework> getFrameworks();
        System.Collections.Generic.List<nmct.ba.webshop.models.Framework> getGekozenFW(string[] stukken);
        System.Collections.Generic.List<nmct.ba.webshop.models.OS> getGekozenOSn(string[] stukken);
        System.Collections.Generic.List<nmct.ba.webshop.models.OS> getOperatingSystems();
        nmct.ba.webshop.models.PresentationModel.ProductPM GetPM(System.Collections.Generic.List<nmct.ba.webshop.models.OS> Osn, System.Collections.Generic.List<nmct.ba.webshop.models.Framework> Frameworks);
        nmct.ba.webshop.models.Product getProductById(int id);
        System.Collections.Generic.List<nmct.ba.webshop.models.Product> getProducten();
        System.Web.Mvc.SelectList getSelectListFW(System.Collections.Generic.List<nmct.ba.webshop.models.Framework> Fws);
        System.Web.Mvc.SelectList getSelectListOS(System.Collections.Generic.List<nmct.ba.webshop.models.OS> Osn);
        void saveFile(System.Web.HttpPostedFileBase file);
        void SaveProduct(nmct.ba.webshop.models.PresentationModel.ProductPM PM, System.Web.HttpPostedFileBase file);
        void SplitGekozenItems(nmct.ba.webshop.models.PresentationModel.ProductPM PM);
    }
}
