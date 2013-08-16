using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
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
    public class PathViewModel : RailElementViewModel
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

        private PathModel _model;

        public PathViewModel(PathModel path)
        {
            this._model = path;

            ViewModelHelper.BindNotifyChanged(this._model, this,
                    (sender, e) =>
                    {
                        RaisePropertyChanged(e.PropertyName);
                    });

            this.PreviousGate = new GateViewModel(this._model.PreviousGate);
            this.NextGate = new GateViewModel(this._model.NextGate);

        }

        public GateViewModel PreviousGate
        {
            get;
            private set;
        }

        public GateViewModel NextGate
        {
            get;
            private set;
        }

        public bool IsStraight
        {
            get { return this._model.IsStraight; }
        }

        public double Angle
        {
            get { return this._model.Angle; }
        }

        public double Length
        {
            get { return this._model.Length; }
        }

        public double Radius
        {
            get { return this._model.Radius; }
        }

        public double StartAngle
        { get { return this._model.StartAngle; } }

        public Point CenterPosition
        {
            get { return this._model.CurveCenter; }
        }

        public override Geometry CurrentGeometry
        {
            get
            {
                Geometry geo = null;
                var castedprev = this.PreviousGate;
                var castednext = this.NextGate;

                if (castedprev == null || castednext == null)
                    throw new InvalidOperationException("undrawable connection");

                if (this._model.IsStraight)
                {
                    //todo : calc
                    var startpos = castedprev.Position;
                    var endpos = castednext.Position;
                    //endpos.Offset(this.BaseData.Length, 0.0);
                    geo = new LineGeometry(startpos, endpos);

                }
                else
                {
                    var r = this.Radius;
                    var t = this.Angle.dtor();
                    var startpos = castedprev.Position;
                    var endpos = castednext.Position;
                    var cr = r / Math.Cos(t / 2);

                    //a = sqrt(2) * b * sqrt( 1- cos(t))

                    var centervec = this.CenterPosition - startpos;
                    centervec.Normalize();
                    var tovec = endpos - startpos;
                    tovec.Normalize();

                    var clockwise = centervec.Y * tovec.X - centervec.X * tovec.Y >= 0.0;

                    //var tan = Math.Tan(t /2);
                    //var controlpoint = new Point( tan *(-startpos.Y) + startpos.X,
                    //     tan*startpos.X + startpos.Y);
                    //var segment = new BezierSegment(controlpoint, controlpoint, endpos, true);

                    var segment = new ArcSegment(endpos,
                                                  new Size(r, r),
                                                  0,
                                                  false,
                                                  (clockwise) ? SweepDirection.Clockwise : SweepDirection.Counterclockwise,
                                                  true);
                    geo = new PathGeometry(new[] { new PathFigure(startpos, new[] { segment }, false) });

                }

                return geo;
            }
        }

        public void WriteSvgGeometry(XmlTextWriter writer)
        {
            var geo = this.CurrentGeometry;

            this.PreviousGate.WriteSvgGeometry(writer);
            this.NextGate.WriteSvgGeometry(writer);

            if (geo is LineGeometry)
            {
                var line = (LineGeometry)geo;
                writer.WriteStartElement("line");
                writer.WriteAttributeString("x1", line.StartPoint.X.ToString());
                writer.WriteAttributeString("y1", line.StartPoint.Y.ToString());
                writer.WriteAttributeString("x2", line.EndPoint.X.ToString());
                writer.WriteAttributeString("y2", line.EndPoint.Y.ToString());
                writer.WriteAttributeString("stroke", "black");
                writer.WriteAttributeString("stroke-width", "1");
                writer.WriteEndElement();

            }
            else if (geo is PathGeometry)
            {
                var path = (PathGeometry)geo;
                foreach (var f in path.Figures)
                {
                    writer.WriteStartElement("path");
                    writer.WriteAttributeString("d", f.ToString());
                    writer.WriteAttributeString("stroke", "black");
                    writer.WriteAttributeString("fill", "none");
                    writer.WriteEndElement();

                }
            }
        }

        public override Rect Bound
        {
            get
            {
                return new Rect(this.PreviousGate.Position, this.NextGate.Position);
            }
        }

        #region implementation of IEqualable
        public static bool operator ==(PathViewModel A, PathViewModel B)
        {
            if (ReferenceEquals(A, B))
                return true;
            else if ((object)A == null || (object)B == null)
                return false;
            else
                return (A._model == B._model);
        }
        public static bool operator !=(PathViewModel A, PathViewModel B) { return !(A == B); }

        public bool Equals(PathViewModel other)
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
                return this == (PathViewModel)obj;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }
        #endregion


    }
}
