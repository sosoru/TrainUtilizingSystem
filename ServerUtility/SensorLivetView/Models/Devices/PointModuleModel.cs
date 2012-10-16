using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using SensorLibrary;
using SensorLibrary.Devices.PicUsbDevices;
using System.Collections.ObjectModel;

namespace SensorLivetView.Models.Devices
{
    public class PointModuleModel : DeviceModel<PointModule>
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

        public PointModuleModel()
        {

        }

        private ReadOnlyObservableCollection<PointModel> _states;
        public ReadOnlyObservableCollection<PointModel> States
        {
            get
            {
                if (_states == null)
                {
                    var states = new ObservableCollection<PointModel>();
                    if (this.TargetDevice.CurrentState == null)
                        return new ReadOnlyObservableCollection<PointModel>(new ObservableCollection<PointModel>());

                    Enumerable.Range(0, this.TargetDevice.CurrentState.StateLength)
                              .ForEach(i => states.Add(new PointModel(this, i)));
                    _states = new ReadOnlyObservableCollection<PointModel>( states);
                }

                return _states;
            }
        }
    }
}
