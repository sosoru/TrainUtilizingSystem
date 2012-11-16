using RouteLibrary.Base;
using RouteLibrary.Parser;
using SensorLibrary.Packet;
using SensorLibrary.Packet.Data;
using SensorLibrary.Packet.IO;
using SensorLibrary.Packet.Control;
using SensorLibrary.Devices;
using SensorLibrary.Devices.TusAvrDevices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Moq;
using Moq.Linq;
using SensorLibrary;
using System.Reactive.Subjects;
using System.Threading;
using System.Reactive.Concurrency;
using Microsoft.Reactive.Testing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace TestProject
{
    [TestClass]
    public class VehicleTest
    {
        IEnumerable<BlockInfo> target_sheet
        {
            get
            {
                var yaml = new BlockYaml();
                var blocks = yaml.Parse("./SampleLayout/staloop.yaml");

                return blocks;
            }
        }

        Route GetRouteFirst(BlockSheet sht)
        {
            var route = new Route(sht, new[] {
                "AT2", "AT3", "AT4", "AT5", "AT6", "BAT6",
                "AT7", "AT8","AT9", "BAT9",
                "AT10", "AT11", "AT12", "BAT12",
                "AT13", "AT14", "AT15", "BAT16",
                "AT16", "AT1", "BAT1" });
            return route;
        }
        private List<IDeviceState<IPacketDeviceData>> written;
        private PacketServer serv;
        private BlockSheet sht;
        private TestScheduler scheduler;

        [TestInitialize]
        public void TestInitialize()
        {
            var mockio = new Mock<IDeviceIO>();
            written = new List<IDeviceState<IPacketDeviceData>>();
            mockio.Setup(e => e.GetReadingPacket()).Returns(Observable.Empty<DevicePacket>());
            mockio.Setup(e => e.GetWritingPacket(It.IsAny<DevicePacket>())).Callback<DevicePacket>(pack =>
                written.AddRange(pack.ExtractPackedPacket())
                )
                .Returns(Observable.Empty<DevicePacket>());
            serv = new PacketServer(new AvrDeviceFactoryProvider());
            serv.Controller = mockio.Object;
            sht = new BlockSheet(target_sheet, serv);

            this.scheduler = new TestScheduler();
            sht.AssociatedScheduler = scheduler;
        }
           
        [TestMethod]
        public void SerializeAsJson()
        {
            var rt = GetRouteFirst(sht);

            var v = new Vehicle(sht, rt);
            var cnt = new DataContractJsonSerializer(typeof(Vehicle));
            
            using(var ms = new MemoryStream())
            {
                cnt.WriteObject(ms, v);

                var str = System.Text.UnicodeEncoding.UTF8.GetString(ms.ToArray());
                Console.WriteLine(str);
            }
            
        }
    }
}
