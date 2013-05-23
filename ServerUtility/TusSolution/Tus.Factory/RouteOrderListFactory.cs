using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Tus.TransControl.Base;
using Tus.TransControl.Parser;

namespace Tus.Factory
{
    [Export]
    public class RouteOrderListFactory : FactoryBase<IList<RouteOrder>>
    {
        public BlockSheet Sheet { get; set; }

        public RouteOrder ReverseRoute(RouteOrder posrt)
        {
            var blocks = alignBlocks(posrt.Blocks.Reverse());

            var reverseRoute = new RouteOrder(blocks.ToArray());
            reverseRoute.Name = posrt.Name + "_REV";
            reverseRoute.Polar =
                (posrt.Polar == BlockPolar.Positive)
                    ? BlockPolar.Negative
                    : (posrt.Polar == BlockPolar.Negative)
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
        public override IList<RouteOrder> Create()
        {
            var parser = new RouteYaml();
            IEnumerable<object> routesobj = parser.ParseFrom(ApplicationSettings.RoutePath);
            IEnumerable<RouteSegmentOnYaml> routes = parser.ParseYamlContent(routesobj);

            var list = routes.Select(rseg => new RouteOrder(Sheet, rseg.Routes) { Name = rseg.Name, Polar = rseg.Polar, IsRepeatable = true })
                             .Select(rt => new RouteOrder(alignBlocks(rt.Blocks).ToArray()) { Name = rt.Name, Polar = rt.Polar, IsRepeatable = true });

            return list.Concat(list.ToArray().Select(rt => ReverseRoute(rt))).ToList();
        }
    }
}
