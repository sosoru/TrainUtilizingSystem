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
using SensorLivetView.Models.Devices;

using SensorLibrary;

using System.Reactive.Linq;
using System.Reactive.Concurrency;

namespace SensorLivetView.ViewModels
{
    public class PointStrategyViewModel : ViewModel
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


        PointStateEnum _PointState;

        public PointStateEnum PointState
        {
            get
            { return _PointState; }
            set
            {
                if (_PointState == value)
                    return;
                _PointState = value;
                RaisePropertyChanged("PointState");
            }
        }

        public bool IsEnable
        {
            get { return this.PointState != PointStateEnum.Any; }
            set { this.PointState = PointStateEnum.Any; }
        }

        public bool IsStraight
        {
            get { return this.PointState == PointStateEnum.Straight; }
            set { this.PointState = PointStateEnum.Straight; }
        }

        public bool IsCurved
        {
            get { return this.PointState == PointStateEnum.Curve; }
            set { this.PointState = PointStateEnum.Curve; }
        }


        string _StrategyName;

        public string StrategyName
        {
            get
            { return _StrategyName; }
            set
            {
                if (_StrategyName == value)
                    return;
                _StrategyName = value;
                RaisePropertyChanged("StrategyName");
            }
        }
      
            
    }
}
