using System;
using System.Linq;

namespace Tus.AutoController
{
    public class LocalOperationPhaseBatchFactory
        : PhaseBatchFactory
    {
        public override PhaseBatch Create()
        {
            var p1 = new Phase("走行P")
            {
                Speed = 1,
                Accelation = 0.5,
                TriggerInitializer = t => t.BlockReached("AT1")
            };

            var p2 = new Phase("減速準備P")
            {
                Speed = 1,
                Accelation = 1.0,
                TriggerInitializer = t => t.WaitByTime(TimeSpan.FromSeconds(5)),
                StayGoSignal = true
            };

            var p3 = new Phase("減速P")
            {
                Speed = 0,
                Accelation = 0.05,
                TriggerInitializer = t => t.SpeedReached(0.0f)
            };

            var p4 = new Phase("停止P")
            {
                Speed = 0,
                Accelation = 1.0,
                TriggerInitializer = t => t.WaitByTime(TimeSpan.FromSeconds(10)),
                StayGoSignal = true
            };

            var p5 = new Phase("発進P")
            {
                Speed = 1,
                Accelation = 0.2,
                TriggerInitializer = t => t.SpeedReached(1.0f)
            };

            return new PhaseBatch() { Phases = new[] { p1, p2, p3, p4, p5 }.ToList() };

        }
    }
}