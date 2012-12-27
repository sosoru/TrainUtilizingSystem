using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
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
    using DialogConsole.Properties;

    [Export]
    class ServerFactory
    {
        [Import]
        public Settings ApplicationSettings;

        public PacketServer Create()
        {
            var ipbase = IPAddress.Parse(this.ApplicationSettings.IpSegment);
            var ipmask = IPAddress.Parse(this.ApplicationSettings.IpMask);

            var dialog = new DialogCnosole();
            var io = new TusEthernetIO(ipbase, ipmask)
            {
                SourceID = new DeviceIdParser().FromString(this.ApplicationSettings.ParentDeviceID).First(),
                Port = this.ApplicationSettings.IpPort,
            };

           var serv = new PacketServer(new AvrDeviceFactoryProvider());
           serv.Controller = io;

           return serv;
        }
    }
}
