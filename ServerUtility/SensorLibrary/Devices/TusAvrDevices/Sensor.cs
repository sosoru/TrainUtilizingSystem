﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary;

namespace SensorLibrary.Devices.TusAvrDevices
{
    public class Sensor
        : Device<SensorState>
    {
        private volatile object hist_lock = new object();
        private LinkedList<SensorState> packet_history = new LinkedList<SensorState>();

        public Sensor()
        {
            this.ModuleType = ModuleTypeEnum.AvrSensor;
            this.CurrentState = new SensorState();
        }

        public override void OnNext(IDeviceState<IPacketDeviceData> value)
        {
            base.OnNext(value);

            var state = value as SensorState;

            if (state == null) 
                return;

            lock (hist_lock)
            {
                packet_history.AddLast(state);
                if (packet_history.Count > 2)
                    packet_history.RemoveFirst();
            }

        }

        public bool IsDetected
        {
            get
            {
                return this.CurrentState.Voltage > 0.55f;

                //IList<SensorState> hist;
                //lock (hist_lock)
                //    hist = this.packet_history.ToArray();

                //if (hist.Any(s => s.Voltage > 0.45f || s.OnVoltage > 200))
                //{
                //    return true;
                //}

                //if (hist.All(s => s.Voltage > 0.30f))
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}

            }
        }
    }
}