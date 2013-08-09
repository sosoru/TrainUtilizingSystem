using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Tus.Communication.Device.AvrComposed
{
    [DataContract]
    public class Motor
        : Device<MotorState>, ISensorDevice
    {
        [IgnoreDataMember]
        private Kernel deviceKernel_;

        public Motor()
            : base(ModuleTypeEnum.AvrMotor, new MotorState())
        {
            States = new Dictionary<MotorMemoryStateEnum, MotorState>();
        }

        public Motor(PacketServer server)
            : this()
        {
            ReceivingServer = server;
        }

        protected Motor(Motor mtr, MotorState state)
            : this()
        {
            CurrentState = state;
            DeviceID = mtr.DeviceID;
        }

        [DataMember]
        public IDictionary<MotorMemoryStateEnum, MotorState> States { get; set; }

        [DataMember]
        public Kernel DeviceKernel
        {
            get
            {
                if (deviceKernel_ == null)
                    deviceKernel_ = new Kernel();

                this.deviceKernel_.DeviceID = this.DeviceID;
                this.deviceKernel_.ReceivingServer = this.ReceivingServer;
                return deviceKernel_;
            }
            set
            {
                Kernel newkernel = value;
                newkernel.DeviceID = DeviceID;
                newkernel.ReceivingServer = ReceivingServer;
                deviceKernel_ = newkernel;
            }
        }

        [IgnoreDataMember]
        public MotorMemoryStateEnum CurrentMemory
        {
            get
            {
                if (!(DeviceKernel.CurrentState.Command == KernelCommand.MemoryState))
                    return MotorMemoryStateEnum.Unknown;

                return (MotorMemoryStateEnum)DeviceKernel.CurrentState.Data.Content1;
            }
            set
            {
                var memstate = new MemoryState((int)value);

                MotorState currentState = CurrentState;
                if (currentState != null) currentState.TargetMemory = value;

                DeviceKernel.CurrentState = memstate;
            }
        }

        [DataMember(Name = "CurrentMemory")]
        public string CurrentMemoryString
        {
            get { return Enum.GetName(typeof(MotorMemoryStateEnum), CurrentMemory); }
            set { CurrentMemory = (MotorMemoryStateEnum)Enum.Parse(typeof(MotorMemoryStateEnum), value); }
        }

        public override MotorState CurrentState
        {
            get { return base.CurrentState; }
            set
            {
                if (value != null)
                {
                    CurrentMemory = value.TargetMemory;
                }
                else
                {
                    CurrentMemory = MotorMemoryStateEnum.Unknown;
                }

                base.CurrentState = value;
            }
        }

        [DataMember(IsRequired = false)]
        public bool IsDetected
        {
            get { return CurrentState.Current > CurrentState.ThresholdCurrent; }
            set { throw new NotImplementedException(); }
        }

        public IEnumerable<DevicePacket> ChangeMemoryTo(MotorMemoryStateEnum mem)
        {
            Kernel kernel = Kernel.MemoryState(DeviceID, new MemoryState((int)mem));
            IEnumerable<DevicePacket> devp = PacketExtension.CreatePackedPacket(kernel);

            return devp;
        }

        private MotorMemoryStateEnum _before_sending = MotorMemoryStateEnum.Unknown;
        private Stopwatch _before_sending_time = new Stopwatch();
        private IEnumerable<DevicePacket> createApplyingStates()
        {
            var statelist = new List<IDevice<IDeviceState<IPacketDeviceData>>>();

            foreach (var state in States)
            {
                state.Value.TargetMemory = state.Key;
                statelist.Add(new Motor(this, state.Value));
            }

            //waiting stateが連続出ない場合，
            //ストップウォッチが動いていない場合，
            if ((CurrentMemory != MotorMemoryStateEnum.Waiting ||
                _before_sending != MotorMemoryStateEnum.Waiting)
                ||(!_before_sending_time.IsRunning))
            {
                // send packet changing memory
                statelist.Add(Kernel.MemoryState(DeviceID, new MemoryState((int)CurrentMemory)));
                _before_sending_time.Restart();
            }
            _before_sending = CurrentMemory;

            return PacketExtension.CreatePackedPacket(statelist);
        }

        public override void Observe(IObservable<IDeviceState<IPacketDeviceData>> observable)
        {
            // TODO: in out で状態を分ける
            //base.Observe(observable);

            DeviceKernel.Observe(observable);
        }

        public override void SendState()
        {
            // TODO: DeviceKernelとのスレッドセーフ
            var currentmem = this.CurrentMemory;
            if (currentmem == MotorMemoryStateEnum.Unknown)
                currentmem = MotorMemoryStateEnum.NoEffect;

            IEnumerable<DevicePacket> app = createApplyingStates();
            foreach (DevicePacket p in app)
                ReceivingServer.EnqueuePacket(p);
            try
            {
                this.CurrentState = this.States[currentmem];
            }
            catch (KeyNotFoundException ex)
            { Console.WriteLine("motor current memory is changed"); }
            //var pack = this.ChangeMemoryTo(this.CurrentMemory);
            //foreach (var p in pack)
            //    this.ReceivingServer.SendPacket(p);
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