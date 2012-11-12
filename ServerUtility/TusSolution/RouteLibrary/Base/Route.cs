using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace RouteLibrary.Base
{

    public class ControllingRoute
    {
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
    }

    public class Route
    {
        //todo: to replace all ilist<block>
        public IList<Block> Blocks { get; private set; }
        public IList<ControllingRoute> Units { get; protected set; }

        private int ind_start = 0, ind_end = 0;
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

        private IEnumerable<ControllingRoute> locked_blocks
        {
            get
            {
                var l = new List<Block>();

                foreach (var b in this.Blocks)
                {
                    l.Add(b);
                    if (b.IsIsolated)
                    {
                        var cr = new ControllingRoute() { Blocks = new List<Block>(l) };

                        yield return cr;
                        l.Clear();
                    }
                }

                //todo : throw exception (not terminated by a block having a sensor
            }
        }

        public Route(BlockSheet sheet, IEnumerable<string> names)
            : this(names.Select(s => sheet.InnerBlocks.First(b => b.Name == s)).ToList()) { }

        public Route(IList<Block> segs)
        {
            this.Blocks = new ReadOnlyCollection<Block>(segs);

            this.Units = new ReadOnlyCollection<ControllingRoute>(locked_blocks.ToArray());
            this.ind_end = -1;
            this.ind_start = 0;
            InitLockingPosition();
        }

        public void InitLockingPosition()
        {
            while (ReleaseBeforeUnit()) ;

        }

        public ControllingRoute NeighborUnit(int i)
        {
            return this.Units[this.ind_end + i];
        }

        public bool TryLockNeighborUnit(int i)
        {
            if (ind_end + i < this.Units.Count
                && this.Units[this.ind_end + i].CanBeAllocated)
            {
                return true;
            }
            return false;
        }

        public bool LockNextUnit()
        {
            if (ind_end + 1 < this.Units.Count
                && this.Units[this.ind_end+1].CanBeAllocated)
            {
                ind_end++;
                this.Units[this.ind_end].Allocate();

                return true;
            }
            return false;
        }

        public bool ReleaseBeforeUnit()
        {
            if ( (ind_start <= ind_end)
                && !this.Units[this.ind_start].CanBeAllocated)
            {
                this.Units[this.ind_start].Release();
                ind_start++;

                return true;
            }
            return false;
        }

        public IEnumerable<Block> LockedBlocks
        {
            get
            {
                var start = this.ind_start;
                var end = this.ind_end;

                var seq = Enumerable.Range(start, end - start + 1)
                    .SelectMany(i =>

                                        this.Units[i].Blocks
                                    );

                return seq;
            }
        }

        public IEnumerable<ControllingRoute> LockedUnits
        {
            get
            {
                return Enumerable.Range(ind_start, ind_end - ind_start + 1).Select(i => this.Units[i]);
            }
        }

        public ControllingRoute GetLockingControlingRoute(Block parentBlock)
        {
            return this.Units.FirstOrDefault(b => b.ControlBlock == parentBlock);
        }

        private IDictionary<Block, RouteSegment> segments_;
        public IDictionary<Block, RouteSegment> Segments
        {
            get
            {
                if (this.segments_ == null)
                {
                    this.segments_ = to_route_dict(this.Blocks);
                }
                return this.segments_;
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

            var blockunit = this.Units.Select((r, i) => new { ind = i, route = r })
                                            .FirstOrDefault(u => u.route.Blocks.Contains(cntblock));

            if (blockunit == null)
                throw new IndexOutOfRangeException("block not found");

            while (this.ReleaseBeforeUnit()) ;

            this.ind_end = blockunit.ind;
            this.ind_start = blockunit.ind - (len-1);

            if (this.ind_start < 0)
                this.ind_start = 0;

            this.LockedUnits.ForEach(u => u.Allocate());
        }

        public bool IsRouteFinished
        {
            get
            {
                // IsSectionFinished And the locked units of this route reach the end of them
                return this.ind_end == this.Units.Count() - 1 && this.IsSectionFinished;
            }
        }
    }
}
