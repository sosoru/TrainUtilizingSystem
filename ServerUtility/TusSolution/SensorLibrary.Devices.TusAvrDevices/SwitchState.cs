using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using Tus.Communication;

namespace Tus.Communication.Device.AvrComposed
{
    [DataContract]
    public class SwitchState
        : DeviceState<SwitchData>
    {
        public SwitchState()
            : base()
        {
            this.Data = new SwitchData();
        }

        //unit : msec
        [DataMember(IsRequired = true)]
        public int DeadTime
        {
            get
            {
                return this.Data.DeadTime + 100;
            }
            set
            {
                var raw = value - 100;
                if (raw < 0 || raw > 255)
                    throw new ArgumentOutOfRangeException("DeadTime must be in [100, 355]");

                this.Data.DeadTime = (byte)raw;
            }
        }

        [DataMember(IsRequired = true)]
        public int ChangingTime
        {
            get
            {
                return this.Data.ChangingTime * 10;
            }
            set
            {
                var raw = Math.Round(((float)value) / 10.0f);
                if (raw <= 0 || raw > 100)
                    throw new ArgumentOutOfRangeException("ChangingTime must be in [10, 1005]");

                this.Data.ChangingTime = (byte)raw;
            }
        }

        [IgnoreDataMember]
        public PointStateEnum Position
        {
            get
            {
                //var pt = (PointStateEnum)this.Data.Position;

                //if (pt == PointStateEnum.Curve)
                //    return PointStateEnum.Straight;
                //else if (pt == PointStateEnum.Straight)
                //    return PointStateEnum.Curve;
                //else
                //    return PointStateEnum.Any;
                return (PointStateEnum)this.Data.Position;
            }
            set
            {
                //byte pt = (byte)PointStateEnum.Any;

                //if (value == PointStateEnum.Straight)
                //    pt = (byte)PointStateEnum.Curve;
                //else if(value == PointStateEnum.Curve)
                //    pt = (byte) PointStateEnum.Straight;

                this.Data.Position = (byte)value;
            }
        }

        [DataMember(Name="Position")]
        public string PositionString
        {
            get { return Enum.GetName(typeof(PointStateEnum), this.Position); }
            set
            {
                this.Position = (PointStateEnum)Enum.Parse(typeof(PointStateEnum), value);
            }
        }

        public override string ToString()
        {
            return base.ToString() + string.Format("|sw : {0} ", Enum.GetName(typeof(PointStateEnum), this.Position));
        }

    }
}
