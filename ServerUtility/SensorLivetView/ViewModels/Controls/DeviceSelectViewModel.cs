using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Command;
using Livet.Messaging;
using Livet.Messaging.File;
using Livet.Messaging.Window;

using SensorLivetView.Models;
using SensorLibrary;

namespace SensorLivetView.ViewModels.Controls
{
    public class DeviceSelectViewModel : ViewModel
    {
        /*コマンド、プロパティの定義にはそれぞれ 
         * 
         *  ldcom   : DelegateCommand(パラメータ無)
         *  ldcomn  : DelegateCommand(パラメータ無・CanExecute無)
         *  ldcomp  : DelegateCommand(型パラメータ有)
         *  ldcompn : DelegateCommand(型パラメータ有・CanExecute無)
         *  lprop   : 変更通知プロパティ
         *  
         * を使用してください。
         */

        /*ViewModelからViewを操作したい場合は、
         * Messengerプロパティからメッセージ(各種InteractionMessage)を発信してください。
         */

        /*
         * UIDispatcherを操作する場合は、DispatcherHelperのメソッドを操作してください。
         * UIDispatcher自体はApp.xaml.csでインスタンスを確保してあります。
         */

        /*
         * Modelからの変更通知などの各種イベントをそのままViewModelで購読する事はメモリリークの
         * 原因となりやすく推奨できません。ViewModelHelperの各静的メソッドの利用を検討してください。
         */

        public DeviceSelectViewModel()
            : base()
        {
            this.Selector = (l) => l;
        }

        public IList<IDevice<IDeviceState<IPacketDeviceData>>> SelectedDevices
        {
            get
            {
                return Selector(this.DeviceCandicates);
            }
        }

        Func<IList<IDevice<IDeviceState<IPacketDeviceData>>>,IList<IDevice<IDeviceState<IPacketDeviceData>>>> _Selector;

        public Func<IList<IDevice<IDeviceState<IPacketDeviceData>>>,IList<IDevice<IDeviceState<IPacketDeviceData>>>> Selector
        {
            get
            { return _Selector; }
            set
            {
                if (_Selector == value)
                    return;
                _Selector = value;
                RaisePropertyChanged("Selector");
                RaisePropertyChanged("SelectedDevices");
            }
        }
      

        IList<IDevice<IDeviceState<IPacketDeviceData>>> _DeviceCandicates;

        public IList<IDevice<IDeviceState<IPacketDeviceData>>> DeviceCandicates
        {
            get
            { return _DeviceCandicates; }
            set
            {
                if (_DeviceCandicates == value)
                    return;
                _DeviceCandicates = value;
                RaisePropertyChanged("DeviceCandicates");
                RaisePropertyChanged("SelectedDevices");
            }
        }
      
    }
}
