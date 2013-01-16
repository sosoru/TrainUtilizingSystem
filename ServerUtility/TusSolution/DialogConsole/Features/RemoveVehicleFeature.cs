using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using DialogConsole.Features.Base;

namespace DialogConsole.Features
{
    [FeatureMetadata("6", "remove vehicle")]
    [Export(typeof(IFeature))]
    class RemoveVehicleFeature
        : BaseFeature, IFeature
    {
        public void Execute()
        {
            Console.WriteLine("type name? ");
            var name = Console.ReadLine();

            var v = this.Param.Vehicles.FirstOrDefault(b => b.Name == name);

            if (v == null)
            {
                Console.WriteLine("not found");
                return;
            }

            v.Length = 1;
            v.Run(0);
            v.Route.InitLockingPosition();

            this.Param.Vehicles.Remove(v);
        }

        public void Init()
        {
        }
    }
}
