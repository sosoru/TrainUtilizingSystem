using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DialogConsole.Features.Base;
using Tus.TransControl.Parser;

namespace DialogConsole.Features
{    [FeatureMetadata("1", "vehicle groups")]
    [Export(typeof(IFeature))]
    class SelectVehicleGroupFeature
        :BaseFeature, IFeature
    {
        private IEnumerable<VehicleGroupSegmentOnYaml> segments;
        private Dictionary<string, VehicleGroupSegmentOnYaml[]> groups;
        private Dictionary<string, VehicleGroupSegmentOnYaml?> usedGroups;

        public void Init()
        {
            var parser = new VehicleGroupYaml();
            var yaml = parser.ParseFrom(DialogConsole.Properties.Settings.Default.VehicleGroupPath);
            this.segments = parser.ParseYamlContent(yaml);
            this.groups = this.segments.GroupBy(seg => seg.idential).ToDictionary(g => g.Key, g => g.ToArray());
            this.usedGroups = this.groups.ToDictionary(p => p.Key, p => (VehicleGroupSegmentOnYaml?) null);
        }

        public void Execute()
        {
            //show used vehicles
            foreach (var pair in this.groups)
            {
                var used = this.usedGroups[pair.Key];
                Console.WriteLine("[{0}]", pair.Key);

                foreach (var seg in pair.Value)
                {
                    Console.WriteLine("{0} {1}", (used.HasValue && used.Value.groupname == seg.groupname) ? "*" : " ",
                                      seg.groupname);
                }

                Console.WriteLine();
            }
        }
    }
}
