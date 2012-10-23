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
            this.CurrentState = new KernelState();
        }

        public Kernel(PacketServer serv)
            : this()
        {
            this.CurrentState.ReceivingServer = serv;
        }

        public static Kernel InquiryState(DeviceID dev)
        {
            var k = new Kernel();

            k.DeviceID = dev;
            k.CurrentState.Command = KernelCommand.InquiryState;

            return k;
        }
    }
}
