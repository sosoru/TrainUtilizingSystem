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

        public override IList<Route> Create()
        {
            var parser = new RouteYaml();
            IEnumerable<object> routesobj = parser.ParseFrom(ApplicationSettings.RoutePath);

            IEnumerable<RouteSegmentOnYaml> routes = parser.ParseYamlContent(routesobj);
            return routes.Select(rseg => new Route(Sheet, rseg.Routes) { Name = rseg.Name, Polar = rseg.Polar }).ToList();
        }
    }
}
