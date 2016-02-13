using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using uPLibrary.Networking.M2Mqtt;

namespace SendToMQQTBroker
{
    class Program
    {
        private static MqttClient localClient;
        static void Main(string[] args)
        {
            try
            {
                localClient = new uPLibrary.Networking.M2Mqtt.MqttClient("172.30.28.5");
                string clientId = Guid.NewGuid().ToString();
                localClient.Connect(clientId);

                XmlDocument doc = new XmlDocument();
                doc.Load("\\Users\\Rodric\\Desktop\\values.xml");


                XmlNodeList elementList = doc.GetElementsByTagName("book");
                XmlNode node = elementList[1];

                for (int i = 0; i < elementList.Count; i++)
                {
                    string loc = elementList[i]["location"].InnerText;
                    string temp = elementList[i]["temperature"].InnerText;
                    string time = elementList[i]["time"].InnerText;

                    string allData = loc + ";" + temp + ";" + time;
                    byte[] data = Encoding.UTF8.GetBytes(allData);

                    localClient.Publish("/home/temperature", Encoding.UTF8.GetBytes(allData), uPLibrary.Networking.M2Mqtt.Messages.MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection Failed: " + ex.Message);
            }


        }
    }
}
