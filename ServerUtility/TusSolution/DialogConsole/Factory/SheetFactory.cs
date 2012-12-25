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
        : IPartImportsSatisfiedNotification
    {
        public string Path { get; set; }

        [Import]
        public ServerFactory ServerCreater { get; set; }

        public void OnImportsSatisfied()
        {
            this.Path = DialogConsole.Properties.Settings.Default.SheetPath;
        }

        public BlockSheet Create()
        {
            var parser = new BlockYaml();
            var blocks = parser.Parse(this.Path);

            return new BlockSheet(blocks, this.ServerCreater.Create());
        }
    }

}
