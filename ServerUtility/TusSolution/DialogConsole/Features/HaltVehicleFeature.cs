using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using DialogConsole.Features.Base;

namespace DialogConsole.Features
{
    [FeatureMetadata("h", "Halt or Leave vehicle")]
    [Export(typeof(IFeature))]
    class HaltVehicleFeature
        : BaseFeature, IFeature
    {
        public void Execute()
        {
            Console.WriteLine("type name? ");
            var name = Console.ReadLine();

            var v = this.Param.UsingLayout.Vehicles.FirstOrDefault(b => b.Name == name);

            if (v == null)
            {
                Console.WriteLine("not found");
                return;
            }

            if (v.IsHalted)
            {
                if (v.CanLeaveHere)
                {
                    v.LeaveHere();
                    Console.WriteLine("{0} leaves here", v.Name);
                }
                else
                {
                    Console.WriteLine("{0} cannot leave here", v.Name);
                }
            }
            else
            {
                if (v.CanHaltHere)
                {
                    v.HaltHere();
                    Console.WriteLine("{0} halts here", v.Name);
                }
                else
                {
                    Console.WriteLine("{0} cannot halt here", v.Name);
                }
            }

        }

        public void Init()
        {
        }
    }
}
