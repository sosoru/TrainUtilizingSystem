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

using SensorLivetView.ViewModels.Controls;

using SensorLibrary;

using System.Reactive.Linq;
using System.Reactive.Concurrency;

using System.Collections.ObjectModel;

namespace SensorLivetView.ViewModels
{
    public class ManyPointStrategyViewModel : PointStrategyViewModel
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

        public ObservableCollection<PointModel> Points { get; private set; }
        public ObservableCollection<PointModel> InversePoints { get; private set; }

        public ManyPointStrategyViewModel()
        {
            this.Points = new ObservableCollection<PointModel>();
            this.InversePoints = new ObservableCollection<PointModel>();

        }

        public override void ChangePoint()
        {
            PointStateEnum positive, negative;

            if (this.PointState == PointStateEnum.Straight)
            {
                positive = PointStateEnum.Straight;
                negative = PointStateEnum.Curve;
            }
            else if (this.PointState == PointStateEnum.Curve)
            {
                positive = PointStateEnum.Curve;
                negative = PointStateEnum.Straight;
            }
            else
            {
                positive = negative = PointStateEnum.Any;
            }

            var devices = Points.Concat(InversePoints).Select(pt => pt.Parent.TargetDevice)
                                                      .Distinct();

            devices.ForEach(dev => dev.IsHold = true);

            try
            {
                Points.ForEach(pm => pm.State = positive);
                InversePoints.ForEach(pm => pm.State = negative);
            }
            finally
            {
                devices.ForEach(dev =>
                {
                    dev.SendPacket();
                    dev.IsHold = false;
                    System.Threading.Thread.Sleep(1000);
                });
            }

        }
    }
}
