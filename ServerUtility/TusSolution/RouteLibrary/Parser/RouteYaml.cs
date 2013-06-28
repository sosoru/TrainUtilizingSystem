using System;
using System.Collections.Generic;
using System.Linq;
using System.Yaml.Serialization;
using Tus.TransControl.Base;

namespace Tus.TransControl.Parser
{
    public struct RouteSegmentOnYaml
    {
        public string Name { get; set; }
        public IEnumerable<string> Routes { get; set; }
        public BlockPolar Polar { get; set; }
    }

    public class RouteYaml
    {
        public IEnumerable<object> ParseFrom(string path)
        {
            var ser = new YamlSerializer();
            object[] objs = ser.DeserializeFromFile(path);

            return (IEnumerable<object>)objs[0];
        }

        public IEnumerable<object> ParseFromContent(string content)
        {
            var ser = new YamlSerializer();
            object[] objs = ser.Deserialize(content);

            return (IEnumerable<object>)objs[0];
        }

        public IEnumerable<RouteSegmentOnYaml> ParseYamlContent(IEnumerable<object> src)
        {
            IEnumerable<Dictionary<object, object>> objs = src.Where(o => o is Dictionary<object, object>)
                                                              .Cast<Dictionary<object, object>>();
            return objs.Select(pair => new RouteSegmentOnYaml
                                           {
                                               Name = (string)pair["name"],
                                               Routes = extractYamlRoute(pair["route"]),
                                               Polar = extractYamlBlockPolar(pair["polar"])
                                           });
        }

        private BlockPolar extractYamlBlockPolar(object polar)
        {
            if (polar is string)
            {
                var pol = (string) polar;
                pol = pol.Trim().ToLower();

                if (pol == "pos")
                {
                    return BlockPolar.Positive;
                }
                else if (pol == "neg")
                    return BlockPolar.Negative;
                else if (pol == "any")
                    return BlockPolar.Any;
                else
                    throw new InvalidOperationException("block polar parsing failed");

            }
            throw new InvalidOperationException("parsing block polar is not string");
        }

        private IEnumerable<string> extractYamlRoute(object route)
        {
            if (route is string)
                route = new[] { route };

            return
                ((object[])route).Cast<string>()
                                  .SelectMany(s => s.Split(',')).Select(s => s.Trim()).Where(seg => seg.Length > 0);
        }
    }
}