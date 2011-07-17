using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensorLibrary
{
    public class TrainController
        : Device<TrainControllerState>
    {
        public TrainController(DeviceID id, IObservable<IDeviceState<IPacketDeviceData>> obsv)
            : base(id, ModuleTypeEnum.TrainController, obsv)
        {
        }

        public TrainController(DeviceID id)
            : this(id, null)
        { }
        
    }
}
