using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Xml;
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

        //public string ToMarkupGeometryStatement(IEnumerable<Geometry> geos)
        //{
        //    var cmd = "";

        //    foreach (var g in geos)
        //    {
        //        if (g is LineGeometry)
        //        {
        //        }
        //    }
        //}

        public override Geometry CurrentGeometry
        {
            get
            {
                var geogroup = new GeometryGroup();
                //var gateposes = this.LocateGate();

                foreach (var gate in this.Gates)
                {
                    //conn.BasePosition = gateposes [conn];
                    var geo = gate.CurrentGeometry;
                    geogroup.Children.Add(geo);
                }

                foreach (var path in this.Pathes)
                {
                    var geo = path.CurrentGeometry;
                    geogroup.Children.Add(geo);
                }

                return geogroup;
            }
        }

        private Rect _rectCache = Rect.Empty;
        public override Rect Bound
        {
            get
            {
                if (this.Pathes.Count == 0)
                    return Rect.Empty;

                if (this._rectCache == Rect.Empty)
                {
                    var rect = this.Pathes.First().Bound;
                    foreach (var p in this.Pathes)
                        rect.Union(p.Bound);
                    foreach (var conn in this.Gates)
                        rect.Union(conn.Bound);

                    this._rectCache = rect;
                }

                return this._rectCache;
            }
        }

        public void WriteSvgGeometry(XmlTextWriter writer)
        {
            foreach (var path in this.Pathes)
                path.WriteSvgGeometry(writer);
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

                var basepoint = dict[path.PreviousGate];

                dict[path.NextGate] = basepoint + sentvec;
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

        public double PositionWidth
        {
            get
            {
                return this.Bound.Width * ratioWidth;
            }
        }

        public double PositionHeight
        {
            get { return this.Bound.Height * ratioHeight; }
        }

        public double PositionLeft
        {
            get
            {
                var ratio = ratioWidth;

                return this.Bound.Left * ratioWidth;
            }
        }

        public double PositionTop
        {
            get { return this.Bound.Top * ratioHeight; }
        }

        private double ratioWidth
        {
            get { return this._layoutvm.DrawingSize.Width / this._layoutvm.LayoutSize.Width; }
        }

        private double ratioHeight
        {
            get { return this._layoutvm.DrawingSize.Height / this._layoutvm.LayoutSize.Height; }
        }

        #region implementation of IEqualable
        public static bool operator ==(RailViewModel A, RailViewModel B)
        {
            if (ReferenceEquals(A, B))
                return true;
            else if ((object)A == null || (object)B == null)
                return false;
            else
                return (A._model == B._model);
        }
        public static bool operator !=(RailViewModel A, RailViewModel B) { return !(A == B); }

        public bool Equals(RailViewModel other)
        {
            return (this == other);
        }

        public override int GetHashCode()
        {
            return this._model.GetHashCode() ^ this._model.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            try
            {
                return this == (RailViewModel)obj;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }
        #endregion
    }
}
