using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Tus.Communication.Device.AvrComposed;

namespace DialogConsole.WebPages
{
    [Export(typeof(IConsolePage))]
    [TusPageMetadata("switch device control", "switch")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SwitchPage : ConsolePageBase
    {
        public override string GetJsonContent()
        {
            var switches =
                this.Param.UsingLayout.Sheet.AllDevices.Where(d => d.ModuleType == Tus.Communication.ModuleTypeEnum.AvrSwitch)
                .Cast<Switch>().ToArray();

            return GetJsonContent<IEnumerable<Switch>>(switches);
        }


        public override void ApplyJsonRequest()
        {
        }
    }
}