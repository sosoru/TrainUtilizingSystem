using SensorLibrary.Packet;
using SensorLibrary.Packet.Data;
using SensorLibrary.Packet.IO;
using SensorLibrary.Packet.Control;
using SensorLibrary.Devices;
using SensorLibrary.Devices.TusAvrDevices;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Linq;
using SensorLibrary;
using Moq;
using System.Collections.Generic;
using Microsoft.Reactive.Testing;

namespace TestProject
{
    [TestClass]
    public class PacketServerTest
    {
        private IList<IDeviceState<IPacketDeviceData>> WrittenPackets { get; set; }
        private PacketServer Server { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            var mockio = new Mock<IDeviceIO>();
            var written = new List<IDeviceState<IPacketDeviceData>>();
            var received = new List<IDevice<IDeviceState<IPacketDeviceData>>>();
            mockio.Setup(e => e.GetReadingPacket()).Returns(DevicePacket.CreatePackedPacket(received).ToObservable());
            mockio.Setup(e => e.GetWritingPacket(It.IsAny<DevicePacket>())).Callback<DevicePacket>(pack =>
                written.AddRange(pack.ExtractPackedPacket())
                )
                .Returns(Observable.Empty<DevicePacket>());
            var serv = new PacketServer();
            serv.Controller = mockio.Object;

            this.WrittenPackets = written;
            this.Server = serv;
        }

        [TestMethod]
        public void TestMethod1()
        {
            this.Server.LoopStart(Scheduler.Default);

            var device = new Motor(this.Server);
            device.DeviceID = new DeviceID(1, 1, 1);
            this.Server.EnqueueState(device);

            this.Server.SendingObservable.Do(p => Console.WriteLine("pero")).Repeat(10).Subscribe();
        }
    }
}
