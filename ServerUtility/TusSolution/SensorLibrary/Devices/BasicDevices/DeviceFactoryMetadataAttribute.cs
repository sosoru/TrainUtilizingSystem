using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

using SensorLibrary;

namespace SensorLibrary.Devices.BasicDevices
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class DeviceFactoryMetadataAttribute
        : Attribute, SensorLibrary.Devices.BasicDevices.IDeviceFactoryMetadataAttribute
    {
        public ModuleTypeEnum ModuleType { get; set; }
        public DeviceFactoryMetadataAttribute(ModuleTypeEnum typeenum)
        {
            this.ModuleType = typeenum; 
        }

        
    }
}
