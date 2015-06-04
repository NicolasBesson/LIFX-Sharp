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

                LifxBulb bulb = new LifxBulb(controller);
                Debug.Print("------------- Start -------------");
                Debug.Print("Bulb Address : " + bulb.MacAddress);
                Debug.Print("Bulb IP Address : " + bulb.IpEndpoint.Address.ToString());
                Debug.Print("Bulb Name : " + bulb.GetLabel());
                
                LifxPowerState lstatus = bulb.GetPowerState();
                Debug.Print("Current Bulb Power : " + lstatus.ToString());

                Debug.Print("Turn Off device");
                bulb.SetPowerState(LifxPowerState.Off);
                Debug.Print("Current Bulb Power : " + lstatus.ToString());

                Thread.Sleep(2000);

                Debug.Print("Turn Off device");
                bulb.SetPowerState(LifxPowerState.On);
                Debug.Print("Current Bulb Power : " + lstatus.ToString());

                Thread.Sleep(2000);

                Debug.Print("Change device color");
                bulb.SetColor(new LifxColor(Microsoft.SPOT.Presentation.Media.ColorUtility.ColorFromRGB(0, 0, 255), 400), 0);            
                Thread.Sleep(2000);

                Debug.Print("Change device color");
                bulb.SetColor(new LifxColor(Microsoft.SPOT.Presentation.Media.ColorUtility.ColorFromRGB(0, 255, 0), 400), 0);
                Thread.Sleep(2000);

                Debug.Print("Change device color");
                bulb.SetColor(new LifxColor(Microsoft.SPOT.Presentation.Media.ColorUtility.ColorFromRGB(255, 0, 0), 400), 0);
                Thread.Sleep(2000);

                Debug.Print("Change device color to white");
                bulb.SetColor(new LifxColor(Microsoft.SPOT.Presentation.Media.ColorUtility.ColorFromRGB(255, 255, 255), 400), 0);
                Thread.Sleep(2000);

                Debug.Print("Dimming down to 1");
                bulb.SetDimLevel(1, 3000);
                Debug.Print("Dimming up to 100");
                bulb.SetDimLevel(100, 3000);
                Thread.Sleep(2000);

                Debug.Print("Turn Off device");
                bulb.SetPowerState(LifxPowerState.Off);
                Debug.Print("------------- End -------------");

            }

            LifxCommunicator.Instance.CloseConnections();
        }

    }
}
