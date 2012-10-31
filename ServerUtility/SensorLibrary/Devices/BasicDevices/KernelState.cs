﻿using System;
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
            get { return this.Data.Content1}
            set { this.Data.Content1 = value; }
        }

        public byte MemoryLimit
        {
            get { return this.Data.Content2; }
            set { this.Data.Content2 = value; }
        }
   }

}
