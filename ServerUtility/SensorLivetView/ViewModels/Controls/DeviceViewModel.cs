using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using SensorLibrary;
using Livet;
using Livet.Command;
using SensorLivetView.Models.Devices;

namespace SensorLivetView.ViewModels
{
    public abstract class DeviceViewModel<TModel>
        : ViewModel
    where TModel : DeviceModel<IDeviceState<IPacketDeviceData>>
    {
        private TModel _model;

        public IEqualityComparer<IDeviceState<IPacketDeviceData>> Comparer;

        public virtual TModel Model
        {
            get
            {
                return this._model;
            }
            set
            {
                //var del = new PacketReceivedDelegate<IDeviceState<IPacketDeviceData>>(ReceivedProcess);
                //if (value != null && value != Model)
                //    value.PacketReceived += del;
                //if (Model != null && value != Model)
                //    Model.PacketReceived -= del;

                _model = value;

            }
        }
        
        public DeviceViewModel(TModel device)
            : base()
        {
            this.Model = device;
            this.Comparer = new DeviceStateComparer();
        }

        protected virtual void ReceivedProcess(IDevice<IDeviceState<IPacketDeviceData>> dev, PacketReceiveEventArgs e)
        {
            if (!this.Comparer.Equals(e.beforestate, e.state))
                this.RaisePropertyChanged("");
        }

        #region SendPacketCommand
        DelegateCommand _SendPacketCommand;

        public DelegateCommand SendPacketCommand
        {
            get
            {
                if (_SendPacketCommand == null)
                {
                    var dev = this.Model as IDevice<IDeviceState<IPacketDeviceData>>;
                    _SendPacketCommand = new DelegateCommand(new Action(() => dev.SendPacket(dev.CurrentState)), CanSendPacket);
                }
                return _SendPacketCommand;
            }
        }

        private bool CanSendPacket()
        {
            return (this.Model != null && this.Model is IDevice<IDeviceState<IPacketDeviceData>> && this.Model.CurrentState != null);
        }

        #endregion

        public DeviceID DevID
        {
            get
            {
                if (this.Model != null)
                    return this.Model.TargetDevice.DeviceID;
                else
                    return new DeviceID();
            }
            set
            {
                if (this.Model != null)
                    this.Model.TargetDevice.DeviceID = value;
            }
        }
    }
}
