using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Tus.TransControl.Base
{
    /// <summary>
    ///     ひとつの駆動源を持ち，絶縁されたセクションを表す．（having one motor, being terminated by a isolator)
    /// </summary>
    [DataContract]
    public class ControlUnit
    {
        [DataMember]
        public IList<Block> Blocks { get; set; }

        public Block ControlBlock
        {
            get { return Blocks.First(b => b.HasMotor); }
        }

        public IEnumerable<Block> HaltableBlocks
        {
            get
            {
                foreach (Block b in Blocks)
                {
                    if (b == Blocks.First())
                        yield return b; // stop at entrance of the blocks this class govern

                    if (b.HasSensor)
                        yield return b; // stop by a IR sensor
                }
            }
        }

        public bool CanBeAllocated
        {
            get { return Blocks.All(b => !b.IsBlocked); }
        }

        public bool IsBlocked
        {
            get { return Blocks.Any(b => b.IsBlocked); }
        }

        public void Allocate()
        {
            Blocks.ForEach(b =>
                               {
                                   if (b.IsBlocked)
                                       throw new InvalidOperationException(
                                           "Allocate() tried to allocate a blocked block");

                                   b.IsBlocked = true;
                               });
        }

        public void Release()
        {
            Blocks.ForEach(b =>
                               {
                                   if (!b.IsBlocked)
                                       throw new InvalidOperationException("Release() tried to relase a released block");

                                   b.IsBlocked = false;
                               });
        }
    }

    /// <summary>
    ///     列車の進むべき経路をブロックから生成するクラス．ControlUnitのIEnumeratableファクトリに相当．
    /// </summary>
    public class RouteOrder
    {
        public RouteOrder(Block[] blocks)
        {
            Blocks = blocks;
            Units = new ReadOnlyCollection<ControlUnit>(locked_blocks.ToArray());
        }

        [DataMember]
        public string Name { get; set; }

        public IReadOnlyList<Block> Blocks { get; private set; }
        public IReadOnlyList<ControlUnit> Units { get; private set; }
        public BlockPolar Polar { get; set; }
        public bool IsRepeatable { get; set; }

        //todo : move logic to ControlUnit
        private IEnumerable<ControlUnit> locked_blocks
        {
            get
            {
                var l = new List<Block>();

                foreach (Block b in Blocks)
                {
                    l.Add(b);
                    if (b.IsIsolated)
                    {
                        var cr = new ControlUnit { Blocks = new List<Block>(l) };

                        yield return cr;
                        l.Clear();
                    }
                }

                if (l.Count > 0)
                    yield return new ControlUnit { Blocks = new List<Block>(l) };

                //todo : throw exception (not terminated by a block having a sensor
            }
        }

        public string ToString()
        {
            string strunits = Units.Aggregate("", (ac, cntrt) =>
                                                  ac + "(" +
                                                  cntrt.Blocks.Aggregate("", (a, b) => a + GetBlockExpression(b) + " ")
                                                       .Trim() + ") ")
                                   .Trim();
            return string.Format("{0} : {1}", Name, strunits);
        }

        public ControlUnit GetLockingControlingRoute(Block parentBlock)
        {
            return Units.FirstOrDefault(b => b.ControlBlock == parentBlock);
        }


        //todo : move to iterator
        public RouteSegment GetSegment(Block blk)
        {
            //var ind = this.Blocks.Where(b => blk == b).Select((b, i) => i).First();

            //if (this.Blocks.First() == blk)
            //{
            //    Block first = null;
            //    if (this.IsRepeatable)
            //        first = this.Blocks.Last();

            //    return new RouteSegment(first, this.Blocks[ind + 1]);
            //}
            //else if (this.Blocks.Last() == blk)
            //{
            //    Block last = null;
            //    if (this.IsRepeatable)
            //        last = this.Blocks.First();

            //    return new RouteSegment(this.Blocks[ind - 1], last);
            //}
            //else
            //{
            //    return new RouteSegment(this.Blocks[ind - 1], this.Blocks[ind + 1]);
            //}
            throw new NotImplementedException();
        }

        private string GetBlockExpression(Block blk)
        {
            string exp = ":";
            if (blk.HasMotor)
                exp += "m";

            if (blk.HasSwitch)
                exp += "s";

            if (blk.HasSensor)
                exp += "e";

            if (blk.IsIsolated)
                exp += "i";

            if (exp.Length == 1)
                exp = "";

            return blk.Name + exp;
        }

        private ControlUnitContainer createContainer(int index)
        {
            return new ControlUnitContainer(this, index);
        }

        public IEnumerable<ControlUnitContainer> Enumerate(ControlUnit from)
        {
            ControlUnitContainer container = CreateContainer(@from);

            yield return container;
            while (true)
            {
                ControlUnitContainer next = container.Next;
                if (next == null)
                    break;
                else
                    yield return next;
            }
        }

        public ControlUnitContainer CreateContainer(ControlUnit unit)
        {
            int ind = Units.Where(u => u == unit).Select((u, i) => i).First();

            if (ind < 0)
                throw new KeyNotFoundException("block is notfound");

            ControlUnitContainer container = createContainer(ind);
            return container;
        }

        public ControlUnit SearchUnit(Block blk)
        {
            var blocklist = Units.Select((r, i) => new { ind = i, unit = r }).ToArray();
            var blockunit = blocklist.FirstOrDefault(u => u.unit.Blocks.Contains(blk));

            if (blockunit == null)
                throw new IndexOutOfRangeException("block not found");

            return blockunit.unit;
        }
    }

    [DataContract]
    public class ControlUnitContainer
    {
        public ControlUnitContainer(RouteOrder order, int index)
        {
            Order = order;
            Index = index;
        }

        public int Index { get; set; }
        public RouteOrder Order { get; set; }

        public ControlUnit Unit
        {
            get { return Order.Units[Index]; }
        }

        public ControlUnitContainer Next
        {
            get { return GetNeighbor(1); }
        }

        public ControlUnitContainer Before
        {
            get { return GetNeighbor(-1); }
        }

        public ControlUnitContainer GetNeighbor(int distance)
        {
            distance %= Order.Units.Count;

            if (distance > 0)
            {
                int nextind = Index + distance;
                if (nextind >= Order.Units.Count)
                {
                    if (Order.IsRepeatable)
                    {
                        nextind %= Order.Units.Count;
                    }
                    else
                        return null;
                }

                return new ControlUnitContainer(Order, nextind);
            }
            else if (distance < 0)
            {
                int beforeind = Index - (-distance);
                if (beforeind < 0)
                {
                    if (Order.IsRepeatable)
                    {
                        beforeind += Order.Units.Count;
                    }
                    else
                        return null;
                }

                return new ControlUnitContainer(Order, beforeind);
            }
            else
                return this;
        }

        public int GetDistanceOfBlockedUnit(int limit)
        {
            ControlUnitContainer container = this;
            for (int i = 0; i <= limit; ++i)
            {
                if (container == null || container.Unit.IsBlocked)
                    return i;
                else
                    container = container.Next;
            }
            return limit;
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
            throw new NotImplementedException();
            return base.Equals(obj);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Index.GetHashCode() ^ Order.GetHashCode();
        }

        public int BlockPassedCount
        {
            get
            {
                if (this.Order.IsRepeatable)
                {
                    return int.MaxValue;
                }
                else
                {
                    return this.Index;
                }
            }
        }
    }

    /// <summary>
    ///     列車が運行する経路を保持するクラス．
    ///     与えられたBlockの配列を，運行に必要なBlockを加味しながら，シーケンシャルに確保・解放する
    /// </summary>
    [DataContract]
    public class Route
    {
        //private int ind_current;
        private readonly RouteOrder _routeOrder;
        private IEnumerator<ControlUnitContainer> _order_enmtor;

        [DataMember]
        public Queue<ControlUnit> LockedUnits { get; private set; }

        [DataMember(IsRequired = false)]
        public ICollection<Block> LockedBlocks
        {
            get
            {
                if (LockedUnits == null)
                    return new Block[] { };

                return LockedUnits.ToArray().SelectMany(u => u.Blocks).ToList();
            }
        }

        public ControlUnit CenterUnit
        {
            get
            {
                int len = LockedUnits.Count;

                if (len < 0) // 確保していなければnull
                    return null;
                else if (len >= 2) //先頭から2つめのブロックがCenterUnit
                    return LockedUnits.ToArray()[1];
                else
                    return LockedUnits.First();
            }
        }

        public ControlUnitContainer HeadContainer
        {
            get { return this._order_enmtor.Current; }
        }

        public RouteOrder RouteOrder
        {
            get { return _routeOrder; }
        }

        #region Initialize

        public Route(BlockSheet sheet, IEnumerable<string> names)
            : this(names.Select(s => sheet.InnerBlocks.First(b => b.Name == s)).ToList())
        {
        }

        public Route(IEnumerable<Block> segs)
        {
            _routeOrder = new RouteOrder(segs.ToArray());
            LockedUnits = new Queue<ControlUnit>();
            //this.ind_current = -1;

            InitLockingPosition();
        }

        private IDictionary<Block, RouteSegment> to_route_dict(IList<Block> list)
        {
            int i;
            var dict = new Dictionary<Block, RouteSegment>();

            if (list.Count < 2)
                return dict;

            dict.Add(list[0], new RouteSegment(null, list[1]));
            for (i = 1; i < list.Count - 1; ++i)
            {
                dict.Add(list[i], new RouteSegment(list[i - 1], list[i + 1]));
            }

            if (!dict.ContainsKey(list.Last()))
                dict.Add(list.Last(), new RouteSegment(list[list.Count - 2], null));

            return dict;
        }

        public void InitLockingPosition()
        {
            while (ReleaseLastUnit()) ;
        }

        private void initEnumerator(ControlUnit unit)
        {
            if (_order_enmtor != null)
            {
                InitLockingPosition();
                _order_enmtor.Dispose();
                _order_enmtor = null;
            }

            _order_enmtor = RouteOrder.Enumerate(unit).GetEnumerator();
            _order_enmtor.MoveNext();
            allocateCurrent();
        }

        #endregion

        #region TryLock Methods

        //public bool TryLockNeighborUnit(int i)
        //{
        //    ControlUnit nextroute;

        //    return TryLockNeighborUnit(i, out nextroute);
        //}

        //public bool TryLockNeighborUnit(int i, out ControlUnit nextunit)
        //{
        //    // todo: minus value support
        //    int ind = ind_current + i;
        //    nextunit = null;

        //    if (RouteOrder.IsRepeatable)
        //    {
        //        ind = ind%RouteOrder.Units.Count;
        //    }

        //    if (ind < RouteOrder.Units.Count
        //        && RouteOrder.Units[ind].CanBeAllocated)
        //    {
        //        nextunit = RouteOrder.Units[ind];
        //        return true;
        //    }
        //    return false;
        //}

        #endregion

        #region Lock Methods

        /// <summary>
        ///     次のControlUnitをロック．戻り値は成功したかどうか
        /// </summary>
        /// <returns>ロックできたならばTrue．失敗した場合はFalse</returns>
        public bool LockNextUnit()
        {
            if (_order_enmtor == null)
                throw new InvalidOperationException("ロックできる状態にありません．AllocateTrainが呼び出されているか確認してください");

            ControlUnitContainer next = _order_enmtor.Current.Next;
            if (next != null && !next.Unit.CanBeAllocated)
            {
                bool movecond = _order_enmtor.MoveNext();
                if (!movecond)
                    return false;

                allocateCurrent();
                return true;
            }
            else
                return false;
        }

        private void allocateCurrent()
        {
            _order_enmtor.Current.Unit.Allocate();
            LockedUnits.Enqueue(_order_enmtor.Current.Unit);
        }

        public bool ReleaseLastUnit()
        {
            if (_order_enmtor == null)
                throw new InvalidOperationException("ロックできる状態にありません．AllocateTrainが呼び出されているか確認してください");

            if (LockedUnits.Count > 0)
            {
                ControlUnit deleted = LockedUnits.Dequeue();
                deleted.Release();

                return true;
            }
            return false;
        }

        #endregion

        public void LookUpTrain()
        {
            //while (!RouteOrder.IsRouteFinished)
            //{
            //    if (!RouteOrder.IsSectionFinished)
            //        this.LockNextUnit();
            //    else
            //        break;
            //}

            //while (RouteOrder.IsLeftSectionFirst)
            //    this.ReleaseLastUnit();
            throw new NotImplementedException();
        }

        public void AllocateTrain(Block cntblock, int len)
        {
            if (len <= 0)
                throw new ArgumentOutOfRangeException("len must be greater than 0");

            ControlUnit unit = RouteOrder.SearchUnit(cntblock);
            ControlUnitContainer container = RouteOrder.CreateContainer(unit);

            //len == 1,2 -> 渡されたブロック
            //len > 2 -> 先頭の一個前
            //get tail
            ControlUnitContainer tail = container;
            if (len > 2)
                tail = container.GetNeighbor(-(len - 2));

            initEnumerator(tail.Unit);
            while (len-- > 0)
            {
                if (!LockNextUnit())
                    throw new InvalidOperationException("cannot allocate block");
            }
        }

        #region ToString

        public override string ToString()
        {
            return _routeOrder.ToString();
        }

        #endregion

        //public int IndCurrent
        //{
        //    set { ind_current = value; }
        //    get { return ind_current; }
        //}
    }
}