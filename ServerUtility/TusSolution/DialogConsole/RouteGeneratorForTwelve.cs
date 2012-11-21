using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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


namespace DialogConsole
{
    public class RouteGeneratorForTwelve
    {
        public IEnumerable<string> GetLoopA(bool inv, bool sub)
        {
            var list = new List<string>();
            list.AddRange(new[] { "AT1", "BAT1", });

            if (sub)
                list.AddRange(new[] { "AT3", "AT4S", "AT5S", "AT5-1", "AT6" });
            else
                list.AddRange(new[] { "AT3", "AT4", "AT5", "AT6" });

            list.AddRange(new[] { "BAT6", "AT7", "BAT7", "AT8", "BAT8", "AT9", "BAT9", "AT10P", "AT10", "BAT10", "AT11", "BAT11" });

            if(inv)
                list = list.Reverse();

            return list;
        }
    }
}
