using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DialogConsole;
using SensorLibrary;
using SensorLibrary.Packet;
using SensorLibrary.Packet.Control;
using SensorLibrary.Packet.Data;
using SensorLibrary.Devices;
using SensorLibrary.Devices.TusAvrDevices;
using System.Reactive;
using System.Reactive.Linq;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Reactive.Concurrency;
using System.Threading.Tasks;

namespace TestProject.DialogController
{
    [TestClass]
    public class SwitchControllerTest
        : DialogControllerTests
    {
        [TestMethod]
        public void SwitchControllerPositionTest()
        {
            var sw = new Switch();
            var state = new SwitchState();

            var fnc = new Action<string>(cmd =>
            {
                PrepareStateTest(cmd, (rst, wst) =>
                    {
                        var cnt = new SwitchController(rst, wst);
                        state = cnt.ConfPosition(state);
                    });
            });

            fnc("s");
            Assert.IsTrue(state.Position == PointStateEnum.Straight);

            fnc("c");
            Assert.IsTrue(state.Position == PointStateEnum.Curve);

            fnc("a");
            Assert.IsTrue(state.Position == PointStateEnum.Any);
        }

        [TestMethod]
        public void SwitchControllerDeadTimeTest()
        {
            var sw = new Switch();
            var state = new SwitchState();

            var fnc = new Action<string>(cmd =>
            {
                PrepareStateTest(cmd, (rst, wst) =>
                    {
                        var cnt = new SwitchController(rst, wst);
                        state = cnt.ConfDeadTime(state);
                    });
            });

            fnc("200");
            Assert.IsTrue(state.DeadTime == 200);

            fnc("50");
            Assert.IsTrue(state.DeadTime == 200); // will be not changed
        }

        [TestMethod]
        public void SwitchControllerChangingTimeTest()
        {
            var sw = new Switch();
            var state = new SwitchState();

            var fnc = new Action<string>(cmd =>
            {
                PrepareStateTest(cmd, (rst, wst) =>
                    {
                        var cnt = new SwitchController(rst, wst);
                        state = cnt.ConfChangingTime(state);
                    });
            });

            fnc("200");
            Assert.IsTrue(state.ChangingTime == 200);

            fnc("-1");
            Assert.IsTrue(state.ChangingTime == 200); // will be not changed
        }
    }
}
