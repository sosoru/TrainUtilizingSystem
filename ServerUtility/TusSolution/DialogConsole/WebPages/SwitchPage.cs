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
    public class SwitchPage : ConsolePageBase<IEnumerable<Switch>>
    {
        private IEnumerable<Switch> Switches
        {
            get
            {
                var switches =
                    this.Param.UsingLayout.Sheet.AllDevices.Where(
                        d => d.ModuleType == Tus.Communication.ModuleTypeEnum.AvrSwitch)
                        .Cast<Switch>();
                return switches;
            }
        }

        public override IEnumerable<Switch> GetContent()
        {
            return Switches;
        }

        public override void ApplyJsonRequest(IEnumerable<Switch> obj)
        {
            foreach (var sentsw in obj)
            {
                var sw = this.Switches.FirstOrDefault(dev => dev.DeviceID == sentsw.DeviceID);
                if (sw != null)
                {
                    
                    sw.CurrentState.Position = sentsw.CurrentState.Position;
                    Console.WriteLine("{0}(switch): Position is changed to {1}", sw.DeviceIDString, sw.CurrentState.PositionString);
                    sw.SendState();
                }
            }
        }
    }
}