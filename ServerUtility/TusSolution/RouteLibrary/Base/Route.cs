using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouteLibrary.Base
{
    public class Route
    {
        public Route(IEnumerable<RouteSegment> segs)
        {

        }

        public RouteSegment Current { get; protected set; }
    }
}
