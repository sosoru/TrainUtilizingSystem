using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Yaml.Serialization;

namespace Tus.TransControl.Parser
{
    public struct VehicleGroupSegmentOnYaml
    {
        public string groupname;
        public string idential;
        public VehicleSettingOnYaml[] vehicles;
    }
    public struct VehicleSettingOnYaml
    {
        public string name;
        public string mode;
        public string pos;
        public string route;
    }
    public class VehicleGroupYaml
    {        public IEnumerable<object> ParseFrom(string path)
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

        public IEnumerable<VehicleGroupSegmentOnYaml> ParseYamlContent(IEnumerable<object> src)
        {
            IEnumerable<Dictionary<object, object>> objs = src.Where(o => o is Dictionary<object, object>)
                                                              .Cast<Dictionary<object, object>>();
            return objs.Select(pair => new VehicleGroupSegmentOnYaml
                                           {
                                               groupname = (string)pair["groupname"],
                                               idential= (string)pair["idential"],
                                               vehicles = ParseVehicleSetting((IEnumerable<object>)pair["vehicles"]).ToArray(),
                                           });
        }

        public IEnumerable<VehicleSettingOnYaml> ParseVehicleSetting(IEnumerable<object> src)
        {
             IEnumerable<Dictionary<object, object>> objs = src.Where(o => o is Dictionary<object, object>)
                                                              .Cast<Dictionary<object, object>>();
            return objs.Select(pair => new VehicleSettingOnYaml
                                           {
                                               name = (string) pair["name"],
                                               mode = (string) pair["mode"],
                                               pos = (string) pair["pos"],
                                               route = (string) pair["route"]
                                           });

        }
    }
}
