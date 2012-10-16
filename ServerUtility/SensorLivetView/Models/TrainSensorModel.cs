using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Livet;
using SensorLibrary;
using SensorLibrary.Devices.PicUsbDevices;
using SensorLibrary.Packet.Data;

namespace SensorLivetView.Models.Devices
{
    public class TrainSensorModel
        : DeviceModel<TrainSensor>
    {
        /*
         * NotifyObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */

        /*
         * リッチクライアントはステートフルであるため、通常のイベントの使用はメモリリークの原因になりやすくなっています。
         * Modelからイベントを発行する場合はNotificatorを使用してください。
         *
         * Notificatorはイベント代替手段です。コードスニペット lnev でCLRイベントと同時に定義できます。
         *
         * Model同士でNotificatorを使用した通知を行う場合はNotificatorHelper、
         * ViewModelへNotificatorを使用した通知を行う場合はViewModelHelperを使用して受信側の登録をしてください。
         */

        public TrainSensorModel()
        {
            this.PacketReceivedProcess = (sender, e) =>
                {
                    var before = e.beforestate as TrainSensorState;
                    var current = e.state as TrainSensorState;

                    if (before == null && current == null)
                        return;
                    else if (before == null || current == null)
                    {
                        RaisePropertyChanged("");
                        return;
                    }

                    if (before.IsDetected != current.IsDetected)
                    {
                        RaisePropertyChanged(() => IsDetected);
                        RaisePropertyChanged(() => Speed);
                    }

                    if (before.Mode != current.Mode)
                    {
                        RaisePropertyChanged(() => Mode);
                        RaisePropertyChanged(() => IsDetectingMode);
                        RaisePropertyChanged(() => IsMeisuringMode);
                    }

                    if (before.ThresholdVoltageLower != current.ThresholdVoltageLower)
                    {
                        RaisePropertyChanged(() => ThresholdVoltageLower);
                    }

                    if (before.ThresholdVoltageHigher != current.ThresholdVoltageHigher)
                    {
                        RaisePropertyChanged(() => ThresholdVoltageHigher);
                    }

                };

        }


        double _ReflactorInterval;

        public double ReflactorInterval
        {
            get
            { return _ReflactorInterval; }
            set
            {
                if (_ReflactorInterval == value)
                    return;
                _ReflactorInterval = value;

                RaisePropertyChanged("ReflactorInterval");
                RaisePropertyChanged(() => Speed);
            }
        }

        public double Speed
        {
            get
            {
                return this.TargetDevice.CalculateSpeed(this.ReflactorInterval);
            }
        }


        public bool IsMeisuringMode
        {
            get { return this.TargetDevice.CurrentState.Mode == TrainSensorMode.meisuring; }
            set
            {
                if (value)
                {
                    ModifyState(() => this.TargetDevice.CurrentState.Mode = TrainSensorMode.meisuring);
                }
            }
        }

        public bool IsDetectingMode
        {
            get { return this.TargetDevice.CurrentState.Mode == TrainSensorMode.detecting; }
            set
            {
                if (value)
                {
                    ModifyState(() => this.TargetDevice.CurrentState.Mode = TrainSensorMode.detecting);
                }
            }
        }

        public bool IsDetected
        {
            get
            {
                if (this.TargetDevice == null)
                    return false;
                else
                    return this.TargetDevice.CurrentState.IsDetected;
            }
        }

        public TrainSensorMode Mode
        {
            get { return this.TargetDevice.CurrentState.Mode; }
            set { ModifyState(() => this.TargetDevice.CurrentState.Mode = value); }
        }

        public float ThresholdVoltageLower
        {
            get { return this.TargetDevice.CurrentState.ThresholdVoltageLower; }
            set { ModifyState(() => this.TargetDevice.CurrentState.ThresholdVoltageLower = value); }
        }

        public float ThresholdVoltageHigher
        {
            get { return this.TargetDevice.CurrentState.ThresholdVoltageHigher; }
            set { ModifyState(() => this.TargetDevice.CurrentState.ThresholdVoltageHigher = value); }
        }
    }

    public class ValueChangedEventArgs<T>
        : EventArgs
    {
        public T before;
        public T current;
    }
}
