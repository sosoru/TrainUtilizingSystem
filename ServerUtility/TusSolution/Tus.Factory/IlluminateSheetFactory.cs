using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tus.Illumination;
using Tus.TransControl.Base;

namespace Tus.Factory
{
    [Export]
    public class IlluminativeSheetFactory : FactoryBase<IlluminativeSheet>
    {
        [Import]
        public ServerFactory ServerCreater { get; set; }

        public override IlluminativeSheet Create()
        {
            var server = this.ServerCreater.Create();
            var factory = new IlluminativeObjectFactory(this.ApplicationSettings.IlluminationPath, server);
            var sheet = new IlluminativeSheet(factory.Create(), server);

            return sheet;
        }
    }
}
