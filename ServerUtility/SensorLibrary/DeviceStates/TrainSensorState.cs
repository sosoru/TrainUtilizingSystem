using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace SensorLibrary
{
    public class TrainSensorState
    : DeviceState<TrainSensorData>
    {
        public TrainSensorState(DevicePacket pack, TrainSensorData data, PacketServer server)
            : base(pack, data, server)
        {
            if (pack.ModuleType != ModuleTypeEnum.TrainSensor)
                throw new ArgumentException("incorrect module type");
        }

        public TrainSensorState(DevicePacket pack)
            : this(pack, null, null) { }

        public override string ToString()
        {
            string ret = string.Format("Timer = {0}, V = {1}", Timer, CurrentVoltage);
            switch (Mode)
            {
                case TrainSensorMode.meisuring:
                    break;
                case TrainSensorMode.detecting:
                    ret += string.Format(", IsDetected = {0}, Threshold = {1}", IsDetected, ThresholdVoltage);
                    break;
            }
            return ret;
        }

        public TrainSensorMode Mode
        {
            get { return this.Data.Mode; }
            set { this.Data.Mode = value; }
        }

        public ushort Timer
        {
            get { return this.Data.Timer; }
            set { this.Data.Timer = value; }
        }

        public ushort OverflowedCount
        {
            get
            {
                if (this.Mode == TrainSensorMode.detecting)
                    return this.Data.OverflowedCount;
                else
                    throw new InvalidOperationException("Overflowed count is invalid except detecting mode.");
            }
            set
            {
                this.Data.OverflowedCount = value;
            }
        }

        public bool IsDetected
        {
            get
            {
                if (this.Data.Mode != TrainSensorMode.detecting)
                    throw new InvalidOperationException("invalid operation unless Mode is Detecting");
                return this.Data.IsDetected != 0;
            }
            set
            {
                if (this.Data.Mode != TrainSensorMode.detecting)
                    throw new InvalidOperationException("invalid operation unless Mode is Detecting");
                this.Data.IsDetected = (value) ? (byte)1 : (byte)0;
            }
        }

        private float convertVoltage(ushort resolving)
        {
            return (float)this.Data.ReferenceVoltageMinus + ((float)(this.Data.ReferenceVoltagePlus - this.Data.ReferenceVoltageMinus) * ((float)resolving / (float)(1 << this.Data.VoltageResolution)));
        }

        private ushort convertResolving(float voltage)
        {
            return (ushort)((voltage - (float)this.Data.ReferenceVoltageMinus) / (float)(this.Data.ReferenceVoltagePlus - this.Data.ReferenceVoltageMinus) * (float)(1 << this.Data.VoltageResolution));
        }

        public float ThresholdVoltage
        {
            get
            {
                return convertVoltage(this.Data.DeviceThresholdVoltage);
            }
            set
            {
                this.Data.DeviceThresholdVoltage = convertResolving(value);
            }
        }

        public float CurrentVoltage
        {
            get
            {
                return convertVoltage(this.Data.DeviceCurrentVoltage);
            }
            set
            {
                this.Data.DeviceCurrentVoltage = convertResolving(value);
            }
        }

        public float ReferenceVoltagePlus
        {
            get
            {
                return (float)this.Data.ReferenceVoltagePlus;
            }
            set
            {
                this.Data.ReferenceVoltagePlus = (byte)value;
            }
        }

        public float ReferenceVoltageMinus
        {
            get
            {
                return (float)this.Data.ReferenceVoltageMinus;
            }
            set
            {
                this.Data.ReferenceVoltageMinus = (byte)value;
            }
        }

        public byte VoltageResolution
        {
            get
            {
                return this.Data.VoltageResolution;
            }
            set
            {
                this.Data.VoltageResolution = value;
            }
        }
    }

}

