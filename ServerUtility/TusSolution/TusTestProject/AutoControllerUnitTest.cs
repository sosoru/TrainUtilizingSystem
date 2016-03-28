using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Runtime.Serialization.Json;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tus.Communication;
using Tus.TransControl.Base;
using Tus.AutoController;
using Tus.AutoController.Deserialized;
using Block = Tus.TransControl.Base.Block;

namespace TusTestProject.AutoController
{
    [TestClass]
    public class AutoControllerUnitTest
    {
        [TestMethod]
        public void Construct()
        {
            var unten = new Unten();
            var phaseBatch = new PhaseBatch();
            var phase = new Phase();
            var trigger = new Trigger();

        }

        [TestMethod]
        public void PhaseEvaluateTest()
        {
            var phase = new Phase();
            var moqTrigger = new Moq.Mock<Trigger>();

            moqTrigger.Setup<bool>(m => m.CheckTriggered()).Returns(true);

            phase.TriggerFactory = new TriggerFactory();
            phase.TriggerInitializer = t => moqTrigger.Object;
            phase.InitializeTrigger(); // InitializeTriggergger()を読んでTriggerを初期化しないと，正しく評価できない
            Assert.IsTrue(phase.Evaluate());
        }

        [TestMethod]
        public void PhaseBatchTest()
        {
            var batch = new PhaseBatch();
            batch.Phases = Enumerable.Range(0, 3).Select(i => new Phase(i.ToString())).ToList();

            batch.SetInitialPhase();
            Assert.AreEqual(batch.CurrentPhase.Name, "0");

            batch.StepNextPhase();
            Assert.AreEqual(batch.CurrentPhase.Name, "1");

            batch.StepNextPhase();
            Assert.AreEqual(batch.CurrentPhase.Name, "2");

            batch.StepNextPhase();
            Assert.AreEqual(batch.CurrentPhase.Name, "0"); // cyclic

        }

        [TestMethod]
        public void UntenTest()
        {
            var batch = new PhaseBatch();

            // Triggerを使う際にはファクトリをPhaseに渡しておかなければならない
            var triggerFactory = new TriggerFactory()
            {
                VehicleDefaultName = "pero",
                VehiclesProvider = VehiclesProvider.ByEnumerable(),
            };
            batch.Phases = Enumerable.Range(0, 3).Select(i => new Phase(i.ToString())
            {
                TriggerFactory = triggerFactory,
                TriggerInitializer = t => t.SpeedReached(0.5f),
            }).ToList();

            batch.SetInitialPhase();
            batch.CurrentPhase.Evaluate(); // batchは内部でTriggerを初期化してる
        }

        [TestMethod]
        public void PhaseBatchFactoryTest()
        {
            var batch = new LocalOperationPhaseBatchFactory().Create();

            // Triggerを使う際にはファクトリをPhaseに渡しておかなければならない
            var triggerFactory = new TriggerFactory()
            {
                VehicleDefaultName = "pero",
                VehiclesProvider = VehiclesProvider.ByEnumerable(),
            };
            foreach (var p in batch.Phases) p.TriggerFactory = triggerFactory;
            batch.SetInitialPhase();
            batch.CurrentPhase.InitializeTrigger();
            batch.CurrentPhase.Evaluate();
        }

        [TestMethod]
        public void StayGoSignalTest()
        {
            // 閉塞によって停止しているとき，列車をあたかも停止信号で止まっているように見せかけたい場合がある．
            // このようなときは，Triggerの判定にかかわらず，先行列車が次の閉塞を抜けるまで待機すれば良い．
            // StayGoSignalをTrueにすることで，VehicleのDistanceが2以上になるまで，UntenはPhaseを先へ進めない．

            var batch = new PhaseBatch()
            {
                Phases = new Phase[]
                                     {
                                         new Phase("p1")
                                         {
                                             Speed = 1.0f,
                                             StayGoSignal = true,
                                             TriggerInitializer = t => t.SpeedReached(1.0f)
                                         },
                                         new Phase("p2")
                                         {
                                             Speed = 0.0f,
                                             StayGoSignal = false,
                                             TriggerInitializer = t => t.SpeedReached(0.0f)
                                         },
                                     }.ToList(),
            };
            // target vehicle
            var vehiclename = "vehi";
            var v1 = new Moq.Mock<DeserializedVehicle>();
            v1.Object.Name = vehiclename;

            var unten = new Unten()
            {
                PhaseBatch = batch,
                VehicleName = vehiclename,
                VehiclesProvider = VehiclesProvider.ByEnumerable(v1.Object),
            };
            unten.InitializeWatching();

            v1.SetupGet<float>(v => v.CurrentSpeed).Returns(0.9f); // not reached
            v1.SetupGet<int>(v => v.Distance).Returns(2); // go signal
            Assert.IsFalse(unten.ContinueWatching());

            v1.SetupGet<float>(v => v.CurrentSpeed).Returns(1.0f); // reached
            v1.SetupGet<int>(v => v.Distance).Returns(1); ; // stop signal
            Assert.IsFalse(unten.ContinueWatching()); // 'staygosignal' tied this phase

            v1.SetupGet<float>(v => v.CurrentSpeed).Returns(1.0f); // reached
            v1.SetupGet<int>(v => v.Distance).Returns(2); ; // changed to go signal
            Assert.IsTrue(unten.ContinueWatching());  // step to the next phase
            Assert.AreEqual(unten.PhaseBatch.CurrentPhase.Name, "p2");

            v1.SetupGet<float>(v => v.CurrentSpeed).Returns(0.9f); // not reached
            v1.SetupGet<int>(v => v.Distance).Returns(2); // go signal
            Assert.IsFalse(unten.ContinueWatching());

            v1.SetupGet<float>(v => v.CurrentSpeed).Returns(0.0f); // reached
            v1.SetupGet<int>(v => v.Distance).Returns(1); ; // stop signal
            Assert.IsTrue(unten.ContinueWatching()); // 'staygosignal' is FALSE, step to the next
            Assert.AreEqual(unten.PhaseBatch.CurrentPhase.Name, "p1");

            unten.AbortWatching();
        }

