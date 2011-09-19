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
    public class PointModuleViewModel
        : DeviceViewModel<PointModule>
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

        public PointModuleViewModel(PointModule pm)
            : base(pm)
        {
        }

        public PointModuleViewModel()
            : this(null)
        { }

        public IList<PointStateClass> StateList
        {
            get
            {
                return Enumerable.Range(0, this.Model.CurrentState.StateLength)
                                 .Select((i) => new PointStateClass()
                                 {
                                     ViewModel = this,
                                     StateNo = i,
                                 })
                                 .ToList();
            }
        }

        public class PointStateClass
        {
            public PointModuleViewModel ViewModel { get; set; }
            public int StateNo { get; set; }
            public PointStateEnum State
            {
                get { return this.ViewModel.Model.CurrentState[StateNo]; }
                set { this.ViewModel.Model.CurrentState[StateNo] = value; }
            }

            public bool IsStraight
            {
                get { return State == PointStateEnum.Straight; }
                set
                {
                    this.State = PointStateEnum.Straight;
                }
            }

            public bool IsCurved
            {
                get { return State == PointStateEnum.Curve; }
                set
                {
                    this.State = PointStateEnum.Curve;
                }
            }


            #region ChangeStraightCommand
            DelegateCommand _ChangeStraightCommand;

            public DelegateCommand ChangeStraightCommand
            {
                get
                {
                    if (_ChangeStraightCommand == null)
                        _ChangeStraightCommand = new DelegateCommand(ChangeStraight, CanChangeStraight);
                    return _ChangeStraightCommand;
                }
            }

            private bool CanChangeStraight()
            {
                return this.ViewModel != null;
            }

            private void ChangeStraight()
            {
                this.IsStraight = true;
                if (this.ViewModel.SendPacketCommand.CanExecute())
                    this.ViewModel.SendPacketCommand.Execute();
            }
            #endregion


            #region ChangeCurvedCommand
            DelegateCommand _ChangeCurvedCommand;

            public DelegateCommand ChangeCurvedCommand
            {
                get
                {
                    if (_ChangeCurvedCommand == null)
                        _ChangeCurvedCommand = new DelegateCommand(ChangeCurved, CanChangeCurved);
                    return _ChangeCurvedCommand;
                }
            }

            private bool CanChangeCurved()
            {
                return this.ViewModel != null;
            }

            private void ChangeCurved()
            {
                this.IsCurved = true;
                if (this.ViewModel.SendPacketCommand.CanExecute())
                    this.ViewModel.SendPacketCommand.Execute();
            }
            #endregion
      
        }
    }
}
