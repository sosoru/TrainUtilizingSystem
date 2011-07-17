using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SensorLibrary;                                                                                                                                                                                                                                                                                                        

namespace SensorViewModels
{
    public class TrainSensorViewModel
        : DeviceViewModel<TrainSensor>
    {
        public TrainSensorViewModel(TrainSensor cens)
            : base(cens) 
        {
            this.Model.TimerOverflowed += new EventHandler((sender, e) => OnPropertyChanged(""));
        }

        public ICommand ChangeMeisuringModeComand
        {
            get
            {
                return new RelayCommand((param) => this.Model.ChangeMeisuringMode(),
                                        (param) => this.Model != null);
                
            }
        }

        public ICommand ChangeDetectingModeCommand
        {
            get
            {
                return new RelayCommand((param) => this.Model.ChangeDetectingMode(float.Parse(param as string)),
                                        (param) => this.Model != null);
            }
        }

        //public BitmapSource CurrentGraph
        //{
        //    get
        //    {
        //        var bmp = new Bitmap(500, 100);
        //        var painter = this.Model.GetPainter();
        //        painter.DrawPoints(Graphics.FromImage(bmp));

        //        //return  BitmapSource.Create(bmp.Width,bmp.Height, bmp.VerticalResolution,
        //    }
        //}

        public Brush CurrentGraphBrush
        {
            get
            {
                var ptlist = this.Model.GetPainter().GetGraphPointCollection();
                var fig = new PathFigure();

                foreach (var pt in ptlist)
                    fig.Segments.Add(new LineSegment(new Point((double)pt.X, (double)pt.Y), true));

                var geo = new PathGeometry(new PathFigure[] { fig }, FillRule.EvenOdd, Transform.Identity);
                var dw = new GeometryDrawing(Brushes.Black, new Pen(Brushes.Black, 1), geo);
                var b = new DrawingBrush(dw);
                
                return b;
            }
        }
        
    }
}
