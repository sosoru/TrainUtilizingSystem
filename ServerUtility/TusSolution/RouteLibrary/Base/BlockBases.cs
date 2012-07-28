using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary;

namespace RouteLibrary.Base
{
    public class RouteSegment
    {
        public BlockInfo From { get; set; }
        public BlockInfo To { get; set; }
    }

    public class MotorInfo
    {
        public DeviceID Address { get; set; }
        public RouteSegment RoutePositive { get; set; }
        public RouteSegment RouteNegative { get; set; }
    }

    public class SwitchInfo
    {
        public DeviceID Address { get; set; }
        public RouteSegment DirStraight { get; set; }
        public RouteSegment DirCurved { get; set; }
    }

    public class SensorInfo
    {
        public IList<DeviceID> Addresses { get; set; }
    }

    public class BlockInfo
    {
        public string Name { get; set; }
        public IList<RouteSegment> Route { get; set; }
        public MotorInfo Motor { get; set; }
        public SwitchInfo Switch { get; set; }
        public SensorInfo Sensor { get; set; }
    }
}
