using System.Collections.Generic;
using System.Linq;
using System.Yaml.Serialization;
using Tus.Communication;
using Tus.TransControl.Base;

namespace Tus.TransControl.Parser
{
    public class BlockYaml
    {
        private DeviceIdParser pr_addr;
        private RouteLiteralParser _prRouteLiteral;

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

        public MotorInfo ParseMotor(RouteSegmentInfo[] route, Dictionary<object, object> src)
        {
            //todo : alert insufficient parameters when route.length > 2 
            var motor = new MotorInfo()
            {
                Addresses = DeviceIdParser.FromString((string)src["addr"]),
                RoutePositive = (src.ContainsKey("pos")) 
                                    ? _prRouteLiteral.FromString((string)src["pos"]).First()
                                    : route.First(),
                RouteNegative = (src.ContainsKey("neg")) 
                                    ? _prRouteLiteral.FromString((string)src["neg"]).First()
                                    : route.Last(),
            };

            return motor;
        }

        public SwitchInfo ParsePoint(RouteSegmentInfo[] route, Dictionary<object, object> src)
        {
            var pt = new SwitchInfo()
            {
                Addresses = DeviceIdParser.FromString((string)src["addr"]),
                DirStraight = _prRouteLiteral.FromString((string)src["s"]),
                DirCurved = _prRouteLiteral.FromString((string)src["c"]),
            };

            return pt;
        }

        public SensorInfo ParseSensor(Dictionary<object, object> src)
        {
            var sens = new SensorInfo()
            {
                Addresses = DeviceIdParser.FromString((string)src["addr"]).ToList(),
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
            this._prRouteLiteral = new RouteLiteralParser() { ReferencedBlocks = ab };

            foreach (var r in src)
            {
                var dict = (Dictionary<object, object>)r;
                var route = _prRouteLiteral.FromString((string)dict["route"]).ToArray();

                var motor_src = (Dictionary<object, object>)extract_dict(dict, "motor");
                var sens_src = (Dictionary<object, object>)extract_dict(dict, "sensor");
                var ptr_src = (Dictionary<object, object>)extract_dict(dict, "point");
                var isolate = ((bool?)extract_dict(dict, "isolate")) ?? false;

                var block = ab.Find(b => b.Name == (string)dict["name"]);

                block.Route = route.ToList();
                block.Motor = (motor_src != null) ? ParseMotor(route, motor_src) : null;
                block.Sensor = (sens_src != null) ? ParseSensor(sens_src) : null;
                block.Switch = (ptr_src != null) ? ParsePoint(route, ptr_src) : null;
                block.IsIsolated = isolate;

                yield return block;
            }

        }
    }
}
