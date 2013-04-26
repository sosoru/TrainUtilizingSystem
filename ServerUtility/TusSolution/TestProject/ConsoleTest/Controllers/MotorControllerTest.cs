using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DialogConsole;
using System.Reactive;
using System.Reactive.Linq;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Reactive.Concurrency;
using System.Threading.Tasks;

using Tus.Communication;
using Tus.Communication.Device;
using Tus.Communication.Device.AvrComposed;

namespace TestProject.DialogController
{

    [TestClass]
    public class DialogMotorControllerTestHelper
        : DialogControllerTestHelper
    {

        [TestMethod]
        public void MotorControllerCurrentConfTest()
        {
            var mtr = new Motor();
            var state = new MotorState();

            var fnc = new Action<string>(cmd =>
                {
                    PrepareStateTest(cmd, (rst, wst) =>
                        {
                            var cnt = new MotorController(rst, wst);
                            cnt.confCurrent(state);
                        });
                });

            fnc("0.5");
            Assert.IsTrue(Math.Round(state.Current, 1) == 0.5);

            fnc("2.0");
            Assert.IsTrue(Math.Round(state.Current, 1) == 2.0);
        }

        [TestMethod]
        public void MotorControllerDutyConfTest()
        {
            var mtr = new Motor();
            var state = new MotorState();

            var fnc = new Action<string>(cmd =>
                {
                    PrepareStateTest(cmd, (rst, wst) =>
                        {
                            var cnt = new MotorController(rst, wst);
                            cnt.confDuty(state);
                        });
                });

            fnc("0.5");
            Assert.IsTrue(Math.Round(state.Duty, 1) == 0.5);

            fnc("2.0");
            Assert.IsTrue(Math.Round(state.Duty, 1) == 0.5); // not changed
        }

        [TestMethod]
        public void MotorControllerModeStateConfTest()
        {
            var mtr = new Motor();
            var state = new MotorState();

            var fnc = new Action<string>(cmd =>
                {
                    PrepareStateTest(cmd, (rst, wst) =>
                        {
                            var cnt = new MotorController(rst, wst);
                            cnt.confMode(state);
                        });
                });

            fnc("duty");
            Assert.IsTrue(state.ControlMode == MotorControlMode.DutySpecifiedMode);

            fnc("curr");
            Assert.IsTrue(state.ControlMode == MotorControlMode.CurrentFeedBackMode);
        }

        [TestMethod]
        public void MotorControllerDirectionStateConfTest()
        {
            var mtr = new Motor();
            var state = new MotorState();

            var fnc = new Action<string>(cmd =>
            {
                PrepareStateTest(cmd, (stread, stwrite) =>
                    {
                        var cnt = new MotorController(stread, stwrite);
                        cnt.confDirection(state);
                    });
            });

            fnc("pos");
            Assert.IsTrue(state.Direction == MotorDirection.Positive);

            fnc("stb");
            Assert.IsTrue(state.Direction == MotorDirection.Standby);

            fnc("neg");
            Assert.IsTrue(state.Direction == MotorDirection.Negative);

        }
    }
}
