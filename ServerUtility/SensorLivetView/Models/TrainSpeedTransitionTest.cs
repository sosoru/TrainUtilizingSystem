using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Livet;
using SensorLibrary;
using System.Reactive.Linq;
using System.Reactive;
using System.Reactive.Concurrency;


namespace SensorLivetView.Models
{
    public class TrainSpeedTransitionTest
        : TestTaskModel
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

        public TrainSensor targetSensor { get; set; }
        public TrainController targetController { get; set; }


        RangedValue _PeriodRange;

        public RangedValue PeriodRange
        {
            get
            { return _PeriodRange; }
            set
            {
                if (_PeriodRange == value)
                    return;
                _PeriodRange = value;
                RaisePropertyChanged("PeriodRange");
            }
        }


        RangedValue _SpeedRange;

        public RangedValue SpeedRange
        {
            get
            { return _SpeedRange; }
            set
            {
                if (_SpeedRange == value)
                    return;
                _SpeedRange = value;
                RaisePropertyChanged("SpeedRange");
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

        public string FilePath { get; set; }

        public TrainSpeedTransitionTest()
            : base()
        {
            this.PeriodRange = new RangedValue(0, 255, 1);
            this.SpeedRange = new RangedValue(0, 5, 0.1);
            this.ReflectorInterval = 5;

            this.FilePath = "speedtest_" + DateTime.Now.ToString() + ".csv";
        }

        protected virtual void serialize(TrainControllerState ctrlstate, TrainSensorState sensorstate, double speed)
        {
            using (var sw = new StreamWriter(FilePath, true))
            {
                var sb = new StringBuilder();
                foreach (var item in new[] { ctrlstate.DeviceFrequency, ctrlstate.Voltage, speed })
                {
                    sb.Append(item);
                    sb.Append(",");
                }
                sb.Remove(sb.Length - 1, 1);

                sw.WriteLine(sb.ToString());
            }
        }

        protected override void internalStart()
        {
            this.IsTesting = true;
            try
            {
                foreach (var period in this.PeriodRange.ToEnumerable())
                {
                    foreach (var speed in this.SpeedRange.ToEnumerable())
                    {
                        var ctrlstate = this.targetController.CurrentState;
                        ctrlstate.ControllerMode = TrainControllerMode.Following;
                        ctrlstate.Voltage = (int)speed;
                        ctrlstate.DeviceRegisteredPeriod = (byte)period;
                        this.targetController.SendPacket(ctrlstate);

                        System.Threading.Thread.Sleep(1000);

                        var tsstate = this.targetSensor.CurrentState;
                        tsstate.Mode = TrainSensorMode.detecting;
                        this.targetSensor.SendPacket(tsstate);

                        System.Threading.Thread.Sleep(1000);

                        try
                        {
                            var resobsv = Observable.SubscribeOn(this.targetSensor.GetSpeedChangedObservable(), Scheduler.NewThread)
                                                    .Timeout(new DateTimeOffset(DateTime.Now, new TimeSpan(0, 1, 0)))
                                                    .Select((f) => f.CalculateSpeed(this.ReflectorInterval))
                                                    .Do((val) => this.serialize(ctrlstate, tsstate, val));
                        }
                        catch (TimeoutException)
                        {
                            this.serialize(ctrlstate, tsstate, 0.0);
                        }


                    }
                }
            }
            finally { this.IsTesting = false; }
        }

    }
}
