using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tus.Route;

namespace DialogConsole
{
    public struct FeatureParameters
    {
        public BlockSheet Sheet { get; set; }
        public IList<Vehicle> Vehicles { get; set; }
    }
}
