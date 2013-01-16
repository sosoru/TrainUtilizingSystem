using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace DialogConsole.Features.Base
{
    [Export(typeof(IFeature))]
    class Feature
        : BaseFeature, IFeature
    {
        public void Execute()
        {
        }

        public void Init()
        {
        }
    }
}
