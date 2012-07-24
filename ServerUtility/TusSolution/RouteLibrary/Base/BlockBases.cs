using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary;

namespace RouteLibrary.Base
{
    public class Route
    {
        public BlockInfo From { get; set; }
        public BlockInfo To { get; set; }
    }

    public class MotorInfo
    {
        public DeviceID Address { get; set; }
        public Route RoutePositive { get; set; }
        public Route RouteNegative { get; set; }
    }

    public class SwitchInfo
    {
        public DeviceID Address { get; set; }
        public Route DirStraight { get; set; }
        public Route DirCurved { get; set; }
    }

    public class SensorInfo
    {
        public IList<DeviceID> Addresses { get; set; }
    }

    public class BlockInfo
    {
        public string Name { get; set; }
        public IList<Route> Route { get; set; }
        public MotorInfo Motor { get; set; }
        public SwitchInfo Switch { get; set; }
        public SensorInfo Sensor { get; set; }
    }
}
