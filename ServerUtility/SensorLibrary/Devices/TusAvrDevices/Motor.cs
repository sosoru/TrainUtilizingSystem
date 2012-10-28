using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary.Packet.Control;

namespace SensorLibrary.Devices.TusAvrDevices
{
    public class Motor
        : Device<MotorState>, ISensorDevice
    {
        public Motor()
        {
            this.ModuleType = ModuleTypeEnum.AvrMotor;
            this.CurrentState = new MotorState();
        }

        public Motor(PacketServer server)
            :this()
        {
            this.CurrentState.ReceivingServer = server;
        }

        public bool IsDetected
        {
            get {
                return this.CurrentState.CurrentValue > this.CurrentState.ThresholdValue;

            }
        }
    }
}
