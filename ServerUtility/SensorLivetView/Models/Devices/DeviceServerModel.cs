using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.ObjectModel;

using Livet;
using SensorLibrary;

namespace SensorLivetView.Models.Devices
{
    internal class DeviceServerModel : NotifyObject, IObserver<IDeviceState<IPacketDeviceData>>
    {
        /*
         * NotifyObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */

        /*
         * リッチクライアントはステートフルであるため、通常のイベントの使用はメモリリークの原因になりやすくなっています。
         * Modelからイベントを発行する場合はNotificatorを使用してください。
         *
         * Notificatorはイベント代替手段です。コードスニペット lnev でCLRイベントと同時に定義できます。
         *
         * Model同士でNotificatorを使用した通知を行う場合はNotificatorHelper、
         * ViewModelへNotificatorを使用した通知を行う場合はViewModelHelperを使用して受信側の登録をしてください。
         */
        public PacketDispatcher Dispatcher { get; private set; }

        private IDisposable unsubscrber;
        private ObservableCollection<IDeviceModel<IDevice<IDeviceState<IPacketDeviceData>>>> activeDevices;

        public DeviceServerModel(PacketDispatcher dispat)
        {
            this.activeDevices = new ObservableCollection<IDeviceModel<IDevice<IDeviceState<IPacketDeviceData>>>>();
            this.Dispatcher = dispat;

            this.unsubscrber = this.Dispatcher.Subscribe(this);
            
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(IDeviceState<IPacketDeviceData> value)
        {
            var devmodel = this.activeDevices.FirstOrDefault((model) => model.DevID == value.BasePacket.ID);

            if (devmodel.TargetDevice.ModuleType != value.BasePacket.ModuleType)
            {
                this.activeDevices.Remove(devmodel);
                devmodel = null;
            }

            if (devmodel == null)
            {
                var fact = DeviceFactory.AvailableDeviceTypes.FirstOrDefault(f => f.ModuleType == value.BasePacket.ModuleType);
                if (fact == null)
                    throw new InvalidOperationException(" unknown module type packet ");

                var dev = fact.DeviceCreate();
                dev.DeviceID = value.BasePacket.ID;
                dev.Observe(this.Dispatcher);

                var modelfact = DeviceViewModelFactory.Factries.FirstOrDefault(f => f.ModuleType == dev.ModuleType);
                devmodel = modelfact.ModelCreate(dev);

                this.activeDevices.Add(devmodel);

            }
        }

        ~DeviceServerModel()
        {
            if (this.unsubscrber != null)
                this.unsubscrber.Dispose(); 
        }
    }
}
