using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;
using SensorLibrary;
using System.Collections.ObjectModel;

namespace SensorLivetView.Models.Devices
{
    public class MotherBoardModel : DeviceModel<MotherBoard>
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

        public MotherBoardModel()
        {
            this.PacketReceivedProcess += (sender, e) =>
                {
                    var bef=  e.beforestate as MotherBoardState;
                    var cur = e.state as MotherBoardState;
                    if (bef == null && cur == null)
                        return;
                    else if (bef == null || cur == null)
                    {
                        RaisePropertyChanged("");
                        return;
                    }

                    if (bef.ParentID != cur.ParentID)
                        RaisePropertyChanged(() => BaseParentID);
                };

        }

        public byte BaseParentID
        {
            get { return this.TargetDevice.CurrentState.ParentID; }
            set
            {
                ModifyState(() => this.TargetDevice.CurrentState.ParentID = value);
            }
        }

        private ReadOnlyObservableCollection<MotherBoardPortModel> ports;
        public ReadOnlyObservableCollection<MotherBoardPortModel> Ports
        {
            get
            {
                if (ports == null)
                {
                    var state = this.TargetDevice.CurrentState;
                    if (state == null)
                        return new ReadOnlyObservableCollection<MotherBoardPortModel>(new ObservableCollection<MotherBoardPortModel>());

                    var list = new ObservableCollection<MotherBoardPortModel>();
                    Enumerable.Range(0, state.ModuleTypeLength)
                              .ForEach(i => list.Add(new MotherBoardPortModel(this, i)));

                    this.ports = new ReadOnlyObservableCollection<MotherBoardPortModel>(list);
                }
                return ports;
            }
        }


    }
}
