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
            : this()
        {
            this.CurrentState.ReceivingServer = server;
        }

        public bool IsDetected
        {
            get
            {
                return this.CurrentState.Current > this.CurrentState.ThresholdValue;
            }
        }

        public IEnumerable<DevicePacket> ChangeMemoryTo(MotorMemoryStateEnum mem)
        {
            var mem = new MemoryState()
            {
                CurrentMemory = (byte)mem,
            };

            var kernel = Kernel.MemoryState(this.DeviceID, mem);
            var devp = DevicePacket.CreatePackedPacket(kernel);

            return new[] { devp };
        }

        public void SendStates()
        {
            var statelist = new List<IDevice<IDeviceState<IPacketDeviceData>>>();

            
        }

        public MotorState StateWhenNoEffect
        {
            get;
            set;
        }

        public MotorState StateWhenControlling
        {
            get;
            set;
        }

        public MotorState StateWhenWaiting
        {
            get;
            set;
        }

    }

    public enum MotorMemoryStateEnum
        : byte
    {
        NoEffect = 0x01,
        Controlling = 0x02,
        Waiting = 0x03,
    }
}
