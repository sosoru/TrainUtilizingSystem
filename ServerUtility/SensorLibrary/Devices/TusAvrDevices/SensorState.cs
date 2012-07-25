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
        { }

        public float Voltage
        {
            get
            {
                return (float)this.Data.Voltage / 255.0f;
            }
            set
            {
                var raw = Math.Round(value * 255.0f);
                if (raw < 0 || raw > 255)
                    throw new ArgumentOutOfRangeException("Voltage must be in [0, 1]");

                this.Data.Voltage = (byte)raw;
            }
        }
    }
}
