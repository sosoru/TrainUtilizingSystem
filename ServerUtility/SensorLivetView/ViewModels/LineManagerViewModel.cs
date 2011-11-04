using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

using Livet;
using Livet.Command;
using Livet.Messaging;
using Livet.Messaging.File;
using Livet.Messaging.Window;

using SensorLivetView.Models;
using SensorLivetView.Models.Devices;
using SensorLivetView.ViewModels.Controls;

using SensorLibrary;

using System.Reactive.Linq;
using System.Reactive.Concurrency;


namespace SensorLivetView.ViewModels
{
    public class LineManagerViewModel : ViewModel
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


        TrainControllerDeviceViewModel _ControllerViewModel;

        public TrainControllerDeviceViewModel ControllerViewModel
        {
            get
            { return _ControllerViewModel; }
            set
            {
                if (_ControllerViewModel == value)
                    return;
                _ControllerViewModel = value;
                RaisePropertyChanged("ControllerViewModel");
            }
        }

        public ObservableCollection<StationViewModel> Stations { get; private set; }
        public ObservableCollection<PointStrategyViewModel> PointStrategies { get; private set; }

        public LineManagerViewModel(TrainControllerDeviceViewModel controller)
        {
            this.ControllerViewModel = controller;

            this.Stations = new ObservableCollection<StationViewModel>();
            this.PointStrategies = new ObservableCollection<PointStrategyViewModel>();
        }
      
    }
}
