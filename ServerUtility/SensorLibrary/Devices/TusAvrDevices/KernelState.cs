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

    public class MemoryState
        : KernelState
    {
        public MemoryState()
            : base()
        {
        }

        public byte CurrentMemory
        {
            get { return this.Data.Content[0];}
            set { this.Data.Content[1];}
        }

        
}
