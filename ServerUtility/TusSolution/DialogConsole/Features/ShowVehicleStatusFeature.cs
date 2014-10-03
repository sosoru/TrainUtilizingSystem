using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DialogConsole.Features.Base;
using Tus.TransControl.Base;

namespace DialogConsole.Features
{
    [FeatureMetadata("v", "show current vehicles")]
    [Export(typeof(IFeature))]
    class ShowVehicleStatusFeature
        : BaseFeature, IFeature
    {
        public void Init()
        {
        }

        private string wrapbystate(Route rt, ControlUnit u)
        {
            var wrp = (rt.LockedUnits.Contains(u))
                          ? "*"
                          : (u.IsBlocked)
                                ? "|"
                                : "";
            return wrp + u.ToString() + wrp;
        }

        public void Execute()
        {
            var vehicles = this.Param.UsingLayout.Vehicles;

            foreach (var v in vehicles)
            {
                var rt = v.AssociatedRoute;
                var rtstr = rt.RouteOrder.Units.Aggregate("", (s, u) => s + wrapbystate(rt, u) + ", ").TrimEnd(',', ' ');
                Console.WriteLine("{0} on {1}: {2}", v.Name, rt.RouteOrder.Name, rtstr);
            }
        }
    }
}
