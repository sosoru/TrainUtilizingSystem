using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using Tus.Diagnostics;

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
            this.deviceKernel_ = new Kernel();
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

        public Kernel DeviceKernel
        {
            get
            {
                this.deviceKernel_.DeviceID = this.DeviceID;
                this.deviceKernel_.ReceivingServer = this.ReceivingServer;
                return deviceKernel_;
            }
        }

        public MotorMemoryStateEnum ReceivedMemory
        {
            get;
            set;
        }
        private object lock_memory = new object();
        [IgnoreDataMember]
        public MotorMemoryStateEnum CurrentMemory
        {
            get
            {
                lock (lock_memory ?? new object())
                {
                    if (!(DeviceKernel.CurrentState.Command == KernelCommand.MemoryState))
                        return MotorMemoryStateEnum.Unknown;

                    return (MotorMemoryStateEnum)DeviceKernel.CurrentState.Data.Content1;

                }
            }
            set
            {
                lock (lock_memory ?? new object())
                {
                    var memstate = new MemoryState((int)value);

                    MotorState currentState = CurrentState;
                    if (currentState != null) currentState.TargetMemory = value;

                    DeviceKernel.CurrentState = memstate;
                    //if (currentState != null) Console.WriteLine(currentState.ToString());

                }
            }
        }

        [DataMember(Name = "CurrentMemory")]
        public string CurrentMemoryString
        {
            get { return Enum.GetName(typeof(MotorMemoryStateEnum), CurrentMemory); }
            set { CurrentMemory = (MotorMemoryStateEnum)Enum.Parse(typeof(MotorMemoryStateEnum), value); }
        }

        public string ReceivedMemoryString
        {
            get { return Enum.GetName(typeof(MotorMemoryStateEnum), this.ReceivedMemory); }
            set { this.ReceivedMemory = (MotorMemoryStateEnum)Enum.Parse(typeof(MotorMemoryStateEnum), value); }

        }

        //public override MotorState CurrentState
        //{
        //    get { return base.CurrentState; }
        //    set
        //    {
        //        if (value != null && this.deviceKernel_!= null)
        //        {
        //            CurrentMemory = value.TargetMemory;
        //        }
        //        else
        //        {
        //            CurrentMemory = MotorMemoryStateEnum.Unknown;
        //        }

        //        base.CurrentState = value;
        //    }
        //}

        [DataMember(IsRequired = false)]
        public bool IsDetected
        {
            get { return CurrentState.Current > CurrentState.ThresholdCurrent; }
            set { throw new NotImplementedException(); }
        }

        public MotorMemoryStateEnum ModeAfterWaiting { get; set; }

        public IEnumerable<DevicePacket> ChangeMemoryTo(MotorMemoryStateEnum mem)
        {
            Kernel kernel = Kernel.MemoryState(DeviceID, new MemoryState((int)mem));
            IEnumerable<DevicePacket> devp = PacketExtension.CreatePackedPacket(kernel);

            return devp;
        }

        private MotorMemoryStateEnum _before_current = MotorMemoryStateEnum.Unknown;
        private DateTime _before_sent_waiting = DateTime.MinValue;
        private IEnumerable<DevicePacket> createApplyingStates()
        {
            var statelist = new List<IDevice<IDeviceState<IPacketDeviceData>>>();
            bool withoutwaiting = (this.CurrentMemory == MotorMemoryStateEnum.Waiting &&
                                    this._before_current == MotorMemoryStateEnum.Waiting);

            foreach (var state in States)
            {
                //if (withoutwaiting && state.Key == MotorMemoryStateEnum.Waiting)
                //    continue;

                state.Value.TargetMemory = state.Key;
                statelist.Add(new Motor(this, state.Value));
            }

            //waiting stateが連続出ない場合，
            // Stateを送る条件：
            //  1，CurrentMemoryがUnknown（初期化）
            var exprinit = this.CurrentMemory == MotorMemoryStateEnum.Unknown;
            //  2，CurrentMemoryとReceivedMemoryの不一致(Waiting以外で)
            var exprrefresh = this.CurrentMemory != MotorMemoryStateEnum.Waiting
                && this.CurrentMemory != this.ReceivedMemory;
            //  3，CurrentMemoryがWaitingStateで，ReceivedMemoryがMotorAfterWaiting||Waitingでない場合
            var exprwaiting = this.CurrentMemory == MotorMemoryStateEnum.Waiting
                && this.ReceivedMemory != this.ModeAfterWaiting
                && this.ReceivedMemory != MotorMemoryStateEnum.Waiting;
            if (exprinit|| exprrefresh|| exprwaiting)
            {
                // send packet changing memory
                statelist.Add(Kernel.MemoryState(DeviceID, new MemoryState((int)CurrentMemory)));
                Logger.WriteLineAsDeviceInfo("Motor {0} changed to {1} ({2})", this.DeviceID, this.CurrentMemoryString, this.ReceivedMemoryString);
                this._before_sent_waiting = DateTime.Now;
            }

            _before_current = CurrentMemory;
            return PacketExtension.CreatePackedPacket(statelist);
        }

        public override void Observe(IObservable<IDeviceState<IPacketDeviceData>> observable)
        {
            // TODO: in out で状態を分ける
            //base.Observe(observable);

            DeviceKernel.Observe(observable);
            DeviceKernel.PacketReceived += DeviceKernel_PacketReceived;
        }

        void DeviceKernel_PacketReceived(IDevice<KernelState> sender, PacketReceiveEventArgs args)
        {
            if (sender is Kernel)
            {
                var dev = (Kernel)sender;
                if (dev.IsMemoryState)
                {
                    this.ReceivedMemory = (MotorMemoryStateEnum)dev.CurrentState.Data.Content1;
                }

            }
        }

        // TODO: SendState使わないとMotorのデータがちゃんと送れない
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
            { Logger.WriteLineAsDeviceInfo("motor current memory is changed"); }
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