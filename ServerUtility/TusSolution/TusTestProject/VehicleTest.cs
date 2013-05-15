using Tus.TransControl.Parser;
using Tus.TransControl;
using Tus.TransControl.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Moq;
using Moq.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Reactive.Concurrency;
using Microsoft.Reactive.Testing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

using Tus.Communication;
using Tus.Communication.Device;
using Tus.Communication.Device.AvrComposed;
using Tus.TransControl.Base;
using Tus.TransControl.Parser;

namespace TestProject
{
    [TestClass]
    [DeploymentItem("SampleLayout/staloop.yaml")]
    public class VehicleTest
    {
        IEnumerable<BlockInfo> target_sheet
        {
            get
            {
                var yaml = new BlockYaml();
                var blocks = yaml.Parse("staloop.yaml");

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
        private CommandFactory lastcmdinfo;

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
            serv = new PacketServer();
            serv.Controller = mockio.Object;

            var mocksht = new Mock<BlockSheet>(target_sheet, serv);
            mocksht.Setup(sht => sht.Effect(It.IsAny<CommandFactory>(), It.IsAny<IEnumerable<Block>>()))
                .Callback<CommandFactory, IEnumerable<Block>>((f,bs) => this.lastcmdinfo = f);

            sht = new BlockSheet(target_sheet, serv);

            this.scheduler = new TestScheduler();
            sht.AssociatedScheduler = scheduler;
        }

        [TestMethod]
        public void SerializeAsJson()
        {
            var rt = GetRouteFirst(sht);

            var v = new Vehicle(sht, rt);

            v.CurrentBlock = sht.GetBlock("AT2");
            v.Speed = 0.5f;
            v.Run();

            var cnt = new DataContractJsonSerializer(typeof(Vehicle));

            using (var ms = new MemoryStream())
            {
                cnt.WriteObject(ms, v);

                var str = System.Text.UnicodeEncoding.UTF8.GetString(ms.ToArray());
                Console.WriteLine(str);
            }

        }

        [TestMethod]
        public void PrintRouteString()
        {
            var rt = GetRouteFirst(sht);
            Console.WriteLine(rt.ToString());
        }

        [TestMethod]
        public void VehicleRouteAllocatingTest()
        {
            var rt = GetRouteFirst(sht);
            var v = new Vehicle(sht, rt);

            var halt = new Mock<Halt>();
            halt.Setup(h => h.HaltBlock).Returns(sht.GetBlock("AT14"));
            halt.Setup(h => h.HaltState).Returns(true);

            v.CurrentBlock = sht.GetBlock("AT6");
            v.Speed = 1.0f;
            v.Length = 1;

            //１，停車時のVehicleのLengthはlen
            Assert.AreEqual<int>(v.CurrentLength, 1);

            //２，発車後，BlockをReleaseする条件（停車するとか，センサを通過するとか）を満たすまでVehicle.Lengthはlen+1を保つ
            v.CurrentBlock = sht.GetBlock("AT9");
            v.Run(); //閉塞を通過（1回目）
            Assert.AreEqual<int>(v.CurrentLength, 2); // RouteがRepetableでないから，打ち切って1が返る

            v.CurrentBlock = sht.GetBlock("AT12");
            v.Run(); //閉塞を通過（2回目）
            Assert.AreEqual<int>(v.CurrentLength, 2);

            v.Halt.Add(halt.Object);
            v.CurrentBlock = sht.GetBlock("AT14");
            v.Run(); //停車（haltable blockへの進入につき）
            Assert.AreEqual<int>(v.CurrentLength, 1);
        }

        public void VehicleRunCheckTest()
        {
            var v1 = new Vehicle(sht, GetRouteFirst(sht));
            var v2 = new Vehicle(sht, GetRouteFirst(sht));

            v1.CurrentBlock = sht.GetBlock("AT6");
            v1.Run();
            Assert.IsTrue(CheckMtrMode("AT6", MotorMemoryStateEnum.Controlling));
            Assert.IsTrue(CheckMtrMode("AT9", MotorMemoryStateEnum.Waiting));

            v2.CurrentBlock = sht.GetBlock("AT12");
            v2.Run(); 

        }

        private bool CheckMtrMode(string blkname, MotorMemoryStateEnum state)
        {
            return this.lastcmdinfo.CreateCommand(sht.GetBlock(blkname)).MotorMode == state;
        }
    }
}
