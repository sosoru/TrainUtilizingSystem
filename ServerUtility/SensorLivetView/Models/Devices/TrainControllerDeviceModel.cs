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
    public class TrainControllerDeviceModel : DeviceModel<TrainController>
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

        public TrainControllerDeviceModel()
            : base()
        {
            this.PacketReceivedProcess = (sender, e) =>
                {
                    var bef  = e.beforestate as TrainControllerState;
                    var cur = e.state as TrainControllerState;

                    if (bef == null && cur == null)
                        return;
                    else if (bef == null || cur == null)
                    {
                        RaisePropertyChanged("");
                        return;
                    }

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
                        RaisePropertyChanged(() => Frequency);

                    }

                    if (bef.Voltage != cur.Voltage)
                    {
                        RaisePropertyChanged(() => Voltage);
                    }

                    if (bef.MeisuredVoltage != cur.MeisuredVoltage)
                    {
                        RaisePropertyChanged(() => MeisuredVoltage);
                        RaisePropertyChanged(() => ActualVoltage);

                    }

                    if (bef.MeisuredVoltage2 != cur.MeisuredVoltage2)
                    {
                        RaisePropertyChanged(() => MeisuredVoltage2);
                        RaisePropertyChanged(() => ActualVoltage);

                    }

                    if (bef.ControllerMode != cur.ControllerMode)
                    {
                        RaisePropertyChanged(() => Mode);
                    }

                    if (bef.LowerFreqEnable != cur.LowerFreqEnable)
                    {
                        RaisePropertyChanged(() => IsLowerFreqEnabled);
                        RaisePropertyChanged(() => Frequency);

                    }

                    if (bef.LowerFreq != cur.LowerFreq)
                    {
                        RaisePropertyChanged(() => LowerFreq);
                        RaisePropertyChanged(() => Frequency);

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
                var d= (double)this.TargetDevice.CurrentState.Duty;
                var pr = (double)this.TargetDevice.CurrentState.DeviceRegisteredPeriod;

                d = d * (value / pr);

                ModifyState(() =>
                {
                    this.TargetDevice.CurrentState.DeviceRegisteredPeriod = (byte)value;
                    this.TargetDevice.CurrentState.Duty = (int)d;
                });
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
            set { this.TargetDevice.CurrentState.PWMFreqency = value; }
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

        public bool IsLowerFreqEnabled
        {
            get { return this.TargetDevice.CurrentState.LowerFreqEnable; }
            set { ModifyState(() => this.TargetDevice.CurrentState.LowerFreqEnable = value); }
        }

        public int LowerFreq
        {
            get { return this.TargetDevice.CurrentState.LowerFreq; }
            set { ModifyState(() => this.TargetDevice.CurrentState.LowerFreq = value); }
        }

        public double Frequency
        {
            get
            {
                if (this.TargetDevice.CurrentState.LowerFreqEnable)
                {
                    return (double)this.TargetDevice.CurrentState.LowerFreq;
                }
                else
                {
                    return this.TargetDevice.CurrentState.PWMFreqency;
                }

            }
            set
            {
                if (value < 0.0)
                    throw new ArgumentException("cannot use negative value");

                if (value < 3000.0)
                {
                    ModifyState(() =>
                    {
                        this.TargetDevice.CurrentState.LowerFreqEnable = true;
                        this.TargetDevice.CurrentState.LowerFreq = (int)value;
                    });
                }
                else
                {
                    ModifyState(() =>
                    {
                        this.TargetDevice.CurrentState.LowerFreqEnable = false;
                        this.TargetDevice.CurrentState.PWMFreqency = value;
                    });
                }
            }
        }

        public double ActualVoltage
        {
            get { return Math.Abs(this.MeisuredVoltage - this.MeisuredVoltage2); }
        }


    }
}
