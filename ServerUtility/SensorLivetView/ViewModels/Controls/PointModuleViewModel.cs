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

using SensorLivetView.Models.Devices;
using SensorLivetView.Models;
using SensorViewLibrary;
using SensorLibrary;
using SensorLibrary.Devices;
using SensorLibrary.Devices.PicUsbDevices;
using SensorLibrary.Packet.Data;
using System.Collections.ObjectModel;

namespace SensorLivetView.ViewModels.Controls
{
    public class PointModuleViewModel
        : DeviceViewModel<PointModuleModel>
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

        public PointModuleViewModel()
            : base()
        {
            this.Comparer = new GenericComparer<IDeviceState<IPacketDeviceData>>((a, b) =>
                                                                                {
                                                                                    var pa = a as PointModuleState; var pb = b as PointModuleState;
                                                                                    return pa != null && pb != null && pa.Data.Directions.SequenceEqual(pb.Data.Directions);
                                                                                });
        }

        public override PointModuleModel Model
        {
            get
            {
                return base.Model;
            }
            set
            {
                base.Model = value;
                if (value == null)
                    return;

                ViewModelHelper.BindNotifyChanged(this.Model, this,
                   (sender, e) =>
                   {
                       RaisePropertyChanged(e.PropertyName);

                       if (e.PropertyName == "States")
                       {
                           RaisePropertyChanged(() => PointModels);
                       }
                   });
            }
        }


        public IEnumerable<PointViewModel> PointModels
        {
            get
            {
                return this.Model.States.Select((sta) => new PointViewModel(sta));
            }
        }


    }
}
