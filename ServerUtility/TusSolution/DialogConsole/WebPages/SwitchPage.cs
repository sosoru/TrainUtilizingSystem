using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using DialogConsole.Features.Base;
using Tus.Communication.Device.AvrComposed;

namespace DialogConsole.WebPages
{
    [Export]
    [TusPageMetadata("switch control", "/switch")]
    public class SwitchPage
    {
        public string Query { get; set; }
        public IFeatureParameters Param { get; set; }

        public string GetJsonContext()
        {
            var ser = new DataContractJsonSerializer(typeof(IEnumerable<Switch>));
            var switches = this.Param.Sheet.InnerBlocks.Where(b => b.HasSwitch)
                               .Select(b => b.SwitchEffector.Device);

            using (var ms = new MemoryStream())
            using (var sr = new StreamReader(ms, Encoding.UTF8))
            {
                ser.WriteObject(ms, switches);
                ms.Seek(0, SeekOrigin.Begin);
                return sr.ReadToEnd();
            }
        }

    }
}
