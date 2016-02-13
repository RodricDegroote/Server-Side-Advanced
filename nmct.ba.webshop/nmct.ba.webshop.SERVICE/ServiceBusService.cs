using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using nmct.ba.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.Azure;

namespace nmct.ba.webshop.Services
{
    public class ServiceBusService : nmct.ba.webshop.Services.IServiceBusService
    {
        private IGenericRepository<FormPost> repoForm = null;

        public ServiceBusService(IGenericRepository<FormPost> repoForm)
        {
            this.repoForm = repoForm;
        }

        public void MakeTopic(FormPost post)
        {
            string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");

            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

            if (!namespaceManager.TopicExists("WebsiteMessages"))
            {
                namespaceManager.CreateTopic("WebsiteMessages");
            }

            TopicClient Client =
                TopicClient.CreateFromConnectionString(connectionString, "WebsiteMessages");

            BrokeredMessage message =
             new BrokeredMessage(post);
            message.Properties["FormType"] = post.FormType;
            Client.Send(message);
        }

        public void MakeSubscription()
        {
            //https://msdn.microsoft.com/en-us/library/azure/hh367516.aspx
            //http://azure.microsoft.com/EN-US/documentation/articles/service-bus-dotnet-how-to-use-topics-subscriptions/

            string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");


            var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

            if (!namespaceManager.SubscriptionExists("WebsiteMessages", "Problem"))
            {
                SqlFilter filter = new SqlFilter("FormType = 'Problem'");
                namespaceManager.CreateSubscription("WebsiteMessages", "Problem", filter);
            }

            if (!namespaceManager.SubscriptionExists("WebsiteMessages", "Question"))
            {
                SqlFilter filter = new SqlFilter("FormType = 'Question'");
                namespaceManager.CreateSubscription("WebsiteMessages", "Question", filter);
            }
        }

        public void SaveSubscription(FormPost post)
        {
            repoForm.Insert(post);
            repoForm.SaveChanges();
        }
    }
}
