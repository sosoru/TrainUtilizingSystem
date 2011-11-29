using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Command;
using Livet.Messaging;
using Livet.Messaging.File;
using Livet.Messaging.Window;

using RouteVisualizer.Models;

namespace RouteVisualizer.ViewModels
{
    public class GateConnectionViewModel : ViewModel
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
        private GateConnectionModel _model;

        public GateConnectionViewModel(GateConnectionModel model)
        {
            this._model = model;

            ViewModelHelper.BindNotifyChanged(model, this,
                (sender, e)
                     =>
                {
                    RaisePropertyChanged(e.PropertyName);
                });

            this.Gates = ViewModelHelper.CreateReadOnlyNotifyDispatcherCollection
                (model.Gates, g => new GateViewModel(g), DispatcherHelper.UIDispatcher);
        }

        public ReadOnlyObservableCollection<GateViewModel> Gates
        { get; private set; }


    }
}
