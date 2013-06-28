using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tus.Communication.Device.AvrComposed;
using Tus.TransControl.Base;

namespace TestProject.DataContract
{
    [TestClass]
    public class SerializeTest
    {
        public string JsonSerializeReadTest<T>(T target)
        {
            using (var ms = new MemoryStream())
            {
                var cnt = new DataContractJsonSerializer(typeof(T));
                cnt.WriteObject(ms, target);
                ms.Seek(0, SeekOrigin.Begin);
                return System.Text.Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public T JsonSerializeWriteTest<T>(string target)
        {
            using (var ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(target)))
            {
                var cnt = new DataContractJsonSerializer(typeof(T));
                return (T)cnt.ReadObject(ms);
            }
        }

        [TestMethod]
        public void SwitchStateDataContractTest()
        {
            var swstate = new SwitchState();
            swstate.DeadTime = 100;
            swstate.ChangingTime = 200;

            var result = JsonSerializeReadTest<SwitchState>(swstate);
            JsonSerializeWriteTest<SwitchState>(result);
        }

        [TestMethod]
        public void SwitchDataContractTest()
        {
            var sw = new Switch();
            sw.CurrentState.DeadTime = 100;
            sw.CurrentState.ChangingTime = 200;

            var result = JsonSerializeReadTest<Switch>(sw);
            JsonSerializeWriteTest<Switch>(result);
        }

        [TestMethod]
        public void MotorStateDataContractTest()
        {
            var mtrstate = new MotorState();

            var result = JsonSerializeReadTest<MotorState>(mtrstate);
            JsonSerializeWriteTest<MotorState>(result);
        }

        [TestMethod]
        public void MotorDataContractTest()
        {
            var mtr = new Motor();

            var result = JsonSerializeReadTest<Motor>(mtr);
            result = result.Replace(@"""IsDetected"":false,", ""); // IsDetected is not required parameter
            JsonSerializeWriteTest<Motor>(result);
        }

        [TestMethod]
        public void SensorStateDataContractTest()
        {
            var snsstate = new SensorState();

            var result = JsonSerializeReadTest<SensorState>(snsstate);
            result = result.Replace(@"""Voltage"":0,", "");
            JsonSerializeWriteTest<SensorState>(result);
        }

        [TestMethod]
        public void SensorDataContractTest()
        {
            var sns = new UsartSensor();

            var result = JsonSerializeReadTest<UsartSensor>(sns);
            result = result.Replace(@"""Voltage"":0,", "");
            JsonSerializeWriteTest<UsartSensor>(result);
        }

    }
}
