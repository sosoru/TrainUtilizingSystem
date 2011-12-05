using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;

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
    public class LayoutViewModel : ViewModel
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

        private LayoutModel _model;

        public LayoutViewModel(LayoutModel model)
        {
            this._model = model;

            ViewModelHelper.BindNotifyChanged(this._model, this, (sender, e) =>
                {
                    this.RaisePropertyChanged(e.PropertyName); 
                });

            this.Rails = ViewModelHelper.CreateReadOnlyNotifyDispatcherCollection(this._model.Rails,
                                                                                    m => new RailViewModel(m, this),
                                                                                    DispatcherHelper.UIDispatcher);
            this.Gates = ViewModelHelper.CreateReadOnlyNotifyDispatcherCollection(this._model.Connections,
                                                                                    conn => new GateConnectionViewModel(conn),
                                                                                    DispatcherHelper.UIDispatcher);

            this.SelectedRail = this.Rails.First();
            RefreshDrawing();
        }

        public ReadOnlyObservableCollection<RailViewModel> Rails { get; private set; }
        public ReadOnlyObservableCollection<GateConnectionViewModel> Gates { get; private set; }

        public Size LayoutSize
        {
            get { return this._model.LayoutSize; }
            set { this._model.LayoutSize = value; }
        }

        public Drawing CurrentDrwaing
        {
            get;
            private set;
        }

        public Brush CurrentBrush
        {
            get
            {
                return new DrawingBrush(this.CurrentDrwaing);
            }
        }

        public void RefreshDrawing()
        {
            var dr = new DrawingGroup();

            this.Rails.ToList()
                    .ForEach(r => dr.Children.Add(new GeometryDrawing(Brushes.Transparent, new Pen(Brushes.Black, 1.0), r.CurrentGeometry)));

            this.CurrentDrwaing = dr;
        }


        RailViewModel _SelectedRail;

        public RailViewModel SelectedRail
        {
            get
            { return _SelectedRail; }
            set
            {
                if (_SelectedRail == value)
                    return;
                _SelectedRail = value;
                RaisePropertyChanged("SelectedRail");
                RaisePropertyChanged(() => Railinfo);
            }
        }

        public string Railinfo
        {
            get
            {
                return this.Rails.IndexOf(this.SelectedRail).ToString();
            }
        }
      

        Size _DrawingSize;

        public Size DrawingSize
        {
            get
            { return _DrawingSize; }
            set
            {
                if (_DrawingSize == value)
                    return;
                _DrawingSize = value;
                RaisePropertyChanged("DrawingSize");
            }
        }
      

    }
}
