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
    public class RailCanvasViewModel : ViewModel
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

        public ObservableCollection<IDrawable> Drawables { get; private set; }
        public PixelScaler DrawingScaler { get; set; }

        public RailCanvasViewModel()
            : base()
        {
            this.Drawables = new ObservableCollection<IDrawable>();
        }

        public virtual WriteableBitmap CreateBitmap(Size bmppxSize)
        {
            var drawables = this.Drawables.ToList();
            var bmp = new WriteableBitmap((int)bmppxSize.Width, (int)bmppxSize.Height, 96.0, 96.0, PixelFormats.Bgr24, BitmapPalettes.WebPalette);
            var srcRect = new Rect();

            drawables.ForEach((d) => srcRect.Union(d.Bound));            
            
            foreach (var d in drawables)
            {
                var b = new DrawingBrush(d.CurrentDrawing);
                b.Transform = this.DrawingScaler.GetTransform();
                
                
                
            }
            
        }
        
    }
}

public abstract class Scaler
{
    public virtual Transform GetTransform();
}

public class PixelScaler
    : Scaler
{
    public Rect srcRect { get; set; }
    public Size pxSize { get; set; }
    public override Transform GetTransform()
    {
        return new ScaleTransform(pxSize.Width, pxSize.Height);
    }
}
