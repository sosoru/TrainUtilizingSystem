using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive.Linq;
using System.Reactive.Concurrency;

using SensorLibrary;

namespace SensorLibrary.Manipulators
{
    public class PIDTuningManipulator
        : DeviceManipulater<TrainController, TrainControllerState>
    {

        protected override void ExecuteInternal()
        {
            //var q = Observable.FromEventPattern<PacketReceiveEventArgs>(this.TargetDevice, "PacketReceived")
            //                  .SubscribeOn(Scheduler.CurrentThread)
            //                  ;
        }

        private PidParams calcParams(IList<TrainControllerState> states)
        {
            if (states == null)
                throw new ArgumentNullException();

            var voltageTo = states.First().Voltage;
            int timeradjust = 0;
            var voltages = Enumerable.Range(0, states.Count - 1)
                                             .Select((i) =>
                                             {
                                                 var staA = states [i];
                                                 var staB = states [i + 1];
                                                 if (staA.DeviceTime - staB.DeviceTime > 0)
                                                 {
                                                     timeradjust = ushort.MaxValue;
                                                 }

                                                 var volA = Math.Abs(staA.MeisuredVoltage - staA.MeisuredVoltage2);
                                                 var volB = Math.Abs(staB.MeisuredVoltage - staB.MeisuredVoltage2);
                                                 return new
                                                 {
                                                     num = i,
                                                     timer = staB.DeviceTime + timeradjust,
                                                     vol = volB,
                                                     Dvol = volA - volB
                                                 };
                                             })
                                             .ToList();

            float l=0.0f, t=0.0f, k=(float)voltageTo;
            for (int i =1; i < voltages.Count; ++i)
            {
                if (l == 0.0f && voltages [i].Dvol > 5)
                    l = voltages [i].timer ;

                if (t == 0.0f && voltages [i].vol > k * 0.63f)
                    t = voltages [i - 1].timer - l;

                if (t != 0.0f && l != 0.0f)
                    break;
            }

            if (t == 0.0f || l == 0.0f)
                throw new ArgumentException("failed to determine pid parameters");

            return new PidParams()
            {
                paramp = 0.6f * t / (k * l),
                paramd = 0.6f / (k * l),
                parami = 0.3f * t / k,
            };

        }
    }
}
