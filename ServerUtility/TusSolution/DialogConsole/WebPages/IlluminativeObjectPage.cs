using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tus.Illumination;

namespace DialogConsole.WebPages
{
    [Export(typeof(IConsolePage))]
    [TusPageMetadata("illuminative object control", "illuminative")]
    public class IlluminativeObjectPage
        : ConsolePageBase<IEnumerable<IlluminativeObject>, IEnumerable<IlluminativeObject>>
    {
        private IEnumerable<IlluminativeObject> illuminatives
        {
            get
            {
                return this.Param.UsingLayout.Illumination.Objects;
            }
        }

        public override IEnumerable<IlluminativeObject> CreateSendingContent()
        {
            return this.illuminatives;
        }

        public override void ApplyReceivedJsonRequest()
        {
            IEnumerable<IlluminativeObject> obj;
            if (!this.ReceivedContents.TryDequeue(out obj)) return;
            foreach (var ill in obj)
            {
                var found = this.illuminatives.FirstOrDefault(lumi => lumi.Name == ill.Name);
                if (found != null)
                {
                    found.Luminance = ill.Luminance;
                    Console.WriteLine("{0}(Led): Luminance value is changed to {1}", found.Name, found.Luminance);

                    found.SwitchingBlocks = ill.SwitchingBlocks;
                    if (!string.IsNullOrEmpty(found.SwitchingBlocks))
                    {
                        RefreshSwitching(found);
                    }
                    else
                    {
                    }
                }
            }
        }

        public void RefreshSwitching(IlluminativeObject illobj)
        {
            //var blocks = illobj.SwitchingBlocks.Split(',').Select(s => s.Trim());
            //var approaching =
            //    this.Param.UsingLayout.Vehicles.Any(
            //        v => v.AssociatedRoute.LockedUnits.Any(u => blocks.Contains(u.ControlBlock.Name)));
            illobj.HalfLighting = false;
            //if (approaching) 
            //{
            //    illobj.HalfLighting = false;
            //}
            //else
            //{
            //    illobj.HalfLighting = true;
            //    illobj.Luminance /= 2.0f;
            //}
        }
    }
}
