﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SensorLibrary;
using System.Windows.Media;

using Livet;

using SensorLivetView.Models;
using SensorLivetView.Models.Devices;
using System.Collections.ObjectModel;


namespace SensorLivetView.ViewModels.Controls
{
    public class TrainControllerDeviceViewModel
        : DeviceViewModel<TrainControllerDeviceModel>
    {
        public TrainControllerDeviceViewModel()
            : base()
        {
        }

        public override TrainControllerDeviceModel Model
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

                ViewModelHelper.BindNotifyChanged(this.Model, this,
                   (sender, e) =>
                   {
                       RaisePropertyChanged(e.PropertyName);
                   });
            }
        }

        public double DutyValue
        {
            get { return this.Model.DutyValue; }
            set { this.Model.DutyValue = value; }
        }

        public double RegisteredPeriodValue
        {
            get { return this.Model.RegisteredPeriodValue; }
            set { this.Model.RegisteredPeriodValue = value; }
        }

        public DoubleCollection PrescaleSliderTicks
        {
            get { return new DoubleCollection(new [] { 1.0, 4.0, 16.0 }); }
        }

        public double PrescaleValue
        {
            get { return this.Model.PrescaleValue; }
            set { this.Model.PrescaleValue = value; }
        }

        public bool DirectionValue
        {
            get { return this.Model.DirectionValue; }
            set { this.Model.DirectionValue = value; }
        }

        public float ParamP
        {
            get { return this.Model.ParamP; }
            set { this.Model.ParamP = value; }
        }

        public float ParamI
        {
            get { return this.Model.ParamI; }
            set { this.Model.ParamI = value; }
        }

        public float ParamD
        {
            get { return this.Model.ParamD; }
            set { this.Model.ParamD = value; }
        }

        public double EssentialDutyResolution
        {
            get { return this.Model.EssentialDutyResolution; }
        }

        public double PWMFreqency
        {
            get { return this.Model.PWMFreqency; }
        }

        public double Voltage
        {
            get { return this.Model.Voltage; }
            set { this.Model.Voltage = value; }
        }

        public double MeisuredVoltage
        {
            get { return this.Model.MeisuredVoltage; }
        }

        public double MeisuredVoltage2
        {
            get { return this.Model.MeisuredVoltage2; }
        }

        public TrainControllerMode Mode
        {
            get { return this.Model.Mode; }
            set { this.Model.Mode = value; }
        }

        public bool IsLowerFreqEnabled
        {
            get { return this.Model.IsLowerFreqEnabled; }
            set { this.Model.IsLowerFreqEnabled = value; }
        }

        public int LowerFreq
        {
            get { return this.Model.LowerFreq; }
            set { this.Model.LowerFreq = value; }
        }

        public double Frequency
        {
            get
            {
                return this.Model.Frequency;
             }
            set
            {
                this.Model.Frequency = value;
            }

        }

        public double ActualVoltage
        {
            get
            {
                return this.Model.ActualVoltage;
            }
        }
    }
}
