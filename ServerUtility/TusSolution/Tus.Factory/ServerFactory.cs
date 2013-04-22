using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Net;

using Tus.Communication;
using Tus.Communication.Device;

using Tus;
using Tus.TransControl.Parser;

namespace Tus.Factory
{

    [Export]
    public class ServerFactory : FactoryBase<PacketServer>
    {
        public override PacketServer Create()
        {
            var ipbase = IPAddress.Parse(this.ApplicationSettings.IpSegment);
            var ipmask = IPAddress.Parse(this.ApplicationSettings.IpMask);

            var io = new TusEthernetIO(ipbase, ipmask)
            {
                SourceID = DeviceIdParser.FromString(this.ApplicationSettings.ParentDeviceID).First(),
                Port = this.ApplicationSettings.IpPort,
            };

            var serv = new PacketServer();
            serv.Controller = io;

            return serv;
        }
    }
}
