﻿using Tus.Route;
using Tus.Route.Parser;
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
using DialogConsole;

using Tus.Communication;
using Tus.Communication.Device;
using Tus.Communication.Device.AvrComposed;

namespace TestProject
{
    [TestClass]
    public class TwelveLayoutTest
    {

        private List<IDeviceState<IPacketDeviceData>> written;
        private PacketServer serv;
        private BlockSheet sht;
        private TestScheduler scheduler;

        IEnumerable<BlockInfo> target_sheet
        {
            get
            {
                var yaml = new BlockYaml();
                var blocks = yaml.Parse(@"C:\Users\Administrator\Desktop\12_layout.yaml");

                return blocks;
            }
        }

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
            sht = new BlockSheet(target_sheet, serv);

            this.scheduler = new TestScheduler();
            sht.AssociatedScheduler = scheduler;
        }

        [TestMethod]
        public void ReadSheetTest()
        {
            var sht = target_sheet;
            sht.ToArray();
        }

        private void LoopTest(Func<bool, bool, IEnumerable<string>> getloop)
        {
            var rttestfunc = new Action<bool, bool>((inv, sub) =>
            {
                var blocks = getloop(inv, sub).Select(s => this.sht.GetBlock(s));
                Assert.IsFalse(blocks.Any(b => b == null));
                var rt = new Route(blocks.ToList());
                rt.IsRepeatable = true;

                Assert.IsTrue(true);
            });

            rttestfunc(false, false);
            rttestfunc(true, false);
            rttestfunc(false, true);
            rttestfunc(true, true);
        }

        [TestMethod]
        public void LoopATest()
        {
            LoopTest(RouteGeneratorForTwelve.GetLoopA);
        }

        [TestMethod]
        public void LoopBTest()
        {
            LoopTest(RouteGeneratorForTwelve.GetLoopB);
        }

        [TestMethod]
        public void LoopCTest()
        {
            LoopTest(RouteGeneratorForTwelve.GetLoopC);
        }

        [TestMethod]
        public void LoopDTest()
        {
            LoopTest(RouteGeneratorForTwelve.GetLoopD);
        }
    }
}