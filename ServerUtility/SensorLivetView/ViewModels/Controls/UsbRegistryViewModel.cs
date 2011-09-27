using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using LibUsbDotNet;
using LibUsbDotNet.Main;

using Livet;
using Livet.Command;
using Livet.Messaging;
using Livet.Messaging.File;
using Livet.Messaging.Window;

using SensorLivetView.Models;
using SensorViewLibrary.ViewModels;

namespace SensorLivetView.ViewModels.Controls
{
    public class UsbRegistryViewModel
        : ModeledViewModel<UsbRegistry>
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

        
        bool _IsReceiving;

        public bool IsReceiving
        {
            get
            { return _IsReceiving; }
            set
            {
                if (_IsReceiving == value)
                    return;
                _IsReceiving = value; 
                RaisePropertyChanged("IsReceiving");
            }
        }
             

        public string SymbolicName
        {
            get { return this.Model.SymbolicName; }
        }
        
    }
}
