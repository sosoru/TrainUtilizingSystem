using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RouteVisualizer.Models;

using Livet;

namespace RouteVisualizer.Graph
{
    public class Route<Tgate, Tedge>
        where Tgate : IGate<Tgate, Tedge>
        where Tedge : IEdge<Tgate, Tedge>
    {

        public Route()
        {
            this.Edges = new List<Tedge>();
            this.Gates = new List<Tgate>();
        }

        public Route(Route<Tgate, Tedge> rt)
        {
            this.Edges = new List<Tedge>(rt.Edges);
            this.Gates = new List<Tgate>(rt.Gates);
        }

        public virtual IList<Tedge> Edges { get; private set; }
        public virtual IList<Tgate> Gates { get; private set; }

    }

    public class RouteFactory<Tgate, Tedge>
        where Tgate : IGate<Tgate, Tedge>
        where Tedge : IEdge<Tgate, Tedge>
    {
        public Func<Route<Tgate, Tedge>, Tedge, bool> ValidateForRouteFunc { get; private set; }

        public virtual Route<Tgate, Tedge> SearchLoop(Tgate start, Tgate end)
        {
            var route  = new Route<Tgate, Tedge>();

            _totalRoute.Clear();

            recSearchLoop(route, start, end);

            return _totalRoute.OrderBy(_ => _.Edges.Count)
                                .First();
        }

        private IList<Route<Tgate, Tedge>> _totalRoute = new List<Route<Tgate, Tedge>>();
        private void recSearchLoop(Route<Tgate, Tedge> rt, Tgate lastgate, Tgate finding)
        {
            var gateconn = lastgate;

            if (gateconn.Equals(finding))
            {
                var newroute = new Route<Tgate, Tedge>(rt);
                _totalRoute.Add(newroute);
                return;
            }

            foreach (var nextedge in lastgate)
            {
                if (rt.Edges.Contains(nextedge))
                {
                    continue;
                }

                rt.Edges.Add(nextedge);
                rt.Gates.Add(nextedge.NextGate);

                recSearchLoop(rt, nextedge.NextGate, finding);

                rt.Gates.Remove(nextedge.NextGate);
                rt.Edges.Remove(nextedge);
            }


        }


    }
}
