using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using SensorLibrary;

namespace SensorViewModels
{
    public class DeviceViewModel<T>
        : ViewModelBase<T>
    where T : IDevice<IDeviceState<IPacketDeviceData>>
    {
        public DeviceViewModel(T device)
            : base(device)
        {
            this.Model.PacketReceived += new PacketReceivedDelegate<IDeviceState<IPacketDeviceData>>((dev, e) => OnPropertyChanged("CurrentState"));
        }

        public IDeviceState<IPacketDeviceData> CurrentState
        {
            get { return this.Model.CurrentState ; }
        }

    }
}
