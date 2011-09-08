﻿using System;
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
            this.ReflectorInterval = 5.0;
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
                    return this.Model != null && this.Model.CurrentState != null&& this.Model.CurrentState.Mode == TrainSensorMode.detecting && this.Model.CurrentState.IsDetected;
                }
                catch
                {
                    return false;
                }
            }
        }

        private DateTime _beforeraised =DateTime.MinValue;
        protected override void ReceivedProcess(IDevice<IDeviceState<IPacketDeviceData>> dev, PacketReceiveEventArgs e)
        {
            var now = DateTime.Now;
            if ((now - this._beforeraised).Milliseconds > 300)
            {
                RaisePropertyChanged("");
                this._beforeraised = now;
            }
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
            return this.Model != null && this.CurrentState != null;
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
            var expr = this.Model != null && this.Model.CurrentState != null;
            try { float.Parse(parameter as string); }
            catch { expr = false; }

            return expr;
        }

        private void ChangeDetectingMode(object parameter)
        {
            var newmodel = this.Model.ChangeDetectingMode(float.Parse((string)parameter));
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
                var model = this.MeisuringModel;
                if (model == null)
                    return new SolidColorBrush(Colors.Black);

                var ptlist = this.MeisuringModel.GetPainter().GetGraphPointCollection(new System.Drawing.RectangleF(0, 0, 200, 100));
                var geogrp = new GeometryGroup();

                for(int i=0; i< ptlist.Count-2;++i)
                {
                    var pta = new Point((double)ptlist[i].X,(double)ptlist[i].Y);
                    var ptb = new Point((double)ptlist[i+1].X, (double)ptlist[i+1].Y);

                    geogrp.Children.Add(new LineGeometry(pta,ptb));
                }

                //guideline
                var axis = new LineGeometry(new Point(0, 0), new Point(200, 0));
                geogrp.Children.Add(axis);

                var dw = new GeometryDrawing(Brushes.Black, new Pen(Brushes.Red, 1), geogrp);
                var b = new DrawingBrush(dw);
                b.Stretch = Stretch.None;
                b.TileMode = TileMode.None;
                b.Freeze();

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
