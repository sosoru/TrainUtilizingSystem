using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Reactive;

namespace SensorLibrary
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
        DeviceID DeviceID { get; }
        ModuleTypeEnum ModuleType { get; }
        bool IsHold { get; }

        void Observe(IObservable<IDeviceState<IPacketDeviceData>> observable);
        void SendPacket(IDeviceState<IPacketDeviceData> packet);

        event PacketReceivedDelegate<TState> PacketReceived;
    }

    public class Device<TState>
        : IDevice<TState>, INotifyPropertyChanged
    where TState : class, IDeviceState<IPacketDeviceData>
    {
        private IDisposable _unsubscriber = null;
        private TState _sentState = null;

        public TState CurrentState { get; protected set; }
        public DeviceID DeviceID { get; private set; }
        public ModuleTypeEnum ModuleType { get; private set; }
        public IEqualityComparer<TState> StateEqualityComparer { get; set; }

        public Device(DeviceID id, ModuleTypeEnum moduletype, IObservable<IDeviceState<IPacketDeviceData>> observable)
        {
            this.DeviceID = id;
            this.ModuleType = moduletype;

            if (observable != null)
                this.Observe(observable);

            this.StateEqualityComparer = new GenericComparer<TState>((x, y) => x.BasePacket.Data.SequenceEqual(y.BasePacket.Data));
        }

        public Device(DeviceID id, ModuleTypeEnum moduletype)
            : this(id, moduletype, null)
        {
        }

        private object LockHold = new object();
        private bool _ishold;
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

        public void Observe(IObservable<IDeviceState<IPacketDeviceData>> observable)
        {
            if (this._unsubscriber != null)
                this._unsubscriber.Dispose();

            this._unsubscriber = observable.Subscribe(this);
        }

        public void SendPacket()
        {
            this.SendPacket(this.CurrentState);
        }

        public void SendPacket(IDeviceState<IPacketDeviceData> state)
        {
            if (state == null || state.ReceivingServer == null)
                throw new InvalidOperationException("missing Device");

            if (!(state is TState))
                throw new InvalidOperationException("invalid state");

            for(int i=0; i < 2; i++)
                this.CurrentState.ReceivingServer.SendPacket(state.BasePacket);

            this._sentState = state as TState;
        }

        protected DevicePacket CreatePacket<T>(T data)
            where T : IPacketDeviceData
        {
            var pack = new DevicePacket()
            {
                ID = this.DeviceID,
                ModuleType = this.ModuleType,
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
            var casted = value as TState;
            if (casted != null)
            {
                var before = this.CurrentState;

                if(!this.IsHold)
                    this.CurrentState = casted;

                if (this._sentState != null && this.StateEqualityComparer != null)
                {
                    if (!this.StateEqualityComparer.Equals(casted,this._sentState))
                    {
                        this.SendPacket(_sentState);
                    }
                    else
                    {
                        this._sentState = null;
                    }
                }

                OnPacketReceived(new PacketReceiveEventArgs() { state = casted, beforestate = before });

                OnPropertyChanged("");
            }
        }

        public IObservable<EventPattern<PacketReceiveEventArgs>> GetNextObservable()
        {
            return Observable.FromEventPattern<PacketReceivedDelegate<TState>, PacketReceiveEventArgs>((del)=> PacketReceived+= del, (del)=>PacketReceived -= del);
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
            if (this._unsubscriber != null)
                this._unsubscriber.Dispose();
        }
    }
}
