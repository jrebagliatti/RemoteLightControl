using Microsoft.Azure.Devices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RemoteLightControl
{
    public partial class Index : System.Web.UI.Page
    {
        static ServiceClient serviceClient;
        static string connectionString = "HostName=remotelightcontrol.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=liSFDuYnBXogLsPbPJVZMvo4plkl4AfrP73XSnooGjc=";
        static string deviceId = "simulated-device";

        protected void Page_Load(object sender, EventArgs e)
        {
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
            string filePath = Path.Combine(
                Environment.GetEnvironmentVariable("HOME"),
                @"data\lightStatus.dat");

            using (StreamReader sr = new StreamReader(filePath))
            {
                String line = sr.ReadToEnd();
                lblStatus.Text = line;
            }
        }

        protected async void btnOn_Click(object sender, EventArgs e)
        {
            await SendCloudToDeviceMessageAsync(deviceId, "1");
        }

        protected async void btnOff_Click(object sender, EventArgs e)
        {
            await SendCloudToDeviceMessageAsync(deviceId, "0");
        }

        private async Task SendCloudToDeviceMessageAsync(String device, String message)
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes(message));
            commandMessage.Ack = DeliveryAcknowledgement.Full;
            commandMessage.MessageId = Guid.NewGuid().ToString();

            await serviceClient.SendAsync(device, commandMessage);
        }
    }
}