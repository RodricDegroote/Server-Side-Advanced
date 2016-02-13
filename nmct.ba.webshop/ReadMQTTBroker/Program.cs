using CloudService;
using nmct.ba.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace ReadMQTTBroker
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                    MqttClient client = new MqttClient("172.30.28.5");

                    client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

                    string clientId = Guid.NewGuid().ToString();
                    client.Connect(clientId);

                    //client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
                    client.Subscribe(new string[] { "/home/temperature" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            }
            catch(Exception ex)
            {
                Console.WriteLine("Failed to connect: " + ex.Message);
            }


        }

        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            byte[] data = e.Message;
            string dataString = System.Text.Encoding.Default.GetString(data);

            string[] dataSplit = dataString.Split(';');
            Console.WriteLine("Location: " + dataSplit[0]);
            Console.WriteLine("Temperatuur: " + dataSplit[1]);
            Console.WriteLine("Time: " + dataSplit[2]);

            SchrijfDataWeg(dataSplit);
        }

        private static void SchrijfDataWeg(string[] dataSplit)
        {
            TemperatureEntity temp = new TemperatureEntity()
            {
                PartitionKey = "Temperatuur",
                RowKey = Guid.NewGuid().ToString(),
                Timestamp = DateTime.Now,
                Location = dataSplit[0],
                Temp = dataSplit[1],
                Time = dataSplit[2]
            };

            SaveTemperatuur(temp);
        }

        private static void SaveTemperatuur(TemperatureEntity temp)
        {
            AzureStorage.saveToTableStorage<TemperatureEntity>(temp, "StorageConnectionString", "Temperatuur");
        }

    }
}
