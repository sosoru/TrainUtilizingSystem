using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Livet;
using SensorLibrary;

namespace SensorLivetView.Models.Devices
{
    internal class TrainSensorModel
        :DeviceModel<TrainSensor>
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

        public TrainSensorModel(TrainSensor dev)
        {
            this.TargetDevice = dev;

            this.TargetDevice.PacketReceived += (sender, e) =>
                {
                    var before = e.beforestate as TrainSensorState;
                    var current = e.state as TrainSensorState;

                    if (before.IsDetected != current.IsDetected)
                    {
                        RaisePropertyChanged(() => IsDetected);
                    }

                    if (before.Mode != current.Mode)
                    {
                        RaisePropertyChanged(() => IsDetectingMode);
                        RaisePropertyChanged(() => IsMeisuringMode);
                    }

                    var speed = this.TargetDevice.CalculateSpeed(this.ReflactorInterval);
                    if (speed != double.NaN)
                        this.Speed = speed;

                    
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
            }
        }


        double _Speed;

        public double Speed
        {
            get
            { return _Speed; }
            private set
            {
                if (_Speed == value)
                    return;
                RaisePropertyChanged("Speed");

                _Speed = value;
            }
        }


        public bool IsMeisuringMode
        {
            get { return this.TargetDevice.CurrentState.Mode == TrainSensorMode.meisuring; }
            set
            {
                if (value)
                {
                    this.TargetDevice.CurrentState.Mode = TrainSensorMode.meisuring;
                    RaisePropertyChanged(() => IsMeisuringMode);
                    RaisePropertyChanged(() => IsDetectingMode);
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
                    this.TargetDevice.CurrentState.Mode = TrainSensorMode.detecting;
                    RaisePropertyChanged(() => IsMeisuringMode);
                    RaisePropertyChanged(() => IsDetectingMode);
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
    }

    public class ValueChangedEventArgs<T>
        : EventArgs
    {
        public T before;
        public T current;
    }
}
