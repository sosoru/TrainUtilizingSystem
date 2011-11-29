using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

using Livet;
using Livet.Command;
using Livet.Messaging;
using Livet.Messaging.File;
using Livet.Messaging.Window;

using RouteVisualizer.Models;

namespace RouteVisualizer.ViewModels
{
    public class RailViewModel : RailElementViewModel
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

        private RailModel _model;
        private LayoutViewModel _layoutvm;

        public RailViewModel(RailModel model, LayoutViewModel lyout)
        {
            this._model = model;
            this._layoutvm = lyout;

            this.Pathes = ViewModelHelper.CreateReadOnlyNotifyDispatcherCollection(this._model.Pathes,
                                                                                    m => new PathViewModel(m),
                                                                                    DispatcherHelper.UIDispatcher);
            this.Gates = ViewModelHelper.CreateReadOnlyNotifyDispatcherCollection(this._model.Connections,
                                                                                conn => new GateViewModel(conn),
                                                                                DispatcherHelper.UIDispatcher);

            ViewModelHelper.BindNotifyChanged(this._model, this, (sender, e) =>
                {
                    RaisePropertyChanged(e.PropertyName);
                });
        }

        public ReadOnlyObservableCollection<PathViewModel> Pathes { get; private set; }

        public ReadOnlyObservableCollection<GateViewModel> Gates { get; private set; }

        public bool IsMirrored
        {
            get { return this._model.IsMirrored; }
            set { this._model.IsMirrored = value; }

        }

        public virtual Geometry CurrentGeometry
        {
            get
            {
                var geogroup = new GeometryGroup();
                //var gateposes = this.LocateGate();

                foreach (var gate in this.Gates)
                {
                    //conn.BasePosition = gateposes [conn];
                    var geo  = gate.CurrentGeometry;
                    geogroup.Children.Add(geo);
                }

                foreach (var path in this.Pathes)
                {
                    var geo  =path.CurrentGeometry;
                    geogroup.Children.Add(geo);
                }

                return geogroup;
            }
        }

        public virtual Rect Bound
        {
            get
            {
                var rect = new Rect();
                foreach (var p in this.Pathes)
                    rect.Union(p.Bound);
                foreach (var conn in this.Gates)
                    rect.Union(conn.Bound);

                return rect;
            }
        }


        public IDictionary<GateViewModel, Point> LocateGate()
        {
            if (this.Pathes == null || this.Pathes.Count == 0)
                return new Dictionary<GateViewModel, Point>();

            var dict = new Dictionary<GateViewModel, Point>();
            foreach (var conn in this.Gates)
                dict.Add(conn, conn.Position);

            foreach (var path in this.Pathes)
            {
                var sentvec = path.Bound.BottomLeft - path.Bound.TopRight;

                var basepoint = dict [path.PreviousGate];

                // basepoint += sentvec;

                //check overwrite
                //var zero = new Point();
                //if (dict [path.NextGate] != zero && dict [path.NextGate] != sentvec)
                //{
                //    throw new InvalidOperationException(string.Format("gate position mismatching : {0}", path.NextGate.ToString()));
                //}

                dict [path.NextGate] = basepoint + sentvec;
            }

            return dict;
        }

        public bool IsPathValidated
        {
            get
            {
                try
                {
                    LocateGate();
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }

    }
}
