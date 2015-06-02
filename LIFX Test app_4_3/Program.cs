using System;
using Microsoft.SPOT;
using System.Collections;
using System.Threading;
using Microsoft.SPOT.Net.NetworkInformation;
using Microsoft.SPOT.Presentation.Media;

using LifxLib;
using LifxLib.Messages;


namespace LIFX_Test_app_4_3
{
    public class Program
    {
        public static void Main()
        {
            NetworkInterface ni;
            // Wait for DHCP (on LWIP devices)
            while (true)
            {
                ni = NetworkInterface.GetAllNetworkInterfaces()[0];

                if (ni.IPAddress != "0.0.0.0") break;

                Thread.Sleep(1000);
            }

            LifxCommunicator.Instance.Initialize();
            ArrayList panController = LifxCommunicator.Instance.Discover();
            if (panController.Count == 0)
            {
                Debug.Print("Could not find any bulbs");
                return;
            }

            // Lets play with the controller
            foreach(LifxPanController controller in panController)
            {
                controller.SetPowerState(LifxPowerState.Off);
                Thread.Sleep(2000);
                controller.SetPowerState(LifxPowerState.On);
                Thread.Sleep(2000);
                controller.SetColor(new LifxColor(Microsoft.SPOT.Presentation.Media.ColorUtility.ColorFromRGB(0, 0, 255), 400), 0);
                Thread.Sleep(2000);
                controller.SetColor(new LifxColor(Microsoft.SPOT.Presentation.Media.ColorUtility.ColorFromRGB(0, 255, 0), 400), 0);
                Thread.Sleep(2000);
                controller.SetColor(new LifxColor(Microsoft.SPOT.Presentation.Media.ColorUtility.ColorFromRGB(255, 0, 0), 400), 0);
                Thread.Sleep(2000);
                controller.SetPowerState(LifxPowerState.Off);
            }

            LifxCommunicator.Instance.CloseConnections();
        }

    }
}
