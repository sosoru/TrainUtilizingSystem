using System.Collections.Generic;
using System.Linq;
using System.Yaml.Serialization;

namespace Tus.TransControl.Parser
{
    public struct RouteSegmentOnYaml
    {
        public string Name { get; set; }
        public IEnumerable<string> Routes { get; set; }
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
                                           });
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