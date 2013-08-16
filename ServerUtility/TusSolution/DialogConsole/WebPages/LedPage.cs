using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SensorLibrary.Devices.TusAvrDevices;

namespace DialogConsole.WebPages
{
    [Export(typeof(IConsolePage))]
    [TusPageMetadata("led device control", "led")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LedPage : ConsolePageBase<IEnumerable<Led>>
    {
        private IEnumerable<Led> leds
        {
            get { return this.Param.UsingLayout.Illumination.Objects.SelectMany(o => o.AssociatedLuminary.Leds); }
        }

        public override IEnumerable<Led> GetContent()
        {
            return this.leds;
        }

        public override void ApplyJsonRequest(IEnumerable<Led> obj)
        {
            foreach (var led in obj)
            {
                var found = this.leds.FirstOrDefault(dev => dev.DeviceID == led.DeviceID);
                if (found != null)
                {
                    found.CurrentState.Duty = led.CurrentState.Duty;
                    found.SendState();
                }
            }
        }
    }
}
