using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensorLibrary
{
    public class MeisuringTrainSensor
        : TrainSensor
    {
        private object LockObject = new object();
        private Queue<TrainSensorState> history = new Queue<TrainSensorState>(1024);

        public MeisuringTrainSensor()
            : base()
        { }

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


        public override void OnNext(IDeviceState<IPacketDeviceData> value)
        {
            var casted = value as TrainSensorState;
            if (casted == null)
                return;

            //lock (LockObject)
            //{
            if (value.BasePacket.ID.ModuleAddr == this.DeviceID.ModuleAddr
                && casted.Mode == TrainSensorMode.meisuring)
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

    }
}
