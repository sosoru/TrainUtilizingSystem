using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using RouteVisualizer.EF;

using Livet;

namespace RouteVisualizer.Models
{
    public class GateConnectionModel : Model
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

        GateConnectionData _BaseData;

        public GateConnectionModel(GateConnectionData data)
        {
            this._BaseData = data;

            this.Gates = new ObservableCollection<GateModel>(data.ConnectedGates.Select(g => new GateModel(g)).ToList());

        }

        public ObservableCollection<GateModel> Gates
        {
            get;
            private set;
        }

    }
}
