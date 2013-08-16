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
    public class GateViewModel : RailElementViewModel, IEquatable<GateViewModel>
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

        private GateModel _model;

        public GateViewModel(GateModel model)
        {
            this._model = model;

            ViewModelHelper.BindNotifyChanged(model, this,
                (sender, e) =>
                {
                    RaisePropertyChanged(e.PropertyName);
                });
        }

        public override Geometry CurrentGeometry
        {
            get
            {
                var geo = new EllipseGeometry(this.Bound);

                return geo;
            }
        }

        public void WriteSvgGeometry(XmlTextWriter writer)
        {
            var centerX = Bound.X + Bound.Width/2.0;
            var centerY = Bound.Y + Bound.Height/2.0;
            var r = Bound.Width/2.0;

            writer.WriteStartElement("circle");
            writer.WriteAttributeString("cx", centerX.ToString());
            writer.WriteAttributeString("cy", centerY.ToString());
            writer.WriteAttributeString("r", r.ToString());
            writer.WriteAttributeString("stroke", "black");
            writer.WriteAttributeString("stroke-width", "1");
            writer.WriteEndElement();
        }

        public string Name
        {
            get
            {
                return this._model.Name;
            }
        }

        public override Rect Bound
        {
            get
            {
                return new Rect(this.Position - new Vector(2.5, 2.5), new Size(5.0, 5.0));
            }
        }

        public Point Position
        {
            get { return this._model.Position; }
        }

        #region implementation of IEqualable
        public static bool operator ==(GateViewModel A, GateViewModel B)
        {
            if (ReferenceEquals(A, B))
                return true;
            else if ((object)A == null || (object)B == null)
                return false;
            else
                return (A._model == B._model);
        }
        public static bool operator !=(GateViewModel A, GateViewModel B) { return !(A == B); }

        public bool Equals(GateViewModel other)
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
                return this == (GateViewModel)obj;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }
        #endregion


    }
}
