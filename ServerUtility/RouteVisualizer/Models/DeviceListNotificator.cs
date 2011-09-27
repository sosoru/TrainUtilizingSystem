using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

using Livet;
using SensorLibrary;

namespace RouteVisualizer.Models
{
    public class DeviceListNotificator <TDevice>
        : Notificator<NotifyCollectionChangedEventArgs>
        where TDevice : IDevice<IDeviceState<IPacketDeviceData>>
    {
        public DeviceListNotificator()
        {
        }

        public 

    }

}
