using System;
using Drawing = System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SensorLibrary;
using Livet;
using Livet.Command;
using System.ComponentModel;
using System.Reactive.Linq;

namespace SensorLivetView.ViewModels.Controls
{
    public class TrainSensorViewModel
        : DeviceViewModel<TrainSensor>
    {
        public TrainSensorViewModel()
            : this(null)
        {
        }

        public TrainSensorViewModel(TrainSensor cens)
            : base(cens)
        {
            //this.Model.TimerOverflowed += new EventHandler((sender, e) => RaisePropertyChanged(""));
        }

        public override TrainSensor Model
        {
            get
            {
                return base.Model;
            }
            set
            {

                base.Model = value;
                RaisePropertyChanged("");
            }
        }

        public DetectingTrainSensor DetectingModel
        {
            get
            {
                return this.Model as DetectingTrainSensor;
            }
        }

        public MeisuringTrainSensor MeisuringModel
        {
            get
            {
                return this.Model as MeisuringTrainSensor;
            }
        }

        public bool IsDetectingMode
        {
            get
            {
                return this.Model != null && this.Model.CurrentState != null && this.Model.CurrentState.Mode == TrainSensorMode.detecting;
            }
        }

        public bool IsMeisuringMode
        {
            get
            {
                return this.Model != null && this.Model.CurrentState != null && this.Model.CurrentState.Mode == TrainSensorMode.meisuring;
            }
        }

        public bool IsSensorDetected
        {
            get
            {
                try
                {
                    return this.Model != null && this.Model.CurrentState != null && this.Model.CurrentState.IsDetected;
                }
                catch
                {
                    return false;
                }
            }
        }

        protected override void ReceivedProcess(IDevice<IDeviceState<IPacketDeviceData>> dev, PacketReceiveEventArgs e)
        {
            RaisePropertyChanged("");
            //RaisePropertyChanged("VoltageGraphVm");
        }

        #region ChangeMeisuringModeCommand
        DelegateCommand _ChangeMeisuringModeCommand;

        public DelegateCommand ChangeMeisuringModeCommand
        {
            get
            {
                if (_ChangeMeisuringModeCommand == null)
                    _ChangeMeisuringModeCommand = new DelegateCommand(ChangeMeisuringMode, CanChangeMeisuringMode);
                return _ChangeMeisuringModeCommand;
            }
        }

        private bool CanChangeMeisuringMode()
        {
            return this.Model != null;
        }

        private void ChangeMeisuringMode()
        {
            var newmodel = this.Model.ChangeMeisuringMode();
            this.Model.Observe(null);
            this.Model = newmodel;
        }
        #endregion

        #region ChangeDetectingModeCommand
        DelegateCommand<object> _ChangeDetectingModeCommand;

        public DelegateCommand<object> ChangeDetectingModeCommand
        {
            get
            {
                if (_ChangeDetectingModeCommand == null)
                    _ChangeDetectingModeCommand = new DelegateCommand<object>(ChangeDetectingMode, CanChangeDetectingMode);
                return _ChangeDetectingModeCommand;
            }
        }

        private bool CanChangeDetectingMode(object parameter)
        {
            var expr = this.Model != null;
            try { float.Parse(parameter as string); }
            catch { expr = false; }

            return expr;
        }

        private void ChangeDetectingMode(object parameter)
        {
            var newmodel = this.Model.ChangeDetectingMode((float)parameter);
            this.Model.Observe(null);
            this.Model = newmodel;
        }
        #endregion

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

        public VoltageGraphViewModel VoltageGraphVm
        {
            get
            {
                if (this.MeisuringModel == null)
                    return new VoltageGraphViewModel();

                var vm = new VoltageGraphViewModel()
                {
                    Painter = this.MeisuringModel.GetPainter(),
                };
                return vm;
            }
        }

        public Brush CurrentGraphBrush
        {
            get
            {
                var ptlist = this.MeisuringModel.GetPainter().GetGraphPointCollection(new System.Drawing.RectangleF(0, 0, 100, 100));
                var fig = new PathFigure();

                foreach (var pt in ptlist)
                    fig.Segments.Add(new LineSegment(new Point((double)pt.X, (double)pt.Y), true));

                var geo = new PathGeometry(new PathFigure [] { fig }, FillRule.EvenOdd, Transform.Identity);
                var dw = new GeometryDrawing(Brushes.Black, new Pen(Brushes.Black, 1), geo);
                var b = new DrawingBrush(dw);

                return b;
            }
        }

        double _ReflectorInterval;

        public double ReflectorInterval
        {
            get
            { return _ReflectorInterval; }
            set
            {
                if (_ReflectorInterval == value)
                    return;
                _ReflectorInterval = value;
                RaisePropertyChanged("ReflectorInterval");
            }
        }


        public bool IsSpeedCalculatable
        {
            get
            {
                double tr = double.NaN;
                try
                {
                    tr = this.DetectingModel.CalculateSpeed(this.ReflectorInterval);
                }
                catch
                {
                    return false;
                }
                return tr != double.NaN;
            }
        }

        public double Speed
        {
            get
            {
                if (this.DetectingModel == null)
                    return double.NaN;

                try
                {
                    return this.DetectingModel.CalculateSpeed(this.ReflectorInterval);
                }
                catch
                {
                    return double.NaN;
                }
            }
        }

        public double TrainSpeed
        {
            get
            {
                return this.DetectingModel.CalculateSpeed(this.ReflectorInterval);
            }
        }


    }
}
