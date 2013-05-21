using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Tus.TransControl.Base;
using Tus.TransControl.Parser;

namespace Tus.Factory
{
    [Export]
    public class RouteListFactory : FactoryBase<IList<Route>>
    {
        public BlockSheet Sheet { get; set; }

        public Route ReverseRoute(Route posrt)
        {
            var blocks = alignBlocks(posrt.RouteOrder.Blocks.Reverse());

            var reverseRoute = new Route(blocks);
            reverseRoute.RouteOrder.Name = posrt.RouteOrder.Name + "_REV";
            reverseRoute.RouteOrder.Polar =
                (posrt.RouteOrder.Polar == BlockPolar.Positive)
                    ? BlockPolar.Negative
                    : (posrt.RouteOrder.Polar == BlockPolar.Negative)
                          ? BlockPolar.Positive
                          : BlockPolar.Any;
            return reverseRoute;
        }

        // endless routeの場合はisolated blockを頭出ししないとダメ
        // not endlessだと余計なお世話
        // not isolated block の頭出し
        private static IEnumerable<Block> alignBlocks(IEnumerable<Block> blks)
        {
            var found = false;
            var buffer = new List<Block>();

            foreach (var b in blks)
            {
                //first isolated has been found and return the rest
                if (found)
                    yield return b;
                else
                {
                    // first isolated is found now
                    if (b.IsIsolated)
                    {
                        found = true;
                    }
                    buffer.Add(b);
                }
            }

            // return the blocks before first isolated block
            foreach (var b in buffer)
                yield return b;
        }

        //todo : extract alignBlocks to super project
        public override IList<Route> Create()
        {
            var parser = new RouteYaml();
            IEnumerable<object> routesobj = parser.ParseFrom(ApplicationSettings.RoutePath);
            IEnumerable<RouteSegmentOnYaml> routes = parser.ParseYamlContent(routesobj);

            var list = routes.Select(rseg => new Route(Sheet, rseg.Routes) { Name = rseg.Name, Polar = rseg.Polar, IsRepeatable = true })
                             .Select(rt => new Route(alignBlocks(rt.RouteOrder.Blocks)) { Name = rt.RouteOrder.Name, Polar = rt.RouteOrder.Polar, IsRepeatable = true });

            return list.Concat(list.ToArray().Select(rt => ReverseRoute(rt))).ToList();
        }
    }
}
