using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus.Messaging;
using System.Threading;
using System.Net.Http;
using System.Configuration;

namespace IoTHubReader
{
    public class Functions
    {
        static string connectionString = "HostName=remotelightcontrol.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=liSFDuYnBXogLsPbPJVZMvo4plkl4AfrP73XSnooGjc=";
        static string iotHubD2cEndpoint = "messages/events";
        static EventHubClient eventHubClient;

        [NoAutomaticTriggerAttribute]
        public static async Task ProcessMethod()
        {
            Console.Out.WriteLine("Stablishing IoT Hub connection.");
            eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, iotHubD2cEndpoint);
            Console.Out.WriteLine("Connection to IoT Hub stablished.");

            var d2cPartitions = eventHubClient.GetRuntimeInformation().PartitionIds;

            var tasks = new List<Task>();
            foreach (string partition in d2cPartitions)
            {
                tasks.Add(ReceiveMessagesFromDeviceAsync(partition));
            }
            Task.WaitAll(tasks.ToArray());
        }

        private static async Task ReceiveMessagesFromDeviceAsync(string partition)
        {
            var eventHubReceiver = eventHubClient.GetDefaultConsumerGroup().CreateReceiver(partition, DateTime.UtcNow);
            while (true)
            {
                EventData eventData = await eventHubReceiver.ReceiveAsync();
                if (eventData == null) continue;

                string data = Encoding.UTF8.GetString(eventData.GetBytes());
                Console.WriteLine("Message received. Partition: {0} Data: '{1}'", partition, data);

                //float dataInt = float.Parse(data);
                float dataInt = 1;

                await ChangeLightStatusAsync(1, dataInt > 0);
            }
        }

        private static async Task ChangeLightStatusAsync(int lightId, bool value)
        {
            var httpClient = new HttpClient();

            var action = String.Format("{0}/{1}", "NotifyLightChange", lightId);
            var request = ConfigurationManager.AppSettings["ChangeLightStatusEndpoint"] + action;

            var response = await httpClient.PostAsync(request, null);

            Console.WriteLine(response.StatusCode.ToString());
        }
    }
}
