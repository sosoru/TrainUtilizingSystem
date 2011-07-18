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

    }
}
