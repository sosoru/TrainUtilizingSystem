using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using DialogConsole.Features.Base;
using Tus.Communication;
using Tus.Communication.Device.AvrComposed;

namespace DialogConsole.Features
{
    [FeatureMetadata("7", "switch check")]
    [Export(typeof(IFeature))]
    class SwitchCheckFeature
        : BaseFeature, IFeature
    {
        private PointStateEnum _beforePos;
        public void Execute()
        {
            PointStateEnum pos = _beforePos == PointStateEnum.Straight ? PointStateEnum.Curve : PointStateEnum.Straight;

            Console.WriteLine(pos);
            if (pos != PointStateEnum.Any)
            {
                var devs = this.Param.UsingLayout.Sheet.AllDevices.Where(d => d is Switch).Cast<Switch>().ToArray();

                Console.WriteLine("deviceid?");
                var id = Console.ReadLine();

                try
                {
                    var devid = DeviceIdParser.FromString(id).First();
                    devs = devs.Where(dev => dev.DeviceID.ToString() == devid.ToString()).ToArray();
                    Console.WriteLine("{0} switch found", devs.Length);
                }
                catch
                {
                    Console.WriteLine("all switch changes");
                }

                devs = devs.OrderBy(dev => dev.DeviceID.GetHashCode()).ToArray();
                ChangeSwitch(pos, devs);
            }
            _beforePos = pos;
        }

        private void ChangeSwitch(PointStateEnum dir, IEnumerable<Switch> devs)
        {
            foreach (var d in devs)
            {
                d.CurrentState.Position = dir;
                d.CurrentState.DeadTime = 355;
                d.CurrentState.ChangingTime = 200;
                //System.Threading.Thread.Sleep(100);
            }

            var packets = PacketExtension.CreatePackedPacket(devs);
            foreach(var packet in packets)
                this.Param.UsingLayout.Sheet.Server.EnqueuePacket(packet);
        }

        public void Init()
        {
        }
    }
}
