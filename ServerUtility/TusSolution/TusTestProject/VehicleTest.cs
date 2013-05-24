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
            var len = 2;
            var locklogicmock = new Mock<IRouteLockPredicator>();
            locklogicmock.Setup(l => l.ShouldLockNext(It.IsAny<Vehicle>())).Returns(true);
            locklogicmock.Setup(l => l.ShouldReleaseBefore(It.IsAny<Vehicle>())).Returns(false);
            v.Predicators.Add(locklogicmock.Object);

            var halt = new Mock<Halt>();
            halt.Setup(h => h.HaltBlock).Returns(sheet.GetBlock("AT14"));
            halt.Setup(h => h.HaltState).Returns(true);

            v.Length = len;

            //１，停車時のVehicleのLengthはlen
            v.Run(0.0f, sheet.GetBlock("AT6"));
            Assert.AreEqual(v.Speed, 0.0f);
            Assert.AreEqual<int>(v.CurrentLength, len);

            //発車
            v.Speed = 0.5f;
            v.Refresh();
            Assert.AreEqual<int>(v.CurrentLength, len+1);

            //２，発車後，BlockをReleaseする条件（停車するとか，センサを通過するとか）を満たすまでVehicle.Lengthはlen+1を保つ
            //v.Run(0.5f, sheet.GetBlock("AT9")); //閉塞を通過（1回目）
            v.Refresh();
            Assert.AreEqual<int>(v.CurrentLength, len+2);

            //v.Run(0.5f, sheet.GetBlock("AT12")); //閉塞を通過（2回目）
            v.Refresh();
            Assert.AreEqual<int>(v.CurrentLength, len+2);

            v.Halt.Add(halt.Object);
            //v.Run(0.0f, sheet.GetBlock("AT14")); //停車（haltable blockへの進入につき）
            v.Predicators.Remove(locklogicmock.Object);
            v.Refresh();
            Assert.AreEqual<int>(v.CurrentLength, len+1);
            v.Refresh();
            Assert.AreEqual<int>(v.CurrentLength, len+1);
            v.Refresh();
            Assert.AreEqual<int>(v.CurrentLength, len+1);
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
