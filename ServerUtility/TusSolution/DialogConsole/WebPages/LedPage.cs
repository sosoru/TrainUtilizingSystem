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
    public class LedPage : ConsolePageBase<IEnumerable<Led>, IEnumerable<Led>>
    {
        private IEnumerable<Led> leds
        {
            get { return this.Param.UsingLayout.Illumination.Objects.SelectMany(o => o.AssociatedLuminary.Leds); }
        }

        public override IEnumerable<Led> CreateSendingContent()
        {
            return this.leds;
        }

        public override void ApplyReceivedJsonRequest()
        {
            IEnumerable<Led> obj;
            if (!this.ReceivedContents.TryDequeue(out obj)) return;
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
