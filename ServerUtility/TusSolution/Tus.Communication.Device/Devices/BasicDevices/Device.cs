using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Reactive;

namespace Tus.Communication
{
    public delegate void PacketReceivedDelegate<in TState>(IDevice<TState> sender, PacketReceiveEventArgs args);

    public class PacketReceiveEventArgs
        : EventArgs
    {
        public IDeviceState<IPacketDeviceData> state;
        public IDeviceState<IPacketDeviceData> beforestate;
    }

    public interface IDevice<out TState>
        : IObserver<IDeviceState<IPacketDeviceData>>
    {
        TState CurrentState { get; }
        DeviceID DeviceID { get; set; }
        ModuleTypeEnum ModuleType { get; }
        bool IsHold { get; set; }

        PacketServer ReceivingServer { get; set; }
        void Observe(IObservable<IDeviceState<IPacketDeviceData>> observable);
        void SendState();
        IObservable<EventPattern<PacketReceiveEventArgs>> GetNextObservable { get; }

        event PacketReceivedDelegate<TState> PacketReceived;
    }

    [DataContract]
    public class Device<TState>
        : IDevice<TState>, INotifyPropertyChanged
    where TState : class, IDeviceState<IPacketDeviceData>
    {
        [IgnoreDataMember]
        protected IDisposable Unsubscriber = null;
        [IgnoreDataMember]
        protected IObservable<IDeviceState<IPacketDeviceData>> Observing = null;

        [IgnoreDataMember]
        public PacketServer ReceivingServer { get; set; }
        [DataMember]
        public virtual TState CurrentState { get; set; }
        [IgnoreDataMember]
        public ModuleTypeEnum ModuleType { get; protected set; }
        [IgnoreDataMember]
        public IEqualityComparer<TState> StateEqualityComparer { get; set; }

        [DataMember(Name = "ModuleType")]
        public string ModuleTypeString
        {
            get { return Enum.GetName(typeof(ModuleTypeEnum), this.ModuleType); }
            set
            {
                this.ModuleType = (ModuleTypeEnum)Enum.Parse(typeof(ModuleTypeEnum), value);
            }
        }

        [IgnoreDataMember]
        private DeviceID devid_;
        [IgnoreDataMember]
        public DeviceID DeviceID
        {
            get
            {
                return this.devid_;
            }
            set
            {
                this.devid_ = value;
                this.CurrentState.ID = value;
            }
        }

        [DataMember(Name = "DeviceID")]
        public string DeviceIDString
        {
            get { return this.DeviceID.ToString(); }
            set { this.DeviceID = DeviceIdParser.FromString(value).First(); }
        }

        public Device(ModuleTypeEnum type, TState state)
        {
            this.ModuleType = type;
            this.CurrentState = state;
            //this.StateEqualityComparer = new GenericComparer<TState>((x, y) => x.Data.SequenceEqual(y.Data));
        }

        [IgnoreDataMember]
        private object LockHold = new object();
        [IgnoreDataMember]
        private bool _ishold;
        [IgnoreDataMember]
        public bool IsHold
        {
            get
            {
                lock (LockHold)
                    return this._ishold;
            }
            set
            {
                lock (LockHold)
                    this._ishold = value;
            }
        }

        public event PacketReceivedDelegate<TState> PacketReceived;
        protected void OnPacketReceived(PacketReceiveEventArgs args)
        {
            if (PacketReceived != null)
            {
                PacketReceived(this, args);
            }
        }

        public virtual void Observe(IObservable<IDeviceState<IPacketDeviceData>> observable)
        {
            if (this.Unsubscriber != null)
                this.Unsubscriber.Dispose();

            this.Observing = observable;
            if (observable != null)
                this.Unsubscriber = observable.Subscribe(this);
        }

        public virtual void SendState()
        {
            if (this.CurrentState == null || this.ReceivingServer == null)
                throw new InvalidOperationException("missing Device");

            //state.BasePacket.ID = this.DeviceID;
            //if (!(state is TState))
            //    throw new InvalidOperationException("invalid state");

            //for (int i = 0; i < 3; i++)
            this.ReceivingServer.EnqueueState(this);

        }

        protected DevicePacket CreatePacket<T>(T data)
            where T : IPacketDeviceData
        {
            var pack = new DevicePacket()
            {
                ID = this.DeviceID,
            };
            pack.CopyToData<T>(data);
            return pack;
        }

        public virtual void OnCompleted()
        {

        }

        public virtual void OnError(Exception error)
        {
            CurrentState = default(TState);
        }

        public virtual void OnNext(IDeviceState<IPacketDeviceData> value)
        {
            var state = value as TState;

            if (state != null)
                this.OnNext(state);
        }

        public IObservable<EventPattern<PacketReceiveEventArgs>> GetNextObservable
        {
            get
            {
                return Observable.FromEventPattern<PacketReceivedDelegate<TState>, PacketReceiveEventArgs>((del) => PacketReceived += del, (del) => PacketReceived -= del);
            }
        }

        public override string ToString()
        {
            if (CurrentState == null)
                return "(null)";
            else
                return this.CurrentState.ToString();
        }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        ~Device()
        {
            if (this.Unsubscriber != null)
                this.Unsubscriber.Dispose();
        }

        private DateTime _before_received_Date = DateTime.MinValue;
        public void OnNext(TState value)
        {
            if (value == null || this.DeviceID != value.ID)
                return;

            var casted = value;
            var before = this.CurrentState;

            if (!this.IsHold && (DateTime.Now - this._before_received_Date).TotalSeconds > 1.0)
            {
                this._before_received_Date = DateTime.Now;
                this.CurrentState = casted;

                OnPacketReceived(new PacketReceiveEventArgs() { state = casted, beforestate = before });

                OnPropertyChanged("");

            }

        }

    }
}
