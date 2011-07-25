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
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using SensorLivetView.ViewModels.Controls;

namespace SensorLivetView.ViewModels
{
    public class TestTaskViewModel<Ttest>
        : ViewModel
        where Ttest : TestTaskModel
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

        public Ttest Model { get; set; }

        #region StartCommand
        DelegateCommand _StartCommand;

        public DelegateCommand StartCommand
        {
            get
            {
                if (_StartCommand == null)
                    _StartCommand = new DelegateCommand(Start, CanStart);
                return _StartCommand;
            }
        }

        private bool CanStart()
        {
            return this.Model != null && this.Model.IsTesting;
        }

        private void Start()
        {
            if (!this.Model.IsTesting)
                Observable.SubscribeOn(this.Model.Start(), Scheduler.NewThread);

        }
        #endregion
    }

    public class TrainSpeedTransitionTestViewModel
        : TestTaskViewModel<TrainSpeedTransitionTest>
    {


        IEnumerable<TrainSensorViewModel> _SensorCandicates;

        public IEnumerable<TrainSensorViewModel> SensorCandicates
        {
            get
            { return _SensorCandicates; }
            set
            {
                if (_SensorCandicates == value)
                    return;
                _SensorCandicates = value;
                RaisePropertyChanged("SensorCandicates");
            }
        }


        IEnumerable<TrainControllerViewModel> _ControllerCandicates;

        public IEnumerable<TrainControllerViewModel> ControllerCandicates
        {
            get
            { return _ControllerCandicates; }
            set
            {
                if (_ControllerCandicates == value)
                    return;
                _ControllerCandicates = value;
                RaisePropertyChanged("ControllerCandicates");
            }
        }
      
            
    }
}
