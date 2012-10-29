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

        protected Motor(Motor mtr, MotorState state)
            :this()
        {
            this.CurrentState = state;
            this.DeviceID = mtr.DeviceID;
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
            var mem = new MemoryState(mem, false);

            var kernel = Kernel.MemoryState(this.DeviceID, mem);
            var devp = DevicePacket.CreatePackedPacket(kernel);

            return new[] { devp };
        }

        public IEnumerable<DevicePacket> CreateApplyingStates()
        {
            var statelist = new List<IDevice<IDeviceState<IPacketDeviceData>>>();

            statelist.Add(Kernel.MemoryState(this.DeviceID, new MemoryState(MotorMemoryStateEnum.NoEffect, true));
            statelist.Add(new Motor(this, this.StateWhenNoEffect));

            statelist.Add(Kernel.MemoryState(this.DeviceID, new MemoryState(MotorMemoryStateEnum.Controlling, true)));
            statelist.Add(new Motor(this, this.StateWhenControlling));

            statelist.Add(Kernel.MemoryState(this.DeviceID, new MemoryState(MotorMemoryStateEnum.Waiting, true)));
            statelist.Add(new Motor(this, this.StateWhenNoEffect));

            return DevicePacket.CreatePackedPacket(statelist);
            
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
