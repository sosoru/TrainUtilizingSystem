using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Tus.TransControl.Base
{
    [DataContract]
    public class ControllingUnit
    {
        [DataMember]
        public IList<Block> Blocks { get; set; }

        public Block ControlBlock
        {
            get { return this.Blocks.First(b => b.HasMotor); }
        }

        public IEnumerable<Block> HaltableBlocks
        {
            get
            {
                foreach (var b in this.Blocks)
                {
                    if (b == this.Blocks.First())
                        yield return b; // stop at entrance of the blocks this class govern

                    if (b.HasSensor)
                        yield return b; // stop by a IR sensor
                }
            }
        }

        public bool CanBeAllocated
        {
            get
            {
                return this.Blocks.All(b => !b.IsBlocked);
            }
        }

        public void Allocate()
        {
            this.Blocks.ForEach(b =>
                {
                    if (b.IsBlocked)
                        throw new InvalidOperationException("Allocate() tried to allocate a blocked block");

                    b.IsBlocked = true;
                });
        }

        public void Release()
        {
            this.Blocks.ForEach(b =>
                {
                    if (!b.IsBlocked)
                        throw new InvalidOperationException("Release() tried to relase a released block");

                    b.IsBlocked = false;
                });
        }

        public bool IsBlocked
        {
            get
            {
                return this.Blocks.Any(b => b.IsBlocked);
            }
        }
    }
    /// <summary>
    /// 列車が運行する経路を保持するクラス．
    /// 与えられたBlockの配列を，運行に必要なBlockを加味しながら，シーケンシャルに確保・解放する
    /// </summary>
    [DataContract]
    public class Route
    {
        [DataMember]
        public string Name { get; set; }
        public IList<Block> Blocks { get; private set; }
        public IList<ControllingUnit> Units { get; protected set; }

        [DataMember]
        public Queue<ControllingUnit> LockedUnits { get; private set; }
        public BlockPolar Polar { get; set; }

        public bool IsRepeatable { get; set; }

        private int ind_current;

        [DataMember(IsRequired = false)]
        public ICollection<Block> LockedBlocks
        {
            get
            {
                if (this.LockedUnits == null)
                    return new Block[] { };

                return this.LockedUnits.ToArray().SelectMany(u => u.Blocks).ToList();
            }
        }

        public bool IsSectionFinished
        {
            get
            {
                // a sensor on end of locked section detects train
                //return this.LockedBlocks.Last().IsDetectingTrain;
                return this.Units.Last().ControlBlock.IsDetectingTrain;
            }
        }

        public bool IsLeftSectionFirst
        {
            get
            {
                //return this.LockedBlocks.First().IsDetectingTrain;
                return this.Units.First().ControlBlock.IsDetectingTrain;
            }
        }

        public bool IsRouteFinished
        {
            get
            {
                // IsSectionFinished And the locked units of this route reach the end of them
                return this.ind_current == this.Units.Count() - 1 && this.IsSectionFinished;
            }
        }

        #region Initialize

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

        private IEnumerable<ControllingUnit> locked_blocks
        {
            get
            {
                var l = new List<Block>();

                foreach (var b in this.Blocks)
                {
                    l.Add(b);
                    if (b.IsIsolated)
                    {
                        var cr = new ControllingUnit() { Blocks = new List<Block>(l) };

                        yield return cr;
                        l.Clear();
                    }
                }

                if (l.Count > 0)
                    yield return new ControllingUnit() { Blocks = new List<Block>(l) };

                //todo : throw exception (not terminated by a block having a sensor
            }
        }

        public Route(BlockSheet sheet, IEnumerable<string> names)
            : this(names.Select(s => sheet.InnerBlocks.First(b => b.Name == s)).ToList())
        {
        }

        public Route(IEnumerable<Block> segs)
        {
            this.Blocks = new ReadOnlyCollection<Block>(segs.ToList());

            this.Units = new ReadOnlyCollection<ControllingUnit>(locked_blocks.ToArray());
            this.LockedUnits = new Queue<ControllingUnit>();
            this.ind_current = -1;

            InitLockingPosition();
        }

        public void InitLockingPosition()
        {
            while (ReleaseBeforeUnit()) ;

        }

        #endregion

        #region TryLock Methods

        public bool TryLockNeighborUnit(int i)
        {
            ControllingUnit nextroute;

            return TryLockNeighborUnit(i, out nextroute);
        }

        public bool TryLockNeighborUnit(int i, out ControllingUnit nextunit)
        {
            // todo: minus value support
            int ind = ind_current + i;
            nextunit = null;

            if (this.IsRepeatable)
            {
                ind = ind % this.Units.Count;
            }

            if (ind < this.Units.Count
                && this.Units[ind].CanBeAllocated)
            {
                nextunit = this.Units[ind];
                return true;
            }
            return false;

        }

        #endregion

        #region Lock Methods

        public bool LockNextUnit()
        {
            var cntlocking = 1;
            if (TryLockNeighborUnit(cntlocking))
            {
                var ind = ind_current + cntlocking;
                if (this.IsRepeatable)
                    ind = ind % this.Units.Count;

                this.Units[ind].Allocate();
                this.LockedUnits.Enqueue(this.Units[ind]);

                this.ind_current = ind;
                return true;
            }

            return false;
        }

        public bool ReleaseBeforeUnit()
        {
            if (this.LockedUnits.Count > 0)
            {
                var deleted = this.LockedUnits.Dequeue();
                deleted.Release();

                return true;
            }
            return false;
        }

        #endregion

        public ControllingUnit GetLockingControlingRoute(Block parentBlock)
        {
            return this.Units.FirstOrDefault(b => b.ControlBlock == parentBlock);
        }

        public RouteSegment GetSegment(Block blk)
        {
            var ind = this.Blocks.IndexOf(blk);

            if (ind == 0)
            {
                Block first = null;
                if (this.IsRepeatable)
                    first = this.Blocks.Last();

                return new RouteSegment(first, this.Blocks[ind + 1]);
            }
            else if (ind >= this.Blocks.Count - 1)
            {
                Block last = null;
                if (this.IsRepeatable)
                    last = this.Blocks.First();

                return new RouteSegment(this.Blocks[ind - 1], last);
            }
            else
            {
                return new RouteSegment(this.Blocks[ind - 1], this.Blocks[ind + 1]);
            }
        }

        public void LookUpTrain()
        {
            while (!this.IsRouteFinished)
            {
                if (!this.IsSectionFinished)
                    this.LockNextUnit();
                else
                    break;
            }

            while (this.IsLeftSectionFirst)
                this.ReleaseBeforeUnit();
        }

        public void AllocateTrain(Block cntblock, int len)
        {
            if (len <= 0)
                throw new ArgumentOutOfRangeException("len must be greater than 0");

            var blocklist = this.Units.Select((r, i) => new { ind = i, route = r }).ToArray();
            var blockunit = blocklist.FirstOrDefault(u => u.route.Blocks.Contains(cntblock));

            if (blockunit == null)
                throw new IndexOutOfRangeException("block not found");

            while (this.ReleaseBeforeUnit()) ;

            //渡されたcntblockは先頭のブロック
            this.ind_current = blockunit.ind - (len - 1); // (index of before block from current) - (vehicle length -1)

            if (this.IsRepeatable && this.ind_current < 0)
                this.ind_current += this.Units.Count;

            this.ind_current--;
            while (len-- > 0)
            {
                if (!this.LockNextUnit())
                    throw new InvalidOperationException("cannot allocate block");
            }
        }

        #region ToString

        private string GetBlockExpression(Block blk)
        {
            var exp = ":";
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

        public override string ToString()
        {
            var strunits = this.Units.Aggregate("", (ac, cntrt) =>
                                                    ac + "(" +
                                                    cntrt.Blocks.Aggregate("", (a, b) => a + GetBlockExpression(b) + " ")
                                                         .Trim() + ") ")
                               .Trim();
            return string.Format("{0} : {1}", this.Name, strunits);
        }

        #endregion

        public ControllingUnit GetNeighborUnit(int p)
        {
            int ind = this.ind_current + p;

            if (this.IsRepeatable)
                ind = ind % this.Units.Count;

            //is valid index
            if (ind < 0 || this.Units.Count <= ind)
                throw new ArgumentOutOfRangeException("no unit is found");

            return this.Units[ind];
        }

        public int GetDistanceOfBlockedUnit(int limit)
        {
            for (int i = 1; i <= limit; ++i)
            {
                if (this.BlockRemainsCount > i
                    && this.GetNeighborUnit(i).IsBlocked)
                {
                    return i;
                }
            }
            return limit; 
        }

        public int BlockRemainsCount
        {
            get
            {
                if (this.IsRepeatable)
                    return int.MaxValue;
                else
                    return this.Units.Count - this.ind_current - 1;
            }
        }

        public int BlockPassedCount
        {
            get
            {
                if (this.IsRepeatable)
                    return int.MaxValue;
                else
                    return (this.ind_current < 0) ? 0 : this.ind_current + 1;
            }

        }
    }
}
