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
    public class BlockPage : ConsolePageBase<IEnumerable<Block>>
    {
        protected override DataContractJsonSerializer JsonSerializer
        {
            get
            {
                return new DataContractJsonSerializer(typeof(IEnumerable<Block>),
                                                     new[] { typeof(Switch), typeof(Motor), typeof(Vehicle), typeof(UsartSensor), typeof(MemoryState) });
            }
        }
        private IEnumerable<Block> Blocks
        {
            get
            {
                var blocks = this.Param.UsingLayout.Sheet.InnerBlocks;
                return blocks;
            }
        }

        public override IEnumerable<Block> GetContent()
        {
            return Blocks;
        }

        public override void ApplyJsonRequest(IEnumerable<Block> obj)
        {
            throw new NotImplementedException();
        }
    }
}