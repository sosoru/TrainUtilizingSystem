using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.Net;

using SensorLibrary;
using SensorLibrary.Packet;
using SensorLibrary.Packet.Control;
using SensorLibrary.Packet.Data;
using SensorLibrary.Devices;
using SensorLibrary.Devices.TusAvrDevices;
using SensorLibrary.Packet.IO;

using RouteLibrary;
using RouteLibrary.Base;
using RouteLibrary.Parser;

namespace DialogConsole.Factory
{
    [Export]
    class ServerFactory
    {
        public PacketServer Create()
        {
            var ipbase = IPAddress.Parse(DialogConsole.Properties.Settings.Default.IpSegment);
            var ipmask = IPAddress.Parse(DialogConsole.Properties.Settings.Default.IpMask);

            var dialog = new DialogCnosole();
            var io = new TusEthernetIO(ipbase, ipmask)
            {
                SourceID = new DeviceIdParser().FromString(DialogConsole.Properties.Settings.Default.MyDeviceID).First(),
                Port = DialogConsole.Properties.Settings.Default.IpPort,
            };

           var serv = new PacketServer(new AvrDeviceFactoryProvider());
           serv.Controller = io;

           return serv;
        }
    }
}
