using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

using RouteVisualizer.Models;
using RouteVisualizer.Graph;

using Livet;

namespace RouteVisualizer.Models
{
    public class LayoutModel : NotifyObject
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
        public LayoutModel()
        {
            this.Rails = new ObservableCollection<RailModel>();
            this.PhysicalConnections = new ObservableCollection<GateConnectionModel>();
            this.TheoreticalConnections = new ObservableCollection<GateConnectionModel>();
        }

        public ObservableCollection<RailModel> Rails { get; private set; }

        public ObservableCollection<GateConnectionModel> PhysicalConnections { get; private set; }

        public ObservableCollection<GateConnectionModel> TheoreticalConnections { get; private set; }

        Size _LayoutSize;

        public Size LayoutSize
        {
            get
            { return _LayoutSize; }
            set
            {
                if (_LayoutSize == value)
                    return;
                _LayoutSize = value;
                RaisePropertyChanged("LayoutSize");
            }
        }

        public void InitializeTheoreticalConnections()
        {

        }


    }
}
