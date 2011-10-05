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

        public TrainSensorViewModel(TrainSensorModel model)
            : base(model)
        {
            //this.Model.TimerOverflowed += new EventHandler((sender, e) => RaisePropertyChanged(""));
            this.ReflectorInterval = 5.0;

            ViewModelHelper.BindNotifyChanged(
                model,
                this,
                (sender, e) =>
                {
                    RaisePropertyChanged(e.PropertyName);
                    if (e.PropertyName == "IsMeisuringMode")
                    {
                        RaisePropertyChanged(() => VoltageGraphVm);
                    }

                }
            );
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

        public bool IsSensorDetected
        {
            get { return this.Model.IsDetected; }
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
            this.Model.IsMeisuringMode = true;
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
            if ((!(parameter is string)) || this.Model == null || this.Model.TargetDevice == null)
                return false;

            double castedparam;
            double.TryParse(parameter as string,out castedparam);
            if (castedparam == 0.0)
                return false;

            var state = this.Model.TargetDevice.CurrentState;
            if (state == null)
                return false;

            var threshold = castedparam;
            return state.ReferenceVoltageMinus <= threshold && threshold <= state.ReferenceVoltagePlus;
        }

        private void ChangeDetectingMode(object parameter)
        {
            this.Model.IsDetectingMode = true;
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
                if (this.IsDetectingMode)
                    return new VoltageGraphViewModel();

                var vm = new VoltageGraphViewModel()
                {
                    DataProvider = new GraphPainter<TrainSensor, TrainSensorState>()
                    {
                        DeterminateFunc = (state) => (state != null) ? (state.CurrentVoltage - state.ReferenceVoltageMinus) / state.ReferenceVoltagePlus
                                                                    : 0.0,
                        Device = this.Model.TargetDevice
                    },
                };
                return vm;
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

        public double ThresholdVoltage
        {
            get { return (double)this.Model.ThresholdVoltage; }
            set { this.Model.ThresholdVoltage = (float)value; }
        }
    }
}
