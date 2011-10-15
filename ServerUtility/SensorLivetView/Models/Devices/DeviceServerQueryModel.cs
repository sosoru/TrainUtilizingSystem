using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Threading;

using System.Collections.ObjectModel;
using System.Collections.Specialized;

using Livet;
using SensorLibrary;

namespace SensorLivetView.Models.Devices
{
    internal class DeviceServerQueryModel : NotifyObject
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

        public DeviceServerModel ParentServer { get; private set; }

        public Func<DeviceModel<IDevice<IDeviceState<IPacketDeviceData>>>, bool> Predicate { get;  set; }

        private  ObservableCollection<IDeviceModel<IDevice<IDeviceState<IPacketDeviceData>>>> activeDevices
            = new ObservableCollection<IDeviceModel<IDevice<IDeviceState<IPacketDeviceData>>>>();
        public ReadOnlyObservableCollection<IDeviceModel<IDevice<IDeviceState<IPacketDeviceData>>>> ActiveDevices
        {
            get { return new ReadOnlyObservableCollection<IDeviceModel<IDevice<IDeviceState<IPacketDeviceData>>>>(this.activeDevices); }
        }

        public DeviceServerQueryModel(DeviceServerModel serv)
        {
            this.ParentServer = serv;          
            
        }
    }
}