        [TestMethod]
        public void PhaseFactoryConstruct()
        {
            var factory = new PhaseBatchFactory();

            factory.Create();
        }

        [TestMethod]
        public void TriggerBlockReached()
        {
            var factory = new TriggerFactory();

            // target vehicle
            var vehiclename = "vehi";
            var v1 = new Moq.Mock<DeserializedVehicle>();
            v1.Object.Name = vehiclename;
            factory.VehiclesProvider = VehiclesProvider.ByEnumerable(v1.Object);

            // false block
            var b1 = createMockBlock("AT0");
            // target block
            var b2 = createMockBlock("AT1");

            var t = factory.BlockReached(vehiclename, "AT1");

            // if vehicle is not prepared, then return false;
            Assert.IsFalse(t.CheckTriggered());

            // set false block, returning false
            v1.SetupGet(v => v.CurrentBlockObject).Returns(b1.Object);
            Assert.IsFalse(t.CheckTriggered());

            // set true block, returning true
            v1.SetupGet(v => v.CurrentBlockObject).Returns(b2.Object);
            Assert.IsTrue(t.CheckTriggered());
        }

        [TestMethod]
        public void TriggerWaitByTime()
        {
            var factory = new TriggerFactory();

            var instancedTime = DateTime.Now;
            var trigger = factory.WaitByTime(TimeSpan.FromSeconds(1)); // 現在時刻から5秒後にスケジュール
            Assert.IsTrue((trigger.ScheduledDateTime - instancedTime).TotalMilliseconds > 500); // 設定された時間だけ設定されていれば良い（500ミリ秒のマージン込み）

            Assert.IsFalse(trigger.CheckTriggered()); // returns false if not comming scheduled time
            Thread.Sleep(1000); // waiting
            Assert.IsTrue(trigger.CheckTriggered()); // scheduled time is come, returning true

        }

        [TestMethod]
        public void TriggerSpeedReached()
        {
            var factory = new TriggerFactory();

            // target vehicle
            var vehiclename = "vehicle";
            var v1 = new Moq.Mock<DeserializedVehicle>();
            v1.Object.Name = vehiclename;
            factory.VehiclesProvider = VehiclesProvider.ByEnumerable(v1.Object); // set vehicles func to factory

            var trigger = factory.SpeedReached("vehi", 0.5f, 0.01f);

            v1.SetupGet<float>(v => v.CurrentSpeed).Returns(0.4f);
            Assert.IsFalse(trigger.CheckTriggered()); // if vehicle's speed is not reached to the spicified value, returning false.
            v1.SetupGet<float>(v => v.CurrentSpeed).Returns(0.5f);
            Assert.IsTrue(trigger.CheckTriggered()); // if the speed is reached, returning true.
            v1.SetupGet<float>(v => v.CurrentSpeed).Returns(0.505f);
            Assert.IsTrue(trigger.CheckTriggered()); // if the speed is reached in the error range, returning true.
            v1.SetupGet<float>(v => v.CurrentSpeed).Returns(0.495f);
            Assert.IsTrue(trigger.CheckTriggered()); // if the speed is reached in the error range, returning true.

            factory.VehiclesProvider = VehiclesProvider.ByEnumerable();
            trigger = factory.SpeedReached("hoge", 0.5f);
            Assert.IsFalse(trigger.CheckTriggered()); // if vehicle status doesnt contains target vehicle, returning false;
        }

        [TestMethod]
        public void ReceiveVehicleInfoFromWebTest()
        {
            Assert.Inconclusive();
            var location = new Uri("http://192.168.2.9:8012");
            var client = new WebClient();
            client.DownloadString(location);
            var cnt = new DataContractJsonSerializer(typeof(DeserializedVehicle[]));
            using (var ms = new MemoryStream())
            {
                var obj = cnt.ReadObject(ms) as DeserializedVehicle[];

                new VehiclesProvider() { VehiclesStatus = () => obj, };
            }

        }

        private static Mock<DeserializedBlock> createMockBlock(string blockname)
        {
            var b1 = new Moq.Mock<DeserializedBlock>();
            b1.SetupGet<string>(b => b.Name).Returns(blockname);
            return b1;
        }


        [TestMethod]
        public void UntenSerializeTest()
        {
            var unten = new Unten()
            {
                VehicleName = "pero",
                VehiclesProvider = new VehiclesProvider(),
                PhaseBatch = new LocalOperationPhaseBatchFactory().Create(),
            };
            unten.RecreatePhaseTriggers();
            var ser = unten.Serializer;
            using (var ms = new MemoryStream())
            {
                ser.WriteObject(ms, unten);
                var str = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                Console.WriteLine(str);

            }

        }
    }
}
