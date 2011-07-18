using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }

}
