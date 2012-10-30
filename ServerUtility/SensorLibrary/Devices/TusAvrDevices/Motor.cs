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
            this.States = new Dictionary<MotorMemoryStateEnum, MotorState>();
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

        public IDictionary<MotorMemoryStateEnum, MotorState> States
        {
            get;
             set;
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
            var kernel = Kernel.MemoryState(this.DeviceID, new MemoryState((int)mem));
            var devp = DevicePacket.CreatePackedPacket(kernel);

            return devp ;
        }

        public IEnumerable<DevicePacket> CreateApplyingStates()
        {
            var statelist = new List<IDevice<IDeviceState<IPacketDeviceData>>>();

            foreach (var state in States)
            {
                statelist.Add(Kernel.MemoryState(this.DeviceID, new MemoryState((int)state.Key)));
                statelist.Add(new Motor(this, state.Value));
            }

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

            var app = this.CreateApplyingStates();
            foreach (var p in app)
                this.CurrentState.ReceivingServer.SendPacket(p);

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
                var memstate = new MemoryState((int)value);

                this.DeviceKernel.CurrentState = memstate;
            }
        }

    }

    public enum MotorMemoryStateEnum
        : byte
    {
        NoEffect = 0x01,
        Controlling = 0x02,
        Waiting = 0x03,
        Locked = 0x04,
        Unknown = 0x00,
    }
}
