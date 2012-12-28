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
using SensorLibrary.Devices;
using Moq;
using System.Collections.Generic;
using Microsoft.Reactive.Testing;

namespace TestProject
{
    [TestClass]
    public class PacketServerTest
    {
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
            var serv = new PacketServer(new AvrDeviceFactoryProvider());
            serv.Controller = mockio.Object;

            this.Server = serv;
        }

        [TestMethod]
        public void TestMethod1()
        {
            var sendingsub = new Subject<DevicePacket>();
            this.Server.SendingObservable.Subscribe(sendingsub);

            var recvsub = new Subject<DevicePacket>();
            this.Server.ReceivingObservable.Subscribe(recvsub);

            this.Server.LoopStart(Scheduler.Default);

            var packet = new DevicePacket();
            sendingsub.OnNext(packet);

        }
    }
}
