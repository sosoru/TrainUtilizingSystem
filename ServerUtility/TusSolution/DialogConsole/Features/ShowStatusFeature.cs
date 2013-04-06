using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using DialogConsole.Features.Base;

namespace DialogConsole.Features
{
    [FeatureMetadata("", "show statuses")]
    [Export(typeof(IFeature))]
    class ShowStatusFeature
        : BaseFeature, IFeature
    {
        public void Execute()
        {
            var blocks = this.Param.Sheet.InnerBlocks;

            this.Param.Sheet.InquiryAllMotors();
            this.Param.Sheet.InquiryDevices(this.Param.Sheet.AllSwitches());

            foreach (var b in blocks)
                Console.WriteLine(b.ToString());
            return;
            //            return blocks.Select(b => b.ToString() + "\n")
            //                            .Aggregate("", (ag, s) => ag += s);
        }

        public void Init()
        {
        }
    }
}
