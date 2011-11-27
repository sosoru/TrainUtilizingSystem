using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RouteVisualizer.EF;

using Livet;

namespace RouteVisualizer.Models
{
    public class GateConnectionModel : NotifyObject
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
        public GateConnectionData BaseData
        {
            get{ return _BaseData ?? (this._BaseData = new GateConnectionData());}
            set
            {
                this._BaseData = value;
            }
        }
    }
}
