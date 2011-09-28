using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using SensorLibrary;
using SensorViewLibrary;

namespace SensorLivetView.ViewModels
{
    public class DeviceViewModelDispatcher<TDevice, TState, TVM>
       where TDevice : class, IDevice<TState>
          where TState : class, IDeviceState<IPacketDeviceData>
       where TVM : DeviceViewModel<TDevice>
    {
        public PacketDispatcherSingle<TDevice, TState> dispat { get; private set; }

        public ObservableWrappingCollection<TDevice, TVM> projected { get; private set;}

        public DeviceViewModelDispatcher()
        {
            this.dispat = new PacketDispatcherSingle<TDevice, TState>();
            this.projected = new ObservableWrappingCollection<TDevice, TVM>();
            this.projected.Context = this.dispat.FoundDeviceList;

            this.projected.Projection = (src) =>
                {
                    var vm = typeof(TVM).GetConstructor(new Type[] { }).Invoke(null) as TVM;
                    vm.Model = src;
                    return vm;
                };
            this.projected.InverseProjection = (proj) =>
                {
                    return proj.Model;
                };
        }
        

    }
}
