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
        public TrainSensorState()
            : base()
        {
        }

        public override string ToString()
        {
            string ret = string.Format("(Module={0}, Internal={1}, MPart={2})Timer = {3}, V = {4}",
                                        this.BasePacket.ID.ModuleAddr, this.BasePacket.ID.InternalAddr, this.BasePacket.ID.ModulePart,
                                        Timer, CurrentVoltage);
            switch (Mode)
            {
                case TrainSensorMode.meisuring:
                    break;
                case TrainSensorMode.detecting:
                    ret += string.Format(", IsDetected = {0}, Threshold = {1}", IsDetected, ThresholdVoltageLower);
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
                    return false;
                else
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
            try
            {

                return (float)this.Data.ReferenceVoltageMinus + ((float)(this.Data.ReferenceVoltagePlus - this.Data.ReferenceVoltageMinus) * ((float)resolving / (float)(1 << this.Data.VoltageResolution)));
            }
            catch { Console.WriteLine(); return 0.0f; }
        }

        private ushort convertResolving(float voltage)
        {
            return (ushort)((voltage - (float)this.Data.ReferenceVoltageMinus) / (float)(this.Data.ReferenceVoltagePlus - this.Data.ReferenceVoltageMinus) * (float)(1 << this.Data.VoltageResolution));
        }

        public float ThresholdVoltageLower
        {
            get
            {
                return convertVoltage((ushort)(this.Data.DeviceThresholdVoltageLower << 2));
            }
            set
            {
                this.Data.DeviceThresholdVoltageLower = (byte)(convertResolving(value) >> 2);
            }
        }

        public float ThresholdVoltageHigher
        {
            get
            {
                return convertVoltage((ushort)(this.Data.DeviceThresholdVoltageHigher << 2));
            }
            set
            {
                this.Data.DeviceThresholdVoltageHigher = (byte)(convertResolving(value) >> 2);
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

