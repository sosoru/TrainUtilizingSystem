﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Tus.Communication;

namespace Tus.Communication.Device.AvrComposed
{
    public class UsartSetting
        : Device<UsartSettingState>
    {
        public UsartSetting()
            : base(ModuleTypeEnum.AvrUartSetting, new UsartSettingState())
        {
        }

    }

    public class UsartSensor
        : Device<SensorState>, ISensorDevice
    {
        private volatile object hist_lock = new object();
        private LinkedList<SensorState> packet_history = new LinkedList<SensorState>();

        public UsartSensor()
            : base(ModuleTypeEnum.AvrSensor, new SensorState())
        {
        }

        public override void OnNext(IDeviceState<IPacketDeviceData> value)
        {
            base.OnNext(value);

            var state = value as SensorState;

            if (state == null)
                return;

            lock (hist_lock)
            {
                this.CurrentState = state;

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

                //if (hist.Any(s => s.Voltage > 0.45f || s.VoltageOn > 200))
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

        public UsartSetting CreateSettingDevice(int modulecount)
        {
            var setting = new UsartSetting();
            var id = this.DeviceID;

            id.InternalAddr &= 0xf0;
            setting.DeviceID = id;

            setting.CurrentState.ModuleCount = modulecount;
            setting.ReceivingServer = this.ReceivingServer;

            return setting;
        }
    }
}
