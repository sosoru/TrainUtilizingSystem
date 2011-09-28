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
        : ModeledViewModel<UsbRegistryModel>
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

        public string Name
        {
            get
            {
                if (this.Model == null)
                    return "model is not selected";
                else
                {
                    return this.Model.Registry.SymbolicName;
                }
            }

        }

        public bool IsSelected
        {
            get
            {
                if (this.Model == null)
                    return false;
                else
                    return this.Model.IsRegistered;

            }
            set
            {
                if (this.Model != null)
                    this.Model.IsRegistered = value;
            }
        }


        #region SelectDeviceCommand
        DelegateCommand _SelectDeviceCommand;

        public DelegateCommand SelectDeviceCommand
        {
            get
            {
                if (_SelectDeviceCommand == null)
                    _SelectDeviceCommand = new DelegateCommand(SelectDevice, CanSelectDevice);
                return _SelectDeviceCommand;
            }
        }

        private bool CanSelectDevice()
        {
            return this.Model != null;
        }

        private void SelectDevice()
        {
            this.IsSelected = true;
        }
        #endregion


        #region UnSelectCommand
        DelegateCommand _UnSelectCommand;

        public DelegateCommand UnSelectCommand
        {
            get
            {
                if (_UnSelectCommand == null)
                    _UnSelectCommand = new DelegateCommand(UnSelect, CanUnSelect);
                return _UnSelectCommand;
            }
        }

        private bool CanUnSelect()
        {
            return this.Model != null;
        }

        private void UnSelect()
        {
            this.IsSelected = false;
        }
        #endregion
      
    }
}
