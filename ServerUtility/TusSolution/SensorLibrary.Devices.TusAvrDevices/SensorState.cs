using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using Tus.Communication;

namespace Tus.Communication.Device.AvrComposed
{
    public class UsartSettingState
        : DeviceState<UsartSettingData>
    {
        public UsartSettingState()
            : base()
        {
            this.Data = new UsartSettingData();
        }

        public int ModuleCount
        {
            get { return (int)this.Data.ModuleCount; }
            set { this.Data.ModuleCount = (byte)value; }
        }
    }

    [DataContract]
    public class SensorState
        : DeviceState<SensorData>
    {
        public SensorState()
            : base()
        {
            this.Data = new SensorData();
        }

        [DataMember(IsRequired = false)]
        public float Voltage
        {
            get
            {
                return ((float)Math.Abs(this.Data.VoltageOn - this.Data.VoltageOff)) / 255.0f;
            }
            set { throw new NotImplementedException(); }
        }

        [DataMember]
        public float VoltageOn
        {
            get { return this.Data.VoltageOn / 255.0f; }
            set
            {
                if (value < 0.0f || 1.0f < value)
                {
                    throw new ArgumentOutOfRangeException("VoltageOn", value, "VoltageOn is [0, 1]");
                }
                else
                {
                    this.Data.VoltageOn = (byte)(value * 255.0f);
                }
            }
        }

        [DataMember]
        public float VoltageOff
        {
            get { return this.Data.VoltageOff / 255.0f; }
            set
            {
                if (value < 0.0f || 1.0f < value)
                {
                    throw new ArgumentOutOfRangeException("VoltageOff", value, "VoltageOff is [0, 1]");
                }
                else
                {
                    this.Data.VoltageOff = (byte)(value * 255.0f);
                }
            }
        }

        [DataMember]
        public float Threshold
        {
            get { return (float)this.Data.Threshold / 255.0f; }
            set
            {
                if (value < 0.0f || 1.0f < value)
                {
                    throw new ArgumentOutOfRangeException("Threshold", value, "Threshold is [0, 1]");
                }
                else
                {
                    this.Data.Threshold = (byte)(value * 255.0f);
                }
            }
        }
    }
}
