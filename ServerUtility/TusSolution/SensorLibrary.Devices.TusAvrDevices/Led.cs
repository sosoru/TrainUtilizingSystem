using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Tus.Communication;

namespace SensorLibrary.Devices.TusAvrDevices
{
    [DataContract]
    public class Led
        :Device<LedState>
    {
        public Led()
            : base(ModuleTypeEnum.AvrLed, new LedState())
        {
            
        }
    }
}
