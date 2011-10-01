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
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Threading;
using System.Threading.Tasks;

using SensorLibrary;
using SensorLibrary.Manipulators;

using SensorLivetView.Models;

namespace SensorLivetView.ViewModels.Controls
{
    public class ManipulatorViewModel : ViewModel
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

        public DeviceManipulatorModel Model { get; set; }



        bool _IsExecuting;

        public bool IsExecuting
        {
            get
            { return _IsExecuting; }
            private set
            {
                if (_IsExecuting == value)
                    return;
                _IsExecuting = value;
                RaisePropertyChanged("IsExecuting");
            }
        }


        #region ExecuteCommand
        DelegateCommand _ExecuteCommand;

        public DelegateCommand ExecuteCommand
        {
            get
            {
                if (_ExecuteCommand == null)
                    _ExecuteCommand = new DelegateCommand(Execute, CanExecute);
                return _ExecuteCommand;
            }
        }

        private bool CanExecute()
        {
            return this.Model != null && this.Model.Manipulator != null && this.Model.Manipulator.IsExecutable;
        }

        private void Execute()
        {
            if (this.IsExecuting)
                return;

            this.IsExecuting = true;
            var task = new Task(this.Model.Manipulator.ExecuteFunc);
            var continuation =task.ContinueWith((t) => this.IsExecuting = false);

            task.Start(TaskScheduler.Default);
        }
        #endregion

    }
}
