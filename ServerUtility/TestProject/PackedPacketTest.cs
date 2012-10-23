using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.IO.Ports;
using System.Reactive.Linq;
using System.Reactive;
using SensorLibrary;
using SensorLibrary.Packet;
using SensorLibrary.Packet.Data;
using SensorLibrary.Devices;
using SensorLibrary.Devices.TusAvrDevices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    [TestClass]
    public class PackedPacketTest
    {
        [TestMethod]
        public void PacketPackBasicTest()
        {
            var devA = new Motor () { DeviceID = new DeviceID(1,1,1)};
            var devB = new Switch() { DeviceID = new DeviceID(1,1,2)};

            devA.CurrentState.Duty = 0.5f;
            devA.CurrentState.Direction = MotorDirection.Negative;
            devA.CurrentState.Current = 1.0f;
                
            var packets = DevicePacket.CreatePackedPacket(new IDevice<IDeviceState<IPacketDeviceData>>[] { devA, devB }, new DeviceID(1, 1, 0));

            var extracts = packets.First().ExtractPackedPacket().ToArray() ;

        }

        [TestMethod]
        public void PacketPackCapacityOverTest()
        {
            var devs = Enumerable.Range(1, 8)
                .Select(i => new Switch() { DeviceID = new DeviceID(1, 1, (byte)i), });

            var packets = DevicePacket.CreatePackedPacket(devs, new DeviceID(1, 1, 0));
            var extracts = packets.SelectMany(p => p.ExtractPackedPacket())
                .OrderBy(state => state.Data.InternalAddr);

            foreach (var dev in devs)
            {
                dev.CurrentState.Data.InternalAddr = dev.DeviceID.InternalAddr;
                var res = extracts.First(p => p.Data.InternalAddr == dev.DeviceID.InternalAddr);

                Assert.IsTrue(dev.CurrentState.Data.ToByteArray().SequenceEqual(res.Data.ToByteArray()));

            }
        }
        
    }
}
