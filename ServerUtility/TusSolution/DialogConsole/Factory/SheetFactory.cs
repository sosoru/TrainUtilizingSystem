using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("TestProject.ConsoleTest")]

namespace DialogConsole.Factory
{
    class SheetFactory
    {
        public string Path { get; set; }
        public ServerFactory ServerCreater { get; set; }
    }

}
