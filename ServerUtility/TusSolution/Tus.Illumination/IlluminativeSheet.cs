using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Yaml.Serialization;
using SensorLibrary.Devices.TusAvrDevices;
using Tus.Communication;

namespace Tus.Illumination
{
    public class IlluminativeSheet
    {
        public IlluminativeSheet(IEnumerable<IlluminativeObject> objects, PacketServer serv)
        {
            this.Objects = objects.ToList();
            this.Server = serv;
        }

        public PacketServer Server { get; set; }
        public IList<IlluminativeObject> Objects { get; set; }

        public void SyncLedDuty()
        {
            var leds = this.Objects.SelectMany(o => o.AssociatedLuminary.Leds);
            var devices = leds.GroupBy(d => (((int) d.DeviceID.ParentPart) << 8) + (int)d.DeviceID.ModuleAddr);
            var packets = devices.SelectMany(PacketExtension.CreatePackedPacket);
            foreach (var p in packets)
                this.Server.EnqueuePacket(p);
        }
    }

    public class IlluminativeObjectFactory
    {
        public string Path { get; set; }
        public PacketServer Server { get; set; }

        public IlluminativeObjectFactory(string path, PacketServer serv)
        {
            this.Path = path;
            this.Server = serv;
        }

        public IEnumerable<Led> ParseLeds(IEnumerable<object> src)
        {
            var objs = src.Where(o => o is Dictionary<object, object>)
                .Cast<Dictionary<object, object>>();
            return objs.Select(pair => new Led()
                                       {
                                           DeviceID = DeviceIdParser.FromString((string)pair["addr"]).First(),
                                           ReceivingServer = this.Server
                                       });
        }

        public Luminary ParseLuminary(Dictionary<object, object> objs)
        {
            return new Luminary()
                   {
                       Leds = ParseLeds((IEnumerable<object>)objs["devices"]).ToList(),
                   };
        }

        public IEnumerable<IlluminativeObject> ParseIlluminativeObject(IEnumerable<object> src)
        {
            var objs = src.Where(o => o is Dictionary<object, object>)
               .Cast<Dictionary<object, object>>();

            return objs.Select(pair => new IlluminativeObject()
                                       {
                                           Name = (string)pair["name"],
                                           AssociatedLuminary = ParseLuminary((Dictionary<object, object>)pair["luminary"])
                                       });
        }

        public IEnumerable<IlluminativeObject> Create()
        {
            var ser = new YamlSerializer();
            object[] objs = ser.DeserializeFromFile(this.Path);

            return ParseIlluminativeObject((IEnumerable<object>)objs[0]);
        }
    }
}
