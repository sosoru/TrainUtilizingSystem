using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

using Tus;
using Tus.TransControl.Base;
using Tus.TransControl.Parser;

namespace Tus.Factory
{
    [Export]
    public class SheetFactory : FactoryBase<BlockSheet>
    {
        [Import]
        public ServerFactory ServerCreater { get; set; }

        public override BlockSheet Create()
        {
            var parser = new BlockYaml();
            var blocks = parser.Parse(this.ApplicationSettings.SheetPath);

            return new BlockSheet(blocks, this.ServerCreater.Create());
        }
    }

}
