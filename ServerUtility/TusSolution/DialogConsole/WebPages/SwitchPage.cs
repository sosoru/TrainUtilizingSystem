using System;
using System.Collections;
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
    public interface IConsolePage
    {
        string Query { get; set; }
        IFeatureParameters Param { get; set; }
        void SetResponseParameter(string query);
        string GetJsonContext();
    }

    [Export(typeof(IConsolePage))]
    [TusPageMetadata("switch control", "/switch")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ConsolePage : IConsolePage
    {
        [Import]
        public IFeatureParameters Param { get; set; }
        public string Query { get; set; }

        public void SetResponseParameter(string query)
        {
            this.Query = query;
        }

        public string GetJsonContext()
        {
            var switches = this.Param.Sheet.InnerBlocks.Where(b => b.HasSwitch)
                               .Select(b => b.SwitchEffector.Device);

            var ser = new DataContractJsonSerializer(typeof (IEnumerable<Switch>));

            using (var ms = new MemoryStream())
            using (var sr = new StreamReader(ms, Encoding.UTF8))
            {
                ser.WriteObject(ms, switches);
                ms.Seek(0, SeekOrigin.Begin);
                return sr.ReadToEnd();
            }
        }

        public void ApplyJsonRequest()
        {

        }

    }
}
