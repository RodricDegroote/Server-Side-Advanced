using nmct.ba.webshop.context;
using nmct.ba.webshop.interfaces;
using nmct.ba.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MailClient
{
    public class ClientMail
    {
        public void SendMail(ApplicationUser IngelogdUser, Order orderInfo)
        {       
          try
          {
            MailMessage mailMsg = new MailMessage();

            // To
            string recipientName = IngelogdUser.Name;
            string recipientEmail = IngelogdUser.Email;
            mailMsg.To.Add(new MailAddress(recipientEmail, recipientName));

            // From
            mailMsg.From = new MailAddress("Webshop@howest.be", "Admin");

            // Subject and multipart/alternative Body
            mailMsg.Subject = "Order " + DateTime.Now;
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString("Order", null, MediaTypeNames.Text.Plain));
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(Message.Create(orderInfo), null, MediaTypeNames.Text.Html));

            // Init SmtpClient and send
            SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("azure_f9b87edfa6f196ff604ed6fcae9aaa31@azure.com", "3kR9KhfB17tisxp");
            smtpClient.Credentials = credentials;

            smtpClient.Send(mailMsg);
          }
          catch (Exception ex)
          {
            Console.WriteLine(ex.Message);
          }

        }
  }
}