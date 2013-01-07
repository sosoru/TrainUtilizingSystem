using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

using DialogConsole;

namespace DialogConsole.Features.Base
{
    [InheritedExport]
    public class BaseFeature
    {
        [Import]
        public IFeatureParameters Param { get; set; }
    }
}
