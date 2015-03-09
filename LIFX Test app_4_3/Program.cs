using System;
using Microsoft.SPOT;
using System.Collections;
using System.Threading;
using Microsoft.SPOT.Net.NetworkInformation;

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

        }

    }
}
