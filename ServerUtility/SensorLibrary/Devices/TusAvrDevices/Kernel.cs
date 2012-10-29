using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary.Packet.Control;
using SensorLibrary.Packet.Data;

namespace SensorLibrary.Devices.TusAvrDevices
{
    public class Kernel
        : Device<KernelState>
    {
        public Kernel()
            : base()
        {
            this.ModuleType = ModuleTypeEnum.AvrKernel;
        }

        public Kernel(PacketServer serv)
            : this()
        {
            this.CurrentState.ReceivingServer = serv;
        }

        public bool IsInquiryState
        {
            get { return this.CurrentState.Command == KernelCommand.InquiryState; }
        }

        public bool IsMemoryState
        {
            get { return this.CurrentState.Command == KernelCommand.MemoryState; }
        }

        public static Kernel InquiryState(DeviceID dev)
        {
            var k = new Kernel();

            k.DeviceID = dev;
            k.CurrentState = new InquiryState();

            return k;
        }

        public static Kernel MemoryState(DeviceID dev, MemoryState mem)
        {
            var k = new Kernel();

            k.DeviceID = dev;
            k.CurrentState = mem;

            return k;
        }
    }
}
