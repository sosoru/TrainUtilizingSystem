using System;
//using Drawing = System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using SensorLibrary;
using SensorLibrary.Packet.Data;
using SensorLibrary.Devices;
using SensorLibrary.Devices.PicUsbDevices;

using Livet;
using Livet.Command;
using System.ComponentModel;
using System.Reactive.Linq;
using SensorLivetView.Models.Devices;

namespace SensorLivetView.ViewModels.Controls
{
    public class TrainSensorViewModel
        : DeviceViewModel<TrainSensorModel>
    {

        public TrainSensorViewModel()
            : base()
        {
            //this.Model.TimerOverflowed += new EventHandler((sender, e) => RaisePropertyChanged(""));
            //this.ReflectorInterval = 5.0;

        }

        public override TrainSensorModel Model
        {
            get
            {
                return base.Model;
            }
            set
            {
                base.Model = value;
                if (value == null)
                    return;

                ViewModelHelper.BindNotifyChanged(
                    this.Model,
                    this,
                    (sender, e) =>
                    {
                        RaisePropertyChanged(e.PropertyName);

                        if (e.PropertyName == "IsMeisuringMode"
                            || e.PropertyName == "IsDetectingMode")
                        {
                            RaisePropertyChanged(() => VoltageGraphVm);
                            RaisePropertyChanged(() => GraphVisiblity);
                            RaisePropertyChanged(() => DetectingVisiblity);
                        }

                    }
                );
            }
        }

        public bool IsDetectingMode
        {
            get { return this.Model.IsDetectingMode; }
            set { this.Model.IsDetectingMode = value; }
        }

        public bool IsMeisuringMode
        {
            get { return this.Model.IsMeisuringMode; }
            set { this.Model.IsMeisuringMode = value; }
        }

        public Visibility GraphVisiblity
        {
            get { return (this.IsMeisuringMode) ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility DetectingVisiblity
        {
            get { return (this.IsDetectingMode) ? Visibility.Visible : Visibility.Collapsed; }
        }

        public bool IsDetected
        {
            get { return this.Model.IsDetected; }
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

        public VoltageGraphViewModel VoltageGraphVm
        {
            get
            {
                if (this.IsDetectingMode)
                    return new VoltageGraphViewModel();

                var vm = new VoltageGraphViewModel()
                {
                    DataProvider = new GraphPainter<TrainSensor, TrainSensorState>()
                    {
                        DeterminateFunc = (state) => (state != null && state.Data != null) ? (state.CurrentVoltage - state.ReferenceVoltageMinus) / state.ReferenceVoltagePlus
                                                                    : 0.0,
                        Device = this.Model.TargetDevice
                    },
                };
                return vm;
            }
        }

        public double ReflectorInterval
        {
            get { return this.Model.ReflactorInterval; }
            set { this.Model.ReflactorInterval = value; }
        }

        public double Speed
        {
            get
            {
                if (this.Model != null)
                    return this.Model.Speed;
                else
                    return 0.0;
            }
        }

        public string Mode
        {
            get { return Enum.GetName(typeof(TrainSensorMode), this.Model.Mode); }
            set
            {
                TrainSensorMode name = TrainSensorMode.meisuring;
                try
                {
                    name = (TrainSensorMode)Enum.Parse(typeof(TrainSensorMode), value);
                }
                catch { }
                this.Model.Mode = name;
            }

        }

        public double ThresholdVoltageLower
        {
            get { return (double)this.Model.ThresholdVoltageLower; }
            set { this.Model.ThresholdVoltageLower = (float)value; }
        }

        public double ThresholdVoltageHigher
        {
            get { return (double)this.Model.ThresholdVoltageHigher; }
            set { this.Model.ThresholdVoltageHigher = (float)value; }
        }

    }
}
