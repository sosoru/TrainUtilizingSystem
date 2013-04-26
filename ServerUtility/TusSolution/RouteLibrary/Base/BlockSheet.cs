using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using Tus.Communication;
using Tus.Communication.Device.AvrComposed;
using IDeviceEffectorAlias = Tus.TransControl.Base.IDeviceEffector<
    Tus.Communication.IDevice<Tus.Communication.IDeviceState<Tus.Communication.IPacketDeviceData>>, Tus.TransControl.Base.DeviceInfo>;

namespace Tus.TransControl.Base
{
    public class BlockSheet
        : IEquatable<BlockSheet>
    {

        public string Name { get; set; }
        public ReadOnlyCollection<Block> InnerBlocks { get; private set; }
        public PacketServer Server { get; private set; }
        public PacketDispatcher Dispatcher { get; private set; }
        public IScheduler AssociatedScheduler { get; set; }

        public IList<Vehicle> Vehicles { get; private set; }
        public TimeSpan TimeWaitingSwitchChanged { get; set; }

        public BlockSheet(IEnumerable<BlockInfo> blockinfos, PacketServer server)
        {
            this.Server = server;
            this.Dispatcher = (PacketDispatcher)this.Server.GetDispatcher();

            var blocks = blockinfos.Select(i => new Block(i, this));

            this.Name = "";
            this.InnerBlocks = new ReadOnlyCollection<Block>(blocks.ToList());

            this.Vehicles = new List<Vehicle>();
            this.AssociatedScheduler = Scheduler.CurrentThread;
            this.TimeWaitingSwitchChanged = TimeSpan.FromTicks(1);
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

        public IEnumerable<IDeviceEffectorAlias> Effectors
        {
            get { return this.InnerBlocks.SelectMany(b => b.Effectors); }
        }

        public void Effect(CommandFactory cmd, IEnumerable<Block> blocks)
        {
            //Observable.Timer(DateTimeOffset.MinValue, this.TimeWaitingSwitchChanged, this.AssociatedScheduler)
            //    .Zip(GetEffectObservable(cmd, blocks), (l, effectors) => new { l, effectors })
            //    .Subscribe(val => val.effectors.ForEach(e => e.ExecuteCommand()));
            //
            this.GetEffectObservable(cmd, blocks)
                .ForEach(p => p.ForEach(eff => eff.ExecuteCommand()));
        }

        public IEnumerable<IDeviceEffectorAlias>[] GetEffectObservable(CommandFactory cmd, IEnumerable<Block> blocks)
        {
            blocks.ForEach(b => b.Effect(new[] { cmd }));

            var ob = blocks
                .SelectMany(b => b.Effectors)
                   .Where(e => e.IsNeededExecution)
                 .GroupBy(e => 0)//(e is SwitchEffector) ? 0 : 1)
                 .OrderBy(g => g.Key);

            return ob.Select(g => (IEnumerable<IDeviceEffectorAlias>)g.ToArray()).ToArray();
        }

        public Block GetBlock(string p)
        {
            return this.InnerBlocks.FirstOrDefault(b => b.Name == p);
        }

        public void ChangeDetectingMode()
        {
            var detectionduty = 0.5f;

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
                    var pack = PacketExtension.CreatePackedPacket(Kernel.InquiryState(dev.DeviceID));
                    this.Server.EnqueuePacket(pack.First());
                })
                .Subscribe();

        }
        public void InquiryStatusOfAllMotors()
        {
            var devs = this.InnerBlocks
                                .Where(b => b.HasMotor)
                                .Select(b => b.info.Motor.Address)
                                .GroupBy(g => g.GetUniqueIdByBoard())
                                .Select(g => g.First());
            foreach (var d in devs)
            {
                var pack = PacketExtension.CreatePackedPacket(Kernel.InquiryState(d));
                this.Server.EnqueuePacket(pack.First());
            }
        }

        public IEnumerable<Switch> AllSwitches()
        {
            var devs = this.InnerBlocks
                                .Where(b => b.HasSwitch)
                                .Select(b => b.SwitchEffector.Device);
            return devs;
        }

        public IEnumerable<IDevice<IDeviceState<IPacketDeviceData>>> AllDevices
        {
            get { var dev = this.InnerBlocks.SelectMany(d => d.Devices).Distinct(d => d.DeviceID); return dev; }
        }

        public void InquiryDevices()
        {
            this.InquiryDevices(this.AllDevices);
        }

        public void InquiryDevices(IEnumerable<IDevice<IDeviceState<IPacketDeviceData>>> devs)
        {
            var addrs = devs
                .GroupBy(d => d.DeviceID.GetUniqueIdByBoard())
                .Select(g => g.First().DeviceID);
            foreach (var d in addrs)
            {
                var pack = PacketExtension.CreatePackedPacket(Kernel.InquiryState(d));
                this.Server.EnqueuePacket(pack.First());
            }
        }


        public void PrepareVehicles()
        {
            // detection process succeeded
            var g = this.InnerBlocks.Where(b => b.HasMotor && b.IsDetectingTrain);





        }

    }
}
