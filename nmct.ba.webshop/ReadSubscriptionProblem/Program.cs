using Microsoft.Azure;
using Microsoft.ServiceBus.Messaging;
using nmct.ba.webshop.interfaces;
using nmct.ba.webshop.models;
using nmct.ba.webshop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadSubscriptionProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("FormType PROBLEM");

            IGenericRepository<FormPost> repoForm = new GenericRepository<FormPost>();
            IServiceBusService sbs = new ServiceBusService(repoForm);

            string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");

            SubscriptionClient Client = SubscriptionClient.CreateFromConnectionString(connectionString, "WebsiteMessages", "Problem");

            // Configure the callback options
            OnMessageOptions options = new OnMessageOptions();
            options.AutoComplete = false;
            options.AutoRenewTimeout = TimeSpan.FromMinutes(1);

            Client.OnMessage((message) =>
            {
                try
                {
                    FormPost form = message.GetBody<FormPost>();
                    //SAVE FORM
                    //sbs.SaveSubscription(form);
                    Console.WriteLine("Voornaam: " + form.Name);
                    Console.WriteLine("Achternaam: " + form.LastName);
                    Console.WriteLine("OrderNr: " + form.OrderNr);
                    Console.WriteLine("FormType: " + form.FormType);
                    Console.WriteLine("Description: " + form.Description);
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("");
                    message.Complete();
                }
                catch (Exception)
                {

                    message.Abandon();
                }
            }, options);


            Console.WriteLine("FormType QUESTION");

            SubscriptionClient Client2 = SubscriptionClient.CreateFromConnectionString(connectionString, "WebsiteMessages", "Question");

            // Configure the callback options
            OnMessageOptions options2 = new OnMessageOptions();
            options.AutoComplete = false;
            options.AutoRenewTimeout = TimeSpan.FromMinutes(1);

            Client2.OnMessage((message) =>
            {
                try
                {
                    FormPost form = message.GetBody<FormPost>();
                    //SAVE FORM
                    //sbs.SaveSubscription(form);
                    Console.WriteLine("Voornaam: " + form.Name);
                    Console.WriteLine("Achternaam: " + form.LastName);
                    Console.WriteLine("OrderNr: " + form.OrderNr);
                    Console.WriteLine("FormType: " + form.FormType);
                    Console.WriteLine("Description: " + form.Description);
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("");
                    message.Complete();
                }
                catch (Exception)
                {

                    message.Abandon();
                }
            }, options);


            Console.Read();
        }
    }
}
