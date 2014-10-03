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
            var blocks = this.Param.UsingLayout.Sheet.InnerBlocks;

            this.Param.UsingLayout.Sheet.InquiryDevices();

            foreach (var b in blocks)
                Console.WriteLine(b.ToString());

            Console.WriteLine();
            //foreach(var rt in this.Param.Routes)
            //    Console.WriteLine(rt.ToString());

            //write pending queue
            Console.WriteLine("server pending queue: {0}", this.Param.UsingLayout.Sheet.Server.QueuingCount);
        }

        public void Init()
        {
        }
    }
}
