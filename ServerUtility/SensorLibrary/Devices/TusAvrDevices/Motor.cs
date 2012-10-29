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
                return this.CurrentState.Current > this.CurrentState.ThresholdCurrent;
            }
        }

        private Kernel deviceKernel_ = null;
        public Kernel DeviceKernel
        {
            get
            {
                if (this.deviceKernel_ == null)
                    this.deviceKernel_ = new Kernel() { DeviceID = this.DeviceID };

                return this.deviceKernel_;
            }
        }

        public IEnumerable<DevicePacket> ChangeMemoryTo(MotorMemoryStateEnum mem)
        {
            var kernel = Kernel.MemoryState(this.DeviceID, new MemoryState((int)mem, false));
            var devp = DevicePacket.CreatePackedPacket(kernel);

            return devp ;
        }

        public IEnumerable<DevicePacket> CreateApplyingStates()
        {
            var statelist = new List<IDevice<IDeviceState<IPacketDeviceData>>>();

            statelist.Add(Kernel.MemoryState(this.DeviceID, new MemoryState((int)MotorMemoryStateEnum.NoEffect, true)));
            statelist.Add(new Motor(this, this.StateWhenNoEffect));

            statelist.Add(Kernel.MemoryState(this.DeviceID, new MemoryState((int)MotorMemoryStateEnum.Controlling, true)));
            statelist.Add(new Motor(this, this.StateWhenControlling));

            statelist.Add(Kernel.MemoryState(this.DeviceID, new MemoryState((int)MotorMemoryStateEnum.Waiting, true)));
            statelist.Add(new Motor(this, this.StateWhenNoEffect));

            return DevicePacket.CreatePackedPacket(statelist);
            
        }

        public override void Observe(IObservable<IDeviceState<IPacketDeviceData>> observable)
        {
            base.Observe(observable);

            this.DeviceKernel.Observe(observable);
        }

        public override void SendState()
        {
            if (this.CurrentMemory == MotorMemoryStateEnum.Unknown)
                this.CurrentMemory = MotorMemoryStateEnum.NoEffect;

            var pack = this.ChangeMemoryTo(this.CurrentMemory);
            foreach (var p in pack)
                this.CurrentState.ReceivingServer.SendPacket(p);
        }

        public MotorMemoryStateEnum CurrentMemory
        {
            get {
                if (!(this.DeviceKernel.CurrentState is MemoryState))
                    return MotorMemoryStateEnum.Unknown;

                var memstate = (MemoryState)this.DeviceKernel.CurrentState;

                return (MotorMemoryStateEnum)memstate.CurrentMemory;
            }
            set
            {
                var memstate = new MemoryState((int)value, false);

                this.DeviceKernel.CurrentState = memstate;
            }
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
        Unknown = 0x00,
    }
}
