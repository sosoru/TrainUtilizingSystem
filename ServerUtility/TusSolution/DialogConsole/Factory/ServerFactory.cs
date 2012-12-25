using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("TestProject.ConsoleTest")]

namespace DialogConsole.Factory
{
    [Export]
    class ServerFactory
    {
        
    }
}
