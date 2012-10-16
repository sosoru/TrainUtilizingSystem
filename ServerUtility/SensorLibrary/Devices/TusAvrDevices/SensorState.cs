using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary.Packet.Data;

namespace SensorLibrary.Devices.TusAvrDevices
{
    public class SensorState
        : DeviceState<SensorData>
    {
        public SensorState()
            : base()
        {
            this.BasePacket = new DevicePacket();
            this.BasePacket.ModuleType = ModuleTypeEnum.AvrSensor;
            this.Data = new SensorData();
        }

        public float Voltage
        {
            get
            {
                return ((float)Math.Abs(this.Data.VoltageOn - this.Data.VoltageOff)) / 255.0f;
            }
        }

        public float OnVoltage
        {
            get { return this.Data.VoltageOn / 255.0f; }
        }

        public float OffVoltage
        {
            get { return this.Data.VoltageOff / 255.0f; }
        }

        public float Threshold
        {
            get { return (float)this.Data.Threshold / 255.0f; }
            set { this.Data.Threshold = (byte)(value * 255.0f); }
        }
    }
}
