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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RemoteLightDevice
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static Boolean lightStatus;
        public MainPage()
        {
            this.InitializeComponent();
            lightStatus = false;
        }

        private void btnToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            toogleLightStatus();
        }

        private void toogleLightStatus()
        {
            lightStatus = !lightStatus;
            light.Fill = new SolidColorBrush(lightStatus?Colors.Blue:Colors.LightGray);
        }
    }
}
