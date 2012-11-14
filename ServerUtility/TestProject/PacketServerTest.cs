using SensorLibrary.Packet;
using SensorLibrary.Packet.Data;
using SensorLibrary.Packet.IO;
using SensorLibrary.Packet.Control;
using SensorLibrary.Devices;
using SensorLibrary.Devices.TusAvrDevices;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
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
            this.Server = new PacketServer(new AvrDeviceFactoryProvider());
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
