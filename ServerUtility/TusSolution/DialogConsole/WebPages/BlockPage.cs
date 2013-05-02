using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization.Json;
using Tus.Communication.Device.AvrComposed;
using Tus.TransControl.Base;

namespace DialogConsole.WebPages
{
    [Export(typeof(IConsolePage))]
    [TusPageMetadata("block control", "block")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class BlockPage : ConsolePageBase
    {
        public override string GetJsonContent()
        {
            var blocks = this.Param.Sheet.InnerBlocks.ToArray();
            var ser = new DataContractJsonSerializer(typeof(IEnumerable<Block>),
                                                     new[] { typeof(Switch), typeof(Motor), typeof(Vehicle), typeof(UsartSensor) });
            return GetJsonContent<IEnumerable<Block>>(blocks, ser);
        }

        public override void ApplyJsonRequest()
        {
        }
    }
}