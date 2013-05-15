using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Tus.Communication.Device.AvrComposed;
using Tus.TransControl.Base;

namespace DialogConsole.WebPages
{
    [Export(typeof(IConsolePage))]
    [TusPageMetadata("vehicle control", "vehicle")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class VehiclePage : ConsolePageBase
    {
        public override string GetJsonContent()
        {
            var vehicles = this.Param.UsingLayout.Vehicles;
            return GetJsonContent<IEnumerable<Vehicle>>(vehicles);
        }

        public override void ApplyJsonRequest()
        {
        }
    }
}