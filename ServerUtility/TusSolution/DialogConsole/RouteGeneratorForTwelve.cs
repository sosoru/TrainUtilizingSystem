﻿using System;
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
        public static IEnumerable<string> GetLoopA(bool inv, bool sub)
        {
            var list = new List<string>();
            list.AddRange(new[] { "AT1", "BAT1", });

            if (sub)
                list.AddRange(new[] { "AT3", "AT4S", "AT5S", "AT5-1", "AT6" });
            else
                list.AddRange(new[] { "AT3", "AT4", "AT5", "AT6" });

            list.AddRange(new[] { "BAT6", "AT7", "BAT7", "AT8", "BAT8", "AT9", "BAT9", "AT10P", "AT10", "BAT10", "AT11", "BAT11" });

            if (inv)
            {
                var lastisolate = list.Last();
                list.RemoveAt(list.Count - 1);

                list.Reverse();
                list.Add(lastisolate);
            }

            return list;
        }

        public static IEnumerable<string> GetLoopB(bool inv, bool sub)
        {
            var list = new List<string>();
            list.AddRange(new[] { "BT1", "BBT1", });

            if (sub)
                list.AddRange(new[] { "BT3", "BT4S", "BT5S", "BT5-1", "BT6", });
            else
                list.AddRange(new[] { "BT3", "BT4", "BT5", "BT6" });

            list.AddRange(new[] { "BBT6", "BT7", "BBT7", "BT8", "BBT8", "BT9", "BBT9", "BT10P", "BT10", "BBT10", "BT11", "BBT11", });

            if (inv)
            {
                var lastisolate = list.Last();
                list.RemoveAt(list.Count - 1);

                list.Reverse();
                list.Add(lastisolate);
            }

            return list;
        }

    }
}
