using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Tus.Communication.Device.AvrComposed;
using Tus.TransControl.Base;

namespace DialogConsole.WebPages
{
    [Export(typeof(IConsolePage))]
    [TusPageMetadata("motor device control", "motor")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MotorPage : ConsolePageBase
    {
        public override string GetJsonContent()
        {
            var motors =
                this.Param.UsingLayout.Sheet.AllDevices.Where(d => d.ModuleType == Tus.Communication.ModuleTypeEnum.AvrMotor)
                    .Cast<Motor>().ToArray();
            return this.GetJsonContent<IEnumerable<Motor>>(motors, new[] { typeof(MemoryState) });
        }

        public override void ApplyJsonRequest()
        {
        }
    }
}