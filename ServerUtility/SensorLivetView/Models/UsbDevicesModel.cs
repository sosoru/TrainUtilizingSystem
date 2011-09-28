using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.ObjectModel;

using LibUsbDotNet;
using LibUsbDotNet.Main;

using Livet;

namespace SensorLivetView.Models
{
    public class UsbDevicesModel
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

        public UsbDevicesModel()
        {
            this.Candicates = new ObservableCollection<UsbRegistryModel>();
        }

        public ObservableCollection<UsbRegistryModel> Candicates { get; private set; }
        public Notificator<UsbDeviceRegisteredEventArgs> ModelRegisteredStateChanged { get; set; }

        int _VenderId;

        public int VenderId
        {
            get
            { return _VenderId; }
            set
            {
                if (_VenderId == value)
                    return;
                _VenderId = value;
                RaisePropertyChanged("VenderId");
            }
        }

        int _ProductId;

        public int ProductId
        {
            get
            { return _ProductId; }
            set
            {
                if (_ProductId == value)
                    return;
                _ProductId = value;
                RaisePropertyChanged("ProductId");
            }
        }

        public void Refresh()
        {
            foreach (var reg in UsbDevice.AllLibUsbDevices
                                        .FindAll(reg => reg.Pid == this.ProductId && reg.Vid == this.VenderId)
                                        .ToArray())
            {
                if (!this.Candicates.Any(_ => _.Registry.SymbolicName == reg.SymbolicName))
                {
                    var m = new UsbRegistryModel(this)
                    {
                        Registry = reg,
                        IsRegistered = false,
                    };

                    m.PropertyChanged += (sender, e) =>
                        {
                            if (this.ModelRegisteredStateChanged == null)
                                return;

                            if (e.PropertyName == "IsRegistered")
                                this.ModelRegisteredStateChanged.Raise(new UsbDeviceRegisteredEventArgs { RegistryModel = m });
                        };
                    this.Candicates.Add(m);
                }
            }
        }

    }

    public class UsbDeviceRegisteredEventArgs
        : EventArgs
    {
        public UsbRegistryModel RegistryModel { get; set; }
    }

}
