using nmct.ba.webshop.interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace nmct.ba.webshop.Services
{
    public class LanguageService : ILanguageService
    {
        public void ChangeCulture(CultureInfo culture)
        {
            //a. Culture instellen
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
    }
}
