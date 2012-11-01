using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.Specialized;
using System.Collections.ObjectModel;

using System.Reactive;
using System.Reactive.Linq;

using SensorLibrary;
using SensorLibrary.Packet;
using SensorLibrary.Packet.Control;
using SensorLibrary.Devices;
using SensorLibrary.Devices.TusAvrDevices;

namespace RouteLibrary.Base
{
    public class BlockSheet
        : IEquatable<BlockSheet>
    {

        public string Name { get; set; }
        public ReadOnlyCollection<Block> InnerBlocks { get; private set; }
        public PacketServer Server { get; private set; }
        public PacketDispatcher Dispatcher { get; private set; }
        public IEnumerable<Vehicle> Vehicles { get; private set; }

        public BlockSheet(IEnumerable<BlockInfo> blockinfos, PacketServer server)
        {
            this.Server = server;
            this.Dispatcher = (PacketDispatcher)this.Server.GetDispatcher();

            var blocks = blockinfos.Select(i => new Block(i, this));

            this.Name = "";
            this.InnerBlocks = new ReadOnlyCollection<Block>(blocks.ToList());
        }

        #region implementation of IEqualable
        public static bool operator ==(BlockSheet A, BlockSheet B)
        {
            if (ReferenceEquals(A, B))
                return true;
            else if ((object)A == null || (object)B == null)
                return false;
            else
                return (A.Name == B.Name);
        }
        public static bool operator !=(BlockSheet A, BlockSheet B) { return !(A == B); }

        public bool Equals(BlockSheet other)
        {
            return (this == other);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // TODO: write your implementation of Equals() here
            return this.Equals((BlockSheet)obj);
        }

        #endregion

        public IEnumerable<IDeviceEffector> Effectors
        {
            get { return this.InnerBlocks.SelectMany(b => b.Effectors); }
        }

        public void Effect(CommandInfo cmd)
        {
            this.Effect(new[] { cmd });
        }

        public void Effect(IEnumerable<CommandInfo> cmds)
        {
            Type[] order = new[] { typeof(SwitchEffector), typeof(MotorEffector), typeof(IDeviceEffector) };

            this.InnerBlocks
                .ToObservable()
                .Do(b => b.Effect(cmds)
                    .OrderBy(cmd => Array.IndexOf(order, cmd.GetType()))
                    .ForEach(cmd => cmd.ExecuteCommand()))
                //.Do(b => b.Detectors.ForEach(d => d.SendCheckCommand()))
                .Subscribe();
        }

        public Block GetBlock(string p)
        {
            return this.InnerBlocks.FirstOrDefault(b => b.Name == p);
        }

        public void ChangeDetectingMode()
        {
            var detectionduty = 0.05f;

            this.InnerBlocks.Where(b => b.HasMotor)
                .ToObservable()
                .Select(b => b.MotorEffector)
                .Do(e =>
                {
                    e.SetDetectingMode(detectionduty);
                })
                .Delay(TimeSpan.FromSeconds(1))
                .GroupBy(e => e.Device.DeviceID.GetUniqueIdByBoard())
                .Select(e => e.First().Device)
                .Do(dev =>
                {
                    var pack = DevicePacket.CreatePackedPacket(Kernel.InquiryState(dev.DeviceID));
                    this.Server.SendPacket(pack.First());
                })
                .Subscribe();

        }

        public void InquiryAllMotors()
        {
            var devs = this.InnerBlocks
                                .Where(b => b.HasMotor)
                                .Select(b => b.info.Motor.Address)
                                .GroupBy(g => g.GetUniqueIdByBoard())
                                .Select(g => g.First());
            foreach (var d in devs)
            {
                var pack = DevicePacket.CreatePackedPacket(Kernel.InquiryState(d));
                this.Server.SendPacket(pack.First());
            }
        }

        public void PrepareVehicles()
        {
            // detection process succeeded
            var g = this.InnerBlocks.Where(b => b.HasMotor && b.IsDetectingTrain)
                .Select(b => new Vehicle() { CurrentBlock = b });

            this.Vehicles = g;
        }
    }
}
