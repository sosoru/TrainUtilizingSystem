using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Tus.Communication;
using Tus.Communication.Device.AvrComposed;

namespace Tus.Route
{
    public class RouteSegmentInfo
    {
        public BlockInfo From { get; set; }
        public BlockInfo To { get; set; }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (RouteSegmentInfo)obj;
            bool eqfrom = false;
            if (this.From == null && other.From == null)
                eqfrom = true;
            else if (this.From == null || other.From == null)
                eqfrom = false;
            else
                eqfrom = this.From.Equals(other.From);

            bool eqto = false;
            if (this.To == null && other.To == null)
                eqto = true;
            else if (this.To == null || other.To == null)
                eqto = false;
            else
                eqto = this.To.Equals(other.To);

            return eqfrom && eqto;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return this.From.GetHashCode() ^ this.To.GetHashCode();
        }
    }

    public class RouteSegment
    {
        public RouteSegment(Block from, Block to)
        {
            this.From = from;
            this.To = to;
        }

        public bool IsFromAny { get { return this.From == null; } }
        public bool IsToAny { get { return this.To == null; } }

        public Block From { get; private set; }
        public Block To { get; private set; }

        private RouteSegmentInfo info_ = null;
        public RouteSegmentInfo Info
        {
            get
            {
                if (this.info_ == null)
                {
                    info_ = new RouteSegmentInfo()
                    {
                        From = (this.From != null) ? this.From.info : null,
                        To = (this.To != null) ? this.To.info : null,
                    };
                }
                return this.info_;
            }
        }


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

        public MotorDirection SelectDirection(RouteSegmentInfo info)
        {
            if (info.Equals(this.RoutePositive))
                return MotorDirection.Positive;
            else if (info.Equals(this.RouteNegative))
                return MotorDirection.Negative;
            else
                throw new InvalidOperationException("direction undecidable");
        }
    }

    public class SwitchInfo
        : DeviceInfo
    {
        public IEnumerable<RouteSegmentInfo> DirStraight { get; set; }
        public IEnumerable<RouteSegmentInfo> DirCurved { get; set; }
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
        public bool IsIsolated { get; set; }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var info = (BlockInfo)obj;
            return this.Name.Equals(info.Name);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return this.Name.GetHashCode();
        }
    }
}
