using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary.Packet.Data;

namespace SensorLibrary.Devices
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

        public override string ToString()
        {
            return string.Format("|kl : cmd={0}, data={1} {2} {3} {4}",
                    Enum.GetName(typeof(KernelCommand), this.Command),
                    this.Data.Content1.ToString("X2"),
                    this.Data.Content2.ToString("X2"),
                    this.Data.Content3.ToString("X2"),
                    this.Data.Content4.ToString("X2")
                    );
        }
    }

    public class InquiryState
        : KernelState
    {
        public InquiryState()
            : base()
        {
            this.Command = KernelCommand.InquiryState;
        }
    }

    public class MemoryState
        : KernelState
    {
        public MemoryState()
            : base()
        {
            this.Command = KernelCommand.MemoryState;
        }

        public MemoryState(int mem)
            : this()
        {
            this.CurrentMemory = (byte)mem;
        }

        public byte CurrentMemory
        {
            get { return this.Data.Content1; }
            set { this.Data.Content1 = value; }
        }

        public byte MemoryLimit
        {
            get { return this.Data.Content2; }
            set { this.Data.Content2 = value; }
        }
   }

}
