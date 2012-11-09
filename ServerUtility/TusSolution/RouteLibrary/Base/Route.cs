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

    }

    public class Route
    {
        //todo: to replace all ilist<block>
        public IList<Block> Blocks { get; private set; }
        public IList<ControllingRoute> LockingUnit { get; protected set; }

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
                    if (b.HasMotor)
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

            this.LockingUnit = new ReadOnlyCollection<IList<Block>>(locked_blocks.ToArray());
            InitLockingPosition();
        }

        public void InitLockingPosition()
        {
            this.ind_end = -1;
            this.ind_start = 0;
        }

        public void LockNextUnit()
        {
            if (ind_end + 1 < this.LockingUnit.Count
                && (ind_start <= ind_end + 1))
            {
                ind_end++;
            }
        }

        public void ReleaseBeforeUnit()
        {
            if (ind_start + 1 < this.LockingUnit.Count
                && (ind_start <= ind_end + 1))
            {
                ind_start++;
            }
        }

        public IEnumerable<Block> LockedBlocks
        {
            get
            {
                var start = this.ind_start;
                var end = this.ind_end;

                var seq = Enumerable.Range(start, end - start + 1)
                    .SelectMany(i =>
                                    {
                                        this.LockingUnit[i].Blocks;
                                    });

                return seq;
            }
        }


        public bool IsSectionFinished
        {
            get
            {
                // a sensor on end of locked section detects train
                //return this.LockedBlocks.Last().IsDetectingTrain;
                return this.LockingUnit.Last().Last().IsDetectingTrain;
            }
        }

        public bool IsLeftSectionFirst
        {
            get
            {
                //return this.LockedBlocks.First().IsDetectingTrain;
                return this.LockingUnit.First().First().IsDetectingTrain;
            }
        }

        public void LookUpTrain()
        {
                while (!this.IsRouteFinished)
                {
                    if (this.IsSectionFinished)
                        this.LockNextUnit();
                    else
                        break;
                }

                while (this.IsLeftSectionFirst)
                    this.ReleaseBeforeUnit();
        }

        public bool IsRouteFinished
        {
            get
            {
                // IsSectionFinished And the locked units of this route reach the end of them
                return this.ind_end == this.LockingUnit.Count() - 1 && this.IsSectionFinished;
            }
        }
    }
}
