using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

using Tus.Communication;

namespace Tus.Communication.Device.Composition
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class DeviceFactoryMetadataAttribute
        : Attribute, IDeviceFactoryMetadataAttribute
    {
        public ModuleTypeEnum ModuleType { get; set; }
        public DeviceFactoryMetadataAttribute(ModuleTypeEnum typeenum)
        {
            this.ModuleType = typeenum; 
        }

        
    }
}
