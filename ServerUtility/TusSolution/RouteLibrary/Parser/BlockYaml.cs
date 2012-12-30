using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Yaml;
using System.Yaml.Serialization;
using Tus.Route;

namespace Tus.Route.Parser
{
    public class BlockYaml
    {
        private DeviceIdParser pr_addr;
        private RouteParser pr_route;

        public IEnumerable<object> ParseFrom(string path)
        {
            var ser = new YamlSerializer();
            var objs = ser.DeserializeFromFile(path);

            return (IEnumerable<object>)objs[0];

        }

        public IEnumerable<BlockInfo> ParseAbstract(IEnumerable<object> src)
        {
            var arr = ((IEnumerable<object>)src).Where(o => o is Dictionary<object, object>)
                                                        .Cast<Dictionary<object, object>>();

            return arr.Select(obj => new BlockInfo() { Name = (string)obj["name"] })
                        .DefaultIfEmpty(new BlockInfo() { Name = "nothing" });
        }

        public MotorInfo ParseMotor(Dictionary<object, object> src)
        {

            var motor = new MotorInfo()
            {
                Address = pr_addr.FromString((string)src["addr"]).First(),
                RoutePositive = pr_route.FromString((string)src["pos"]).First(),
                RouteNegative = pr_route.FromString((string)src["neg"]).First(),
            };

            return motor;
        }

        public SwitchInfo ParsePoint(Dictionary<object, object> src)
        {
            var pt = new SwitchInfo()
            {
                Address = pr_addr.FromString((string)src["addr"]).First(),
                DirStraight = pr_route.FromString((string)src["s"]),
                DirCurved = pr_route.FromString((string)src["c"]),
            };

            return pt;
        }

        public SensorInfo ParseSensor(Dictionary<object, object> src)
        {
            var sens = new SensorInfo()
            {
                Addresses = pr_addr.FromString((string)src["addr"]).ToList(),
            };

            return sens;
        }

        private object extract_dict(Dictionary<object, object> dict, string key)
        {
            object value;
            if (dict.TryGetValue(key, out value))
            {
                return value;
            }
            return null;
        }

        public IEnumerable<BlockInfo> Parse(string path)
        {
            var objs = this.ParseFrom(path);
            return this.Parse(objs);
        }

        public IEnumerable<BlockInfo> Parse(IEnumerable<object> src)
        {
            var ab = ParseAbstract(src).ToList();

            this.pr_addr = new DeviceIdParser();
            this.pr_route = new RouteParser() { ReferencedBlocks = ab };

            foreach (var r in src)
            {
                var dict = (Dictionary<object, object>)r;
                var route = pr_route.FromString((string)dict["route"]);

                var motor_src = (Dictionary<object, object>)extract_dict(dict, "motor");
                var sens_src = (Dictionary<object, object>)extract_dict(dict, "sensor");
                var ptr_src = (Dictionary<object, object>)extract_dict(dict, "point");
                var isolate = ((bool?)extract_dict(dict, "isolate")) ?? false;

                var block = ab.Find(b => b.Name == (string)dict["name"]);

                block.Route = route.ToList();
                block.Motor = (motor_src != null) ? ParseMotor(motor_src) : null;
                block.Sensor = (sens_src != null) ? ParseSensor(sens_src) : null;
                block.Switch = (ptr_src != null) ? ParsePoint(ptr_src) : null;
                block.IsIsolated = isolate;

                yield return block;
            }

        }
    }
}
