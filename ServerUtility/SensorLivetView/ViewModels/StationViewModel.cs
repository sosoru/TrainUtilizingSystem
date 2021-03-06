﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Command;
using Livet.Messaging;
using Livet.Messaging.File;
using Livet.Messaging.Window;

using SensorLivetView.Models;
using SensorLivetView.Models.Devices;

using SensorLibrary;
using SensorLibrary.Packet.Data;
using SensorLibrary.Devices;
using SensorLibrary.Devices.PicUsbDevices;

using System.Reactive.Linq;
using System.Reactive.Concurrency;


namespace SensorLivetView.ViewModels
{
    public class StationViewModel : ViewModel
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

        TrainControllerDeviceModel _Controller;

        public TrainControllerDeviceModel Controller
        {
            get
            { return _Controller; }
        }

        TrainSensorModel _SensorStartingHalt;

        public TrainSensorModel SensorStartingHalt
        {
            get
            { return _SensorStartingHalt; }
        }


        TrainSensorModel _SensorHalt;

        public TrainSensorModel SensorHalt
        {
            get
            { return _SensorHalt; }
        }


        bool _ImmediateStopFlag;

        public bool ImmediateStopFlag
        {
            get
            { return _ImmediateStopFlag; }
            set
            {
                if (_ImmediateStopFlag == value)
                    return;
                _ImmediateStopFlag = value;
                RaisePropertyChanged("ImmediateStopFlag");
            }
        }


        bool _IsHalt;

        public bool IsHalt
        {
            get
            { return _IsHalt; }
            set
            {
                if (_IsHalt == value)
                    return;
                _IsHalt = value;
                RaisePropertyChanged("IsHalt");
            }
        }

        double _IntervalStoppingPos;

        public double IntervalStoppingPos
        {
            get
            { return _IntervalStoppingPos; }
            set
            {
                if (_IntervalStoppingPos == value)
                    return;
                _IntervalStoppingPos = value;
                RaisePropertyChanged("IntervalStoppingPos");
            }
        }


        double _LeavingVoltage;

        public double LeavingVoltage
        {
            get
            { return _LeavingVoltage; }
            set
            {
                if (_LeavingVoltage == value)
                    return;
                _LeavingVoltage = value;
                RaisePropertyChanged("LeavingVoltage");
            }
        }


        double _DutyWhenHalt;

        public double DutyWhenHalt
        {
            get
            { return _DutyWhenHalt; }
            set
            {
                if (_DutyWhenHalt == value)
                    return;
                _DutyWhenHalt = value;
                RaisePropertyChanged("DutyWhenHalt");
            }
        }


        TimeSpan _StoppingTime;

        public TimeSpan StoppingTime
        {
            get
            { return _StoppingTime; }
            set
            {
                if (_StoppingTime == value)
                    return;
                _StoppingTime = value;
                RaisePropertyChanged("StoppingTime");
            }
        }

        int _StepResolution;

        public int StepResolution
        {
            get
            { return _StepResolution; }
            set
            {
                if (_StepResolution == value)
                    return;
                _StepResolution = value;
                RaisePropertyChanged("StepResolution");
            }
        }


        StationMode _Mode;

        public StationMode Mode
        {
            get
            { return _Mode; }
            set
            {
                if (_Mode == value)
                    return;
                _Mode = value;
                RaisePropertyChanged("Mode");
            }
        }

        string _StationName;

        public string StationName
        {
            get
            { return _StationName; }
            set
            {
                if (_StationName == value)
                    return;
                _StationName = value;
                RaisePropertyChanged("StationName");
            }
        }

        public StationViewModel(TrainControllerDeviceModel controller, TrainSensorModel stoppingSensor, TrainSensorModel haltSensor)
        {
            this._Controller = controller;
            this._SensorStartingHalt = stoppingSensor;
            this._SensorHalt = haltSensor;

            ViewModelHelper.BindNotifyChanged(controller, this, ControllerNotifyChanged);
            ViewModelHelper.BindNotifyChanged(stoppingSensor, this, StoppingSensorNotifyChanged);
            ViewModelHelper.BindNotifyChanged(haltSensor, this, HaltSensorNotifyChanged);

            this.Mode = StationMode.Idle;

        }

        #region NotifiyChangedHandler

        private void StoppingSensorNotifyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsDetected")
            {
                var sens = sender as TrainSensorModel;
                if (sens != null && sens.IsDetected)
                    StoppingStation();
            }
        }

        private void HaltSensorNotifyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsDetected")
            {
                var sens = sender as TrainSensorModel;
                if (sens != null && sens.IsDetected
                    && this.Mode == StationMode.Stopping)
                {
                    HaltStation();
                }
            }
        }

        private void ControllerNotifyChanged(object sender, PropertyChangedEventArgs e)
        {

        }
        #endregion

        #region Trasition

        private void StoppingStation()
        {
            if (this.Mode != StationMode.Idle || this.Controller == null || !this.Controller.IsStateReady)
                return;

            if (!this.IsHalt)
                return;

            this.Mode = StationMode.Stopping;

            this.Controller.Mode = TrainControllerMode.Following;

            var volt = this.Controller.Voltage;
            var remain = this.DutyWhenHalt;

            Observable.Interval(TimeSpan.FromMilliseconds(100))
                    .SubscribeOn(Scheduler.ThreadPool)
                    .TakeWhile(i => i <= this.StepResolution)
                    .Subscribe(i =>
                      {
                          if (this.Mode != StationMode.Stopping)
                              return;

                          if (this.Controller.Voltage > 0.1)
                          {
                              this.Controller.Mode = TrainControllerMode.Following;
                              this.Controller.Voltage = (double)i * (volt - 0.1) / ((double)this.StepResolution) + 0.1;
                          }
                          else
                          {
                              this.Controller.Mode = TrainControllerMode.Duty;
                              this.Controller.DutyValue = this.DutyWhenHalt + 20.0;
                          }

                          if (this.ImmediateStopFlag)
                          {
                              this.Controller.Mode = TrainControllerMode.Duty;
                              this.Controller.DutyValue = remain;

                              HaltStation();
                          }

                          if (i == this.StepResolution)
                              HaltStation();
                      });
        }

        private void LeaveStation()
        {
            if (this.Mode != StationMode.Halt)
                return;

            this.Mode = StationMode.Leaving;

            if (this.Controller == null)
                return;

            this.Controller.Mode = TrainControllerMode.Following;
            this.Controller.Voltage = this.LeavingVoltage;

            this.Mode = StationMode.Idle;


        }

        private void HaltStation()
        {
            if (this.Mode != StationMode.Stopping)
                return;

            this.Mode = StationMode.Halt;

            if (this.Controller == null)
                return;

            this.Controller.Mode = TrainControllerMode.Duty;
            this.Controller.DutyValue = this.DutyWhenHalt;

            Observable.Range(0, 1)
                    .Delay(this.StoppingTime, Scheduler.ThreadPool)
                    .Subscribe(t => { while (this.ImmediateStopFlag); LeaveStation(); });
        }

        #endregion
    }

    public enum StationMode
    {
        Idle,
        Stopping,
        Halt,
        Leaving,
    }
}
