using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SensorLibrary.Devices.TusAvrDevices;
using Tus.Communication;

namespace Tus.Illumination
{
    public class Luminary
    {
        public Luminary()
        {
        }

        public IList<Led> Leds { get; set; }
    }
}
