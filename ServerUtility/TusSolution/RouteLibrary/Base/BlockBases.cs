using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary;

namespace RouteLibrary.Base
{
    public class RouteSegmentInfo
    {
        public BlockInfo From { get; set; }
        public BlockInfo To { get; set; }
    }

    public class RouteSegment
    {
        public RouteSegment(Block from, Block to)
        {
            this.From = from;
            this.To = to;
        }

        public Block From { get; private set; }
        public Block To { get; private set; }
    }

    public class DeviceInfo
    {
        public DeviceID Address { get; set; }
    }

    public class MotorInfo
        : DeviceInfo
    {
        public RouteSegmentInfo RoutePositive { get; set; }
        public RouteSegmentInfo RouteNegative { get; set; }
    }

    public class SwitchInfo
        : DeviceInfo
    {
        public RouteSegmentInfo DirStraight { get; set; }
        public RouteSegmentInfo DirCurved { get; set; }
    }

    public class SensorInfo
    {
        public IList<DeviceID> Addresses { get; set; }
    }

    public class BlockInfo
    {
        public string Name { get; set; }
        public IList<RouteSegmentInfo> Route { get; set; }
        public MotorInfo Motor { get; set; }
        public SwitchInfo Switch { get; set; }
        public SensorInfo Sensor { get; set; }
    }
}
