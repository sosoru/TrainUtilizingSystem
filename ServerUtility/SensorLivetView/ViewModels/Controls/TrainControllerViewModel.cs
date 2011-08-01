using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary;
using System.Windows.Media;

namespace SensorLivetView.ViewModels.Controls
{
    public class TrainControllerViewModel
        : DeviceViewModel<TrainController>
    {
        public TrainControllerViewModel(TrainController ctrl)
            : base(ctrl)
        { }

        public TrainControllerViewModel()
            : this(null)
        { }

        protected override void ReceivedProcess(IDevice<IDeviceState<IPacketDeviceData>> dev, PacketReceiveEventArgs e)
        {
            base.ReceivedProcess(dev, e);
        }

        public double DutyValue
        {
            get { return Math.Round((double)this.Model.CurrentState.Duty,0); }
            set
            {
                this.Model.CurrentState.Duty = (int)value;
                RaisePropertyChanged("DutyValue");
            }
        }

        public double RegisteredPeriodValue
        {
            get { return Math.Floor((double)this.Model.CurrentState.DeviceRegisteredPeriod); }
            set
            {
                this.Model.CurrentState.DeviceRegisteredPeriod = (byte)value;
                RaisePropertyChanged("RegisteredPeriodValue");
            }
        }

        public DoubleCollection PrescaleSliderTicks
        {
            get { return new DoubleCollection(new[] { 1.0, 4.0, 16.0 }); }
        }

        public double PrescaleValue
        {
            get { return this.Model.CurrentState.PreScale; }
            set
            {
                this.Model.CurrentState.PreScale = (int)value;
                RaisePropertyChanged("PrescaleValue");
            }
        }

        public bool DirectionValue
        {
            get
            {
                return this.Model.CurrentState.Direction == TrainControllerDirection.Positive;
            }
            set
            {
                this.Model.CurrentState.Direction = (value) ? TrainControllerDirection.Positive : TrainControllerDirection.Negative;
            }
        }

        public float ParamP
        {
            get { return this.Model.CurrentState.PidParams.paramp; }
            set
            {
                var st = this.Model.CurrentState.PidParams;
                st.paramp = getLimitedValue(value, -1.0f, 1.0f);
                this.Model.CurrentState.PidParams = st;
            }
        }

        public float ParamI
        {
            get { return this.Model.CurrentState.PidParams.parami; }
            set
            {
                var st = this.Model.CurrentState.PidParams;
                st.parami = getLimitedValue(value, -1.0f, 1.0f);
                this.Model.CurrentState.PidParams = st;
            }
        }

        public float ParamD
        {
            get { return this.Model.CurrentState.PidParams.paramd; }
            set
            {
                var st = this.Model.CurrentState.PidParams;
                st.paramd = getLimitedValue(value, -1.0f, 1.0f);
                this.Model.CurrentState.PidParams = st;
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

    }
}
