using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

using Tus;
using Tus.TransControl.Base;

namespace Tus.Factory
{
    public class RouteFactory : FactoryBase<IList<Route>>
    {
        public override IList<Route> Create()
        {
            var parser = new Tus.TransControl.Parser.RouteYaml();
            var routesobj = parser.ParseFrom(this.ApplicationSettings.RoutePath);

            var routes = parser.ParseYamlContent(routesobj);
            return routes.Select(rseg => )
        }
    }
}
