using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouteLibrary.Base
{
    public class Route
    {
        //todo: to replace all ilist<block>
        public IDictionary<Block, RouteSegment> Segments { get; private set; }
        public IList<Dictionary<Block, RouteSegment>> LockedUnits { get; protected set; }

        private int ind_start = 0, ind_end = 0;
        private IDictionary<Block, RouteSegment> to_route_dict(IList<Block> list)
        {
            int i;
            var dict = new Dictionary<Block, RouteSegment>();

            for (i = 1; i < list.Count - 1; ++i)
            {
                dict.Add(list[i], new RouteSegment(list[i - 1], list[i + 1]));
            }

            return dict;
        }

        public Route(IList<Block> segs)
        {
            this.Segments = to_route_dict(segs);

            int i = 0;
            //separate by block having motor
            var units = this.Segments.Zip(segs.Select(s => (s.HasMotor) ? ++i : i), (b, g) => new KeyValuePair<KeyValuePair<Block, RouteSegment>, int>(b, g))
                                    .GroupBy(k => k.Value)
                                    .Select(g => g.Select(pair => pair.Key).ToDictionary(p => p.Key, p => p.Value));

            this.LockedUnits = units.ToList();
        }



        public void LockNextUnit()
        {
            if (ind_end < this.LockedUnits.Count - 1
                && ind_start < ind_end)
            {
                ind_end++;
            }
        }

        public void ReleaseBeforeUnit()
        {
            if (ind_start < this.LockedUnits.Count - 1
                && ind_start < ind_end)
            {
                ind_start++;
            }
        }

        public IDictionary<Block, RouteSegment> LockedSegments
        {
            get
            {
                return Enumerable.Range(this.ind_start, this.ind_end - this.ind_start)
                            .SelectMany(i => this.LockedUnits[i])
                            .ToDictionary(pair => pair.Key, pair => pair.Value);
            }
        }

    }
}
