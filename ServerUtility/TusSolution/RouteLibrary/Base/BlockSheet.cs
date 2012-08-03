using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.Specialized;
using System.Collections.ObjectModel;

using SensorLibrary;
using SensorLibrary.Packet;
using SensorLibrary.Packet.Control;

namespace RouteLibrary.Base
{
    public class BlockSheet
        : IEquatable<BlockSheet>
    {

        public string Name { get; set; }
        public ReadOnlyCollection<Block> InnerBlocks { get; private set; }
        public PacketDispatcher Dispatcher { get; private set; }

        public BlockSheet(IEnumerable<BlockInfo> blockinfos, PacketDispatcher dispat)
        {
            this.Dispatcher = dispat;

            var blocks = blockinfos.Select(i => new Block(i, this));

            this.Name = "";
            this.InnerBlocks = new ReadOnlyCollection<Block>(blocks.ToList());
        }

        #region implementation of IEqualable
        public static bool operator ==(BlockSheet A, BlockSheet B)
        {
            if (ReferenceEquals(A, B))
                return true;
            else if ((object)A == null || (object)B == null)
                return false;
            else
                return (A.Name == B.Name);
        }
        public static bool operator !=(BlockSheet A, BlockSheet B) { return !(A == B); }

        public bool Equals(BlockSheet other)
        {
            return (this == other);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // TODO: write your implementation of Equals() here
            return this.Equals((BlockSheet)obj);
        }

        #endregion


    }
}
