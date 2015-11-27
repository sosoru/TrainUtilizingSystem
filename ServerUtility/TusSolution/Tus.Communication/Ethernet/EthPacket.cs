using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.IO.Ports;
using System.Reactive.Linq;
using System.Reactive;

namespace Tus.Communication.Ethernet
{
<<<<<<< HEAD
    [StructLayout(LayoutKind.Sequential, Size = 40)]
=======
    [StructLayout(LayoutKind.Sequential, Size = 104)]
>>>>>>> parent of 24cd721... DevicePacketのパケットサイズの調整
    public class EthPacket
    {
        public EthPacket()
        {
            this.DataPacket = new DevicePacket();
        }

        public DeviceID srcId;
        public DeviceID destId;

        public DevicePacket DataPacket;

    }
}
