using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LibUsbDotNet;
using LibUsbDotNet.Main;


using Livet;

namespace SensorLivetView.Models
{
    internal class UsbRegistryModel
        : NotifyObject
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

        public UsbRegistryModel(UsbDevicesModel devmodel)
        {
            this.DevicesModel = devmodel;
        }

        public UsbDevicesModel DevicesModel { get; private set; }
        public Notificator<EventArgs> SelectedNotificator { get; set; }

        UsbRegistry _Registry;

        public UsbRegistry Registry
        {
            get
            { return _Registry; }
            set
            {
                if (_Registry == value)
                    return;
                _Registry = value;
                RaisePropertyChanged("Registry");
            }
        }


        bool _IsRegistered;

        public bool IsRegistered
        {
            get
            { return _IsRegistered; }
            set
            {
                if (_IsRegistered == value)
                    return;
                _IsRegistered = value;
                RaisePropertyChanged("IsRegistered");
            }
        }
      
        

    }
}
