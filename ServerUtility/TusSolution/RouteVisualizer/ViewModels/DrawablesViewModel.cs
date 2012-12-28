using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

using Livet;
using Livet.Command;
using Livet.Messaging;
using Livet.Messaging.File;
using Livet.Messaging.Window;

using RouteVisualizer.Models;

namespace RouteVisualizer.ViewModels
{
    public class DrawablesViewModel : ViewModel
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

        public virtual ReadOnlyObservableCollection<IDrawable> Drawables { get; protected set; }

        public Size DrawingSize { get; set; }

        public DrawablesViewModel()
            : base()
        {
        }

        private GeometryDrawing _curdrawing;
        public Drawing CurrentDrawing
        {
            get
            {
                var drawables = this.Drawables.ToList();
                var group = new GeometryGroup();
                
                foreach (var d in drawables)
                {
                    var geo = d.CurrentGeometry;
                    
                    group.Children.Add(d.CurrentGeometry);
                }

                if (_curdrawing == null)
                {
                    var dr = new GeometryDrawing();
                    dr.Brush = Brushes.White;
                    dr.Pen = new Pen(Brushes.Black, 1.0);
                    this._curdrawing = dr;
                }

                _curdrawing.Geometry = group;

                return this._curdrawing;
            }
        }

    }
}
