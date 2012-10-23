using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary.Packet.Data;

namespace SensorLibrary.Devices.TusAvrDevices
{
    public class KernelState
        : DeviceState<KernelData>
    {
        public KernelState()
            : base()
        {
            this.Data = new KernelData();

        }

        public KernelCommand Command
        {
            get { return this.Data.Command; }
            set { this.Data.Command = value; }
        }
    }
}
