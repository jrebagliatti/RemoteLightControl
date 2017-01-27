using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Devices;
using System.Threading.Tasks;
using System.Text;

namespace RemoteLightControl.Shared
{
    public class CloudToDeviceManager
    {
        static ServiceClient serviceClient;
        static string connectionString = "HostName=remotelightcontrol.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=liSFDuYnBXogLsPbPJVZMvo4plkl4AfrP73XSnooGjc=";

        public CloudToDeviceManager()
        {
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
        }

        public async Task SendCloudToDeviceMessageAsync(String device, String message)
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes(message));
            await serviceClient.SendAsync(device, commandMessage);
        }
    }
}