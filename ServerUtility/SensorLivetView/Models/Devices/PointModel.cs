using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using SensorLibrary;
using System.Collections.ObjectModel;

namespace SensorLivetView.Models.Devices
{
    public class PointModel : NotifyObject
    {
        /*
         * NotifyObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */

        /*
         * リッチクライアントはステートフルであるため、通常のイベントの使用はメモリリークの原因になりやすくなっています。
         * Modelからイベントを発行する場合はNotificatorを使用してください。
         *
         * Notificatorはイベント代替手段です。コードスニペット lnev でCLRイベントと同時に定義できます。
         *
         * Model同士でNotificatorを使用した通知を行う場合はNotificatorHelper、
         * ViewModelへNotificatorを使用した通知を行う場合はViewModelHelperを使用して受信側の登録をしてください。
         */

        public PointModuleModel Parent { get; private set; }
        public int Address { get; private set; }

        public PointModel(PointModuleModel parentmodel, int addr)
        {
            this.Parent = parentmodel;
            this.Address = addr;

            this.Parent.TargetDevice.PacketReceived += (sender, e) =>
                {
                    var bef = e.beforestate as PointModuleState;
                    var cur  = e.state as PointModuleState;
                    if (bef == null && cur == null)
                        return;
                    else if (bef == null || cur == null)
                        RaisePropertyChanged("");

                    if (bef.GetPointState(this.Address) != cur.GetPointState(this.Address))
                    {
                        RaisePropertyChanged(() => State);
                    }
                };

        }

        public PointStateEnum State
        {
            get
            {
                return this.Parent.TargetDevice.CurrentState [this.Address];
            }
            set
            {
                //if (this.Parent.TargetDevice.CurrentState [this.Address] == value)
                //    return;

                var state = this.Parent.TargetDevice.CurrentState;
                state [this.Address] = value;
                if (!this.Parent.TargetDevice.IsHold)
                    this.Parent.TargetDevice.SendPacket(state);
            }
        }
    }
}
