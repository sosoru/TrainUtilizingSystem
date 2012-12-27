using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

using RouteLibrary;
using RouteLibrary.Base;
using RouteLibrary.Parser;

namespace DialogConsole.Factory
{
    [Export]
    class SheetFactory
    {
        [Import]
        public IConsoleApplicationSettings Settings { get; set; }

        [Import]
        public ServerFactory ServerCreater { get; set; }

        public BlockSheet Create()
        {
            var parser = new BlockYaml();
            var blocks = parser.Parse(this.Settings.SheetPath);

            return new BlockSheet(blocks, this.ServerCreater.Create());
        }
    }

}
