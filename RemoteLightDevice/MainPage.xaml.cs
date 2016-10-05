using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RemoteLightDevice
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static Boolean lightStatus;
        static DeviceClient deviceClient;
        static string iotHubUri = "remotelightcontrol.azure-devices.net";
        static string deviceId = "simulated-device";
        static string deviceKey = "RXyCB0EaGR8qBxj0ju5cj3wSBEHoWhkwMqdXpc9p5ow=";

        public MainPage()
        {
            this.InitializeComponent();
            lightStatus = false;
            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(deviceId, deviceKey));
            SendDeviceToCloudMessagesAsync();
            ReceiveC2dAsync();
        }

        private void btnToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            toogleLightStatus();
        }

        private void toogleLightStatus()
        {
            lightStatus = !lightStatus;
            updateLight();
        }

        private void updateLight()
        {
            light.Fill = new SolidColorBrush(lightStatus ? Colors.Blue : Colors.LightGray);
            SendDeviceToCloudMessagesAsync();
        }

        private static async void SendDeviceToCloudMessagesAsync()
        {

            var messagePayload = new
            {
                deviceId = deviceId,
                lightValue = lightStatus ? 1.0 : 0.0
            };

                var messageString = JsonConvert.SerializeObject(messagePayload);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                await deviceClient.SendEventAsync(message);
                Debug.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

                Task.Delay(1000).Wait();
        }

        private async void ReceiveC2dAsync()
        {
            Debug.WriteLine("\nReceiving cloud to device messages from service");
            while (true)
            {
                Message receivedMessage = await deviceClient.ReceiveAsync();
                if (receivedMessage == null) continue;

                var messageBytes = receivedMessage.GetBytes();

                var messageData = Encoding.ASCII.GetString(messageBytes);

                Debug.WriteLine("Received message: {0}", messageData);


                lightStatus = (int.Parse(messageData.ToString()) > 0);
                updateLight();

                await deviceClient.CompleteAsync(receivedMessage);
            }
        }
    }
}
