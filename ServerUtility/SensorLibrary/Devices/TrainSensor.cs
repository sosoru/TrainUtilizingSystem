using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive;
using System.Reactive.Linq;

namespace SensorLibrary
{
    public class TrainSensor
    : Device<TrainSensorState>
    {

        private object LockObject = new object();
        private Queue<TrainSensorState> history = new Queue<TrainSensorState>(1024);

        //public event EventHandler TimerOverflowed;

        public TrainSensor(DeviceID id)
            : this(id, null)
        {
            //Observable.Range(1, 1024).Do((i) => this.history.Push(new TrainSensorState(new DevicePacket())));
        }

        public TrainSensor(DeviceID id, IObservable<IDeviceState<IPacketDeviceData>> obsv)
            : base(id, ModuleTypeEnum.TrainSensor, obsv) { }

        //protected void OnTimerOverflowed()
        //{            
        //    if (this.TimerOverflowed != null)
        //        this.TimerOverflowed(this, new EventArgs());
        //}

        public override void OnNext(IDeviceState<IPacketDeviceData> value)
        {
            var casted = value as TrainSensorState;
            if (casted == null)
                return;

            //lock (LockObject)
            //{
            if (value.BasePacket.ID == this.DeviceID)
            {
                lock (this.LockObject)
                {
                    history.Enqueue(casted);
                    if (history.Count > 1024)
                        history.Dequeue();
                }

                base.OnNext(casted);
            }
            //}
        }

        public void ChangeMeisuringMode()
        {
            var state = this.CurrentState;

            state.Mode = TrainSensorMode.meisuring;
            this.SendPacket(state);
        }

        public void ChangeDetectingMode(float threshold)
        {
            var state = this.CurrentState;

            state.Mode = TrainSensorMode.detecting;
            state.ThresholdVoltage = threshold;
            this.SendPacket(state);
        }

        public GraphPainter<TrainSensorState> GetPainter()
        {
            IList<TrainSensorState> states;
            lock (LockObject)
            {
                states = history.ToList();
            }

            var painter = new GraphPainter<TrainSensorState>()
            {
                plotXDetermine = (state, rect) => (state == null) ? 0
                                                                  : (float)state.Timer * rect.Width / 65536.0f,
                plotYDetermine = (state, rect) => (state == null) ? 0
                                                                 : (state.CurrentVoltage / 5.0F) * rect.Height,
                States = states
            };

            return painter;
        }

        public IObservable<TrainSensor> GetSpeedChangedObservable()
        {
            return this.GetNextObservable().Where((args) =>
                {
                    var bef = args.EventArgs.beforestate as TrainSensorState;
                    var cur = args.EventArgs.state as TrainSensorState;
                    if (bef != null && cur != null
                        && bef.Mode == TrainSensorMode.detecting && cur.Mode == TrainSensorMode.detecting
                        )
                        return true;
                    else
                        return false;
                })
                .Select((args) => args.Sender as TrainSensor);
                       
        }

        public double CalculateSpeed(double leninterval)
        {
            if (this.CurrentState.Mode != TrainSensorMode.detecting)
                throw new InvalidOperationException("cannot calculate speed unless its mode is detecting");

            if (this.history.Count > 1)
            {
                TrainSensorState before, current;
                before = this.history.ElementAt(this.history.Count - 2);
                current = this.CurrentState;

                if (before != null && current != null && before.Mode == TrainSensorMode.detecting && current.Mode == TrainSensorMode.detecting)
                {
                    double sec = 0.0f;
                    if (current.Timer - before.Timer < 0)
                        sec = Math.Abs(current.Timer - before.Timer) + ushort.MaxValue;
                    else
                        sec = current.Timer - before.Timer;

                    sec /= 48000000.0;

                    return leninterval / sec;
                }
            }

            return double.NaN;
        }
    }

}
