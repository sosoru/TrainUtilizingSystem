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
    public class BlockPage : ConsolePageBase<IEnumerable<Block>, IEnumerable<Block>>
    {
        protected override IEnumerable<Type> KnownTypesWhenSerialization
        {
            get
            {
                return new[]
                           {
                               typeof (Switch), typeof (Motor), typeof (Vehicle), typeof (UsartSensor),
                               typeof (MemoryState)
                           };
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

        public override IEnumerable<Block> CreateSendingContent()
        {
            return Blocks;
        }

        public override void ApplyReceivedJsonRequest()
        {
        }
    }
}