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

        RouteOrder GetRouteOrderFirst(BlockSheet sht)
        {
            var route = new RouteOrder(sht, new[] {
                "AT2", "AT3", "AT4", "AT5", "AT6", "BAT6",
                "AT7", "AT8","AT9", "BAT9",
                "AT10", "AT11", "AT12", "BAT12",
                "AT13", "AT14", "AT15", "BAT16",
                "AT16", "AT1", "BAT1" });
            route.IsRepeatable = true;
            return route;
        }
        private List<IDeviceState<IPacketDeviceData>> written;
        private PacketServer serv;
        private BlockSheet sheet;
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

            //var mocksht = new Mock<BlockSheet>(target_sheet, serv);
            //mocksht.Setup(sht => sht.Effect(It.IsAny<CommandFactory>(), It.IsAny<IEnumerable<Block>>()))
            //    .Callback<CommandFactory, IEnumerable<Block>>((f,bs) => this.lastcmdinfo = f);

            sheet = new BlockSheet(target_sheet, serv);

            this.scheduler = new TestScheduler();
            sheet.AssociatedScheduler = scheduler;
        }

        [TestMethod]
        public void SerializeAsJson()
        {
            var rt = GetRouteOrderFirst(sheet);

            var v = new Vehicle(sheet, rt);

            v.Run(0.5f, sheet.GetBlock("AT2"));

            var cnt = new DataContractJsonSerializer(typeof(Vehicle), new[] { typeof(Switch), typeof(UsartSensor), typeof(Motor), typeof(MemoryState) });

            using (var ms = new MemoryStream())
            {
                cnt.WriteObject(ms, v);

                var str = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                Console.WriteLine(str);
            }

        }
        [TestMethod]
        public void DeserializeVehicleTest()
        {
            var rt = GetRouteOrderFirst(sheet);

            var v = new Vehicle(sheet, rt);

            v.Run(0.5f, sheet.GetBlock("AT2"));

            var cnt = new DataContractJsonSerializer(typeof(Vehicle[]), new[] { typeof(Switch), typeof(UsartSensor), typeof(Motor), typeof(MemoryState) });
            var deserializedcnt = new DataContractJsonSerializer(typeof(Tus.AutoController.Deserialized.DeserializedVehicle[]));

            using (var ms = new MemoryStream())
            {
                cnt.WriteObject(ms, new[] { v, v });
                ms.Seek(0, SeekOrigin.Begin);

                var obj = deserializedcnt.ReadObject(ms);
            }
        }

        [TestMethod]
        public void PrintRouteString()
        {
            var rt = GetRouteOrderFirst(sheet);
            Console.WriteLine(rt.ToString());
        }

        [TestMethod]
        public void VehicleRouteAllocatingTest()
        {
            var rt = GetRouteOrderFirst(sheet);
            var v = new Vehicle(sheet, rt);
            var len = 1;
            var locklogicmock = new Mock<IRouteLockPredicator>();
            var block_entering = locklogicmock.Setup(l => l.ShouldLockNext(It.IsAny<Vehicle>()));
            var block_leaving = locklogicmock.Setup(l => l.ShouldReleaseBefore(It.IsAny<Vehicle>()));
            v.Predicators.Add(locklogicmock.Object);

            var halt = new Mock<Halt>();
            halt.Setup(h => h.HaltBlock).Returns(sheet.GetBlock("AT14"));
            halt.Setup(h => h.HaltState).Returns(true);

            v.Halt.Add(halt.Object);
            v.Length = len;

            //１，停車．Lenのみ確保．Reserveしない
            block_leaving.Returns(false);
            block_entering.Returns(false);
            v.Run(0.0f, "AT2");
            Assert.AreEqual(v.AssociatedRoute.LockedUnits.Count(), v.Length);
            Assert.AreEqual(v.AssociatedRoute.IsReserving, false);

            //２，進行開始．Len+1のみ確保．Reserveされてる
            block_leaving.Returns(false);
            block_entering.Returns(false);
            v.Run(1.0f);
            Assert.AreEqual(v.AssociatedRoute.LockedUnits.Count(), v.Length + 1);
            Assert.AreEqual(v.AssociatedRoute.IsReserving, true);

            //３，閉塞境界を通過．Len+1のみ確保．Reserveされてる．次の閉塞移っていること
            block_leaving.Returns(true);
            block_entering.Returns(true);
            v.Run(1.0f);
            Assert.AreEqual(v.AssociatedRoute.LockedUnits.Count(), v.Length + 1);
            Assert.AreEqual(v.AssociatedRoute.IsReserving, true);
            Assert.AreEqual(v.AssociatedRoute.HeadContainer.Unit.ControlBlock.Name, "AT9");

            //４，Haltに接近．Len+1のみ確保．Reserveされてる．次の閉塞に移っている．速度減少
            block_leaving.Returns(true);
            block_entering.Returns(true);
            v.Run(1.0f);
            Assert.AreEqual(v.AssociatedRoute.LockedUnits.Count(), v.Length + 1);
            Assert.AreEqual(v.AssociatedRoute.IsReserving, true);
            Assert.AreEqual(v.AssociatedRoute.HeadContainer.Unit.ControlBlock.Name, "AT12");

            //５，Haltと接触．Len+1のみ確保．Reserveしない．
            block_leaving.Returns(true);
            block_entering.Returns(true);
            v.Run(1.0f);
            Assert.AreEqual(v.AssociatedRoute.LockedUnits.Count(), v.Length);
            Assert.AreEqual(v.AssociatedRoute.IsReserving, false);
        }

        public void VehicleRunCheckTest()
        {
            var v1 = new Vehicle(sheet, GetRouteOrderFirst(sheet));
            var v2 = new Vehicle(sheet, GetRouteOrderFirst(sheet));

            v1.Run(0.5f, sheet.GetBlock("AT6"));
            Assert.IsTrue(CheckMtrMode("AT6", MotorMemoryStateEnum.Controlling));
            Assert.IsTrue(CheckMtrMode("AT9", MotorMemoryStateEnum.Waiting));

            v2.Run(0.5f, sheet.GetBlock("AT12"));

        }

        private bool CheckMtrMode(string blkname, MotorMemoryStateEnum state)
        {
            return this.lastcmdinfo.CreateCommand(sheet.GetBlock(blkname)).MotorMode == state;
        }
    }
}
