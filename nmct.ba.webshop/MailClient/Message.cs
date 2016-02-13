using CloudService;
using nmct.ba.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MailClient
{
    public class Message
    {
        public static string Create(Order info)
        {
            try
            {
                string producten = "";
                // Producten
                foreach (OrderLine orderline in info.OrderLines)
                {
                    producten += getProductLayout(orderline);        
                }

                string html = getBodyEmail(producten, info.ID);
                return html;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        private static string getBodyEmail(string producten, int orderId)
        {
            string body =@"
                        <h1>Bestelling</h1>
                        <p>Bedankt voor het plaatsen van uw order (<strong>OrderID:</strong> " + orderId + ")" + "\r\n" +
                          "in onze WebShop. Uw order wordt zo dadelijk verwerkt.</p><br />" + "\r\n" +
                        "<h2>Order info</h2>" + "\r\n" +
                        producten + "\r\n" +
                        "<br />" + "\r\n" +
                        "<p>Met vriendelijke groeten</p>" + "\r\n" +
                        "<p>Het Webshop-team</p>";

            return body;
        }

        private static string getProductLayout(OrderLine item)
        {
            string layout = "<table>" + "\r\n" +
                            "<tr><td rowspan='3'><img src='https://webshop.blob.core.windows.net/images/" + item.BasketItem.prod.Image + "'/></td>" + "\r\n" +
                            "<td><strong>Product: </strong>" + item.BasketItem.prod.Naam + "</td></tr>" + "\r\n" +
                            "<tr><td><strong>Aantal: </strong>" + item.BasketItem.Aantal + "</td></tr>" + "\r\n" +
                            "<tr><td><strong>Prijs: </strong> € " + item.TotaalPrijs +"</td></tr><tr><td></td><td></td></table>";

            return layout;
        }
    }
}
