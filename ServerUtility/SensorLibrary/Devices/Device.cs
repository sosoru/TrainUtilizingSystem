using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.ComponentModel;
using System.Reactive.Linq;

namespace SensorLibrary
{
    public delegate void PacketReceivedDelegate<in TState>(IDevice<TState> sender, PacketReceiveEventArgs args);

    public class PacketReceiveEventArgs
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
        void SendPacket(DevicePacket packet);

        event PacketReceivedDelegate<TState> PacketReceived;
    }

    public class Device<TState>
        : IDevice<TState>, INotifyPropertyChanged
    where TState : class, IDeviceState<IPacketDeviceData>
    {
        private IDisposable _unsubscriber = null;

        public TState CurrentState { get; protected set; }
        public DeviceID DeviceID { get; private set; }
        public ModuleTypeEnum ModuleType { get; private set; }

        public Device(DeviceID id, ModuleTypeEnum moduletype, IObservable<IDeviceState<IPacketDeviceData>> observable)
        {
            this.DeviceID = id;
            this.ModuleType = moduletype;

            if (observable != null)
                this.Observe(observable);
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
            this.SendPacket(this.CurrentState.BasePacket);
        }

        public void SendPacket(DevicePacket pack)
        {
            if (this.CurrentState == null || this.CurrentState.ReceivingServer == null)
                throw new InvalidOperationException("missing Device");

            for(int i=0; i < 2; i++)
                this.CurrentState.ReceivingServer.SendPacket(pack);
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

                OnPacketReceived(new PacketReceiveEventArgs() { state = casted, beforestate = before });

                OnPropertyChanged("");
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
            if (this._unsubscriber != null)
                this._unsubscriber.Dispose();
        }
    }
}
