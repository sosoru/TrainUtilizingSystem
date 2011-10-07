using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SensorLivetView.Models;
using SensorLivetView.Models.Devices;
using SensorLibrary;
using System.Collections.ObjectModel;
using System.Windows.Media;

using Livet;

namespace SensorLivetView.Models.Devices
{
    public class TrainControllerModel : DeviceModel<TrainController>
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

        public TrainControllerModel(TrainController controller)
            : base()
        {
            this.TargetDevice = controller;

            this.TargetDevice.PacketReceived += (sender, e) =>
                {
                    var bef  = e.beforestate as TrainControllerState;
                    var cur = e.state as TrainControllerState;

                    if (bef == null && cur == null)
                        return;
                    else if (bef == null || cur == null)
                        RaisePropertyChanged("");


                    if (bef.Duty != cur.Duty)
                    {
                        RaisePropertyChanged(() => DutyValue);
                    }

                    if (bef.DeviceRegisteredPeriod != cur.DeviceRegisteredPeriod)
                    {
                        RaisePropertyChanged(() => RegisteredPeriodValue);
                    }

                    if (bef.PreScale != cur.PreScale)
                    {
                        RaisePropertyChanged(() => PrescaleValue);
                    }

                    if (bef.Direction != cur.Direction)
                    {
                        RaisePropertyChanged(() => DirectionValue);
                    }

                    if (bef.PidParams.paramp != cur.PidParams.paramp)
                    {
                        RaisePropertyChanged(() => ParamP);
                    }

                    if (bef.PidParams.parami != cur.PidParams.parami)
                    {
                        RaisePropertyChanged(() => ParamI);
                    }

                    if (bef.PidParams.paramd != cur.PidParams.paramd)
                    {
                        RaisePropertyChanged(() => ParamD);
                    }

                    if (bef.EssentialDutyResolution != cur.EssentialDutyResolution)
                    {
                        RaisePropertyChanged(() => EssentialDutyResolution);
                    }

                    if (bef.PWMFreqency != cur.PWMFreqency)
                    {
                        RaisePropertyChanged(() => PWMFreqency);
                    }

                    if (bef.Voltage != cur.Voltage)
                    {
                        RaisePropertyChanged(() => Voltage);
                    }

                    if (bef.MeisuredVoltage != cur.MeisuredVoltage)
                    {
                        RaisePropertyChanged(() => MeisuredVoltage);
                    }

                    if (bef.MeisuredVoltage2 != cur.MeisuredVoltage2)
                    {
                        RaisePropertyChanged(() => MeisuredVoltage2);
                    }

                    if (bef.ControllerMode != cur.ControllerMode)
                    {
                        RaisePropertyChanged(() => Mode);
                    }
                };
        }

        public double DutyValue
        {
            get { return Math.Round((double)this.TargetDevice.CurrentState.Duty, 0); }
            set
            {
                ModifyState(() => this.TargetDevice.CurrentState.Duty = (int)value);
            }
        }

        public double RegisteredPeriodValue
        {
            get { return Math.Floor((double)this.TargetDevice.CurrentState.DeviceRegisteredPeriod); }
            set
            {
                ModifyState(() => this.TargetDevice.CurrentState.DeviceRegisteredPeriod = (byte)value);
            }
        }

        public double PrescaleValue
        {
            get { return this.TargetDevice.CurrentState.PreScale; }
            set
            {
                ModifyState(() => this.TargetDevice.CurrentState.PreScale = (int)value);
            }
        }

        public bool DirectionValue
        {
            get
            {
                return this.TargetDevice.CurrentState.Direction == TrainControllerDirection.Positive;
            }
            set
            {
                ModifyState(() => this.TargetDevice.CurrentState.Direction
                                         = (value) ? TrainControllerDirection.Positive : TrainControllerDirection.Negative);
            }
        }

        public float ParamP
        {
            get { return this.TargetDevice.CurrentState.PidParams.paramp; }
            set
            {
                var st = this.TargetDevice.CurrentState.PidParams;
                st.paramp = getLimitedValue(value, -1.0f, 1.0f);
                ModifyState(() => this.TargetDevice.CurrentState.PidParams = st);
            }
        }

        public float ParamI
        {
            get { return this.TargetDevice.CurrentState.PidParams.parami; }
            set
            {
                var st = this.TargetDevice.CurrentState.PidParams;
                st.parami = getLimitedValue(value, -1.0f, 1.0f);
                ModifyState(() => this.TargetDevice.CurrentState.PidParams = st);
            }
        }

        public float ParamD
        {
            get { return this.TargetDevice.CurrentState.PidParams.paramd; }
            set
            {
                var st = this.TargetDevice.CurrentState.PidParams;
                st.paramd = getLimitedValue(value, -1.0f, 1.0f);
                ModifyState(() => this.TargetDevice.CurrentState.PidParams = st);
            }
        }

        private float getLimitedValue(float val, float min, float max)
        {
            if (val < min)
                val = min;
            else if (val > max)
                val = max;

            return val;
        }

        public double EssentialDutyResolution
        {
            get { return this.TargetDevice.CurrentState.EssentialDutyResolution; }
        }

        public double PWMFreqency
        {
            get { return this.TargetDevice.CurrentState.PWMFreqency; }
        }

        public double Voltage
        {
            get { return this.TargetDevice.CurrentState.Voltage; }
            set { ModifyState(() => this.TargetDevice.CurrentState.Voltage = (int)value); }
        }

        public double MeisuredVoltage
        {
            get { return this.TargetDevice.CurrentState.MeisuredVoltage; }
        }

        public double MeisuredVoltage2
        {
            get { return this.TargetDevice.CurrentState.MeisuredVoltage2; }
        }

        public TrainControllerMode Mode
        {
            get { return this.TargetDevice.CurrentState.ControllerMode; }
            set
            {
                var mode = value;
                if (mode == TrainControllerMode.OnDevice)
                    return;

                ModifyState(() => this.TargetDevice.CurrentState.ControllerMode = mode);
            }
        }

    }
}
