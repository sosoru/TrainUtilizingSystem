using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
//using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Windows.Media;
using Microsoft.WindowsAPICodePack.DirectX.Direct2D1;
using Microsoft.WindowsAPICodePack.DirectX.DirectWrite;

using DWrite = Microsoft.WindowsAPICodePack.DirectX.DirectWrite;
using Microsoft.WindowsAPICodePack.DirectX.DXGI;
using Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent;
using System.Windows.Interop;

using Drawing = System.Drawing;

using SensorLivetView.ViewModels.Controls;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows.Threading;

namespace SensorLivetView.Views
{
    /// <summary>
    /// VoltageGraphView.xaml の相互作用ロジック
    /// </summary>
    public partial class VoltageGraphView : UserControl
    {
        private D2DFactory d2dfactory;
        private DWriteFactory dwfactory;
        private RenderTarget renderTarget;

        // Maintained simply to detect changes in the interop back buffer
        IntPtr m_pIDXGISurfacePreviousNoRef;

        public VoltageGraphView()
        {
            InitializeComponent();
        }

        public VoltageGraphViewModel ViewModel
        {
            get { return this.DataContext as VoltageGraphViewModel; }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.d2dfactory = D2DFactory.CreateFactory(D2DFactoryType.SingleThreaded);
            this.dwfactory = DWriteFactory.CreateFactory();

            this.dxImage.HWNDOwner = (new WindowInteropHelper(Window.GetWindow(this))).Handle;
            this.dxImage.OnRender = DoRender;

            Observable.Interval(new TimeSpan(0, 0, 0, 0, 200), Scheduler.ThreadPool)
                      .ObserveOn(System.Threading.SynchronizationContext.Current)
                      .Subscribe(_ => this.dxImage.RequestRender());
        }

        private void DoRender(IntPtr pIDXGISurface)
        {
            //if changed interop back buffer ?
            if (pIDXGISurface != m_pIDXGISurfacePreviousNoRef)
            {
                this.m_pIDXGISurfacePreviousNoRef = pIDXGISurface;

                var surf = Surface.FromNativeSurface(pIDXGISurface);
                var surfdesc = surf.Description;
                var renderprop = new RenderTargetProperties(RenderTargetType.Hardware,
                                                            new PixelFormat(Format.Unknown, AlphaMode.Premultiplied),
                                                            96,
                                                            96,
                                                            RenderTargetUsage.None,
                                                            Microsoft.WindowsAPICodePack.DirectX.Direct3D.FeatureLevel.Default);

                try
                {
                    this.renderTarget = this.d2dfactory.CreateDxgiSurfaceRenderTarget(surf, renderprop);
                }
                catch
                {
                    return;
                }

                renderTarget.BeginDraw();
                renderTarget.Clear(new ColorF(1, 1, 1, 0));
                renderTarget.EndDraw();
            }

            renderTarget.BeginDraw();

            renderTarget.Clear(new ColorF(0, 0, 0, 1));
            GraphPaint(this.renderTarget);

            renderTarget.EndDraw();
        }

        private void GraphPaint(RenderTarget target)
        {
            try
            {
                if (this.ViewModel != null && this.ViewModel.Painter != null)
                {
                    var size = target.Size;
                    var points = this.ViewModel.Painter.GetGraphPointCollection(new Drawing.RectangleF(0, 0, size.Width, size.Height));
                    var b = target.CreateSolidColorBrush(new ColorF(0, 1, 0, 1));

                    for (int i = 0; i < points.Count - 1; i++)
                    {
                        target.DrawLine(new Point2F((float)i * size.Width / (float)points.Count, points[i].Y),
                                        new Point2F((float)(i + 1) * size.Width / (float)points.Count, points[i + 1].Y),
                                        b,
                                        1.0F);

                    }
                }
            }
            catch { Console.WriteLine(); }
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //if (e.NewValue == null)
            //    return;

            //if (this.ViewModel != null && this.dxImage != null)
            //{
            //    this.dxImage.RequestRender();
            //}
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.dxImage != null)
                this.dxImage.SetPixelSize((uint)this.ActualWidth, (uint)this.ActualHeight);
        }
    }
}