using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tus.Communication
{
    public interface IDeviceCommand<TState>
        : IObserver<TState>
        where TState : IDeviceState<IPacketDeviceData>
    {
         string Name { get; }
         Func<IDeviceCommand<TState>, bool> IsExecutable { get; }
         Action<IDeviceCommand<TState>> Command { get; }
         IDevice<TState> OwnerDevice { get; }
    }
}
