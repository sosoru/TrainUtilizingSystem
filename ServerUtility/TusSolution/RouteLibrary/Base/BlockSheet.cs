using System;
using System.Collections;
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

        public TimeSpan TimeWaitingSwitchChanged { get; set; }

        public BlockSheet(IEnumerable<BlockInfo> blockinfos, PacketServer server)
        {
            this.Server = server;
            this.Dispatcher = (PacketDispatcher)this.Server.GetDispatcher();

            var blocks = blockinfos.Select(i => new Block(i, this));

            this.Name = "";
            this.InnerBlocks = new ReadOnlyCollection<Block>(blocks.ToList());

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

            //this.GetDetectorEffects().ForEach(us => us.SendState());

            this.GetEffectObservable(cmd, blocks)
                .ForEach(p => p.ForEach(eff => eff.ExecuteCommand()));
        }

        public IEnumerable<UsartSetting> GetDetectorEffects()
        {
            //detector init
            //todo : move out of this class for layer inversion
            var detectors = this.InnerBlocks.Where(b => b.HasSensor).SelectMany(b => b.Detector.Devices);
            foreach (
                var taildetector in
                    detectors.GroupBy(d => d.DeviceID.InternalAddr & 0xF0)
                             .Select(g => g.OrderBy(s => s.DeviceID.InternalAddr).Last()))
            {
                var modulecount = (taildetector.DeviceID.InternalAddr & 0x0F);
                yield return taildetector.CreateSettingDevice(modulecount);
            }

        }

        public IEnumerable<IDeviceEffectorAlias>[] GetEffectObservable(CommandFactory cmd, IEnumerable<Block> blocks)
        {
            blocks.ForEach(b => b.Effect(new[] { cmd }));

            var ob = blocks
                .SelectMany(b => b.Effectors)
                   .Where(e => e.IsNeededExecution)
                 .GroupBy(e => (e is SwitchEffector) ? 0 : 1)
                 .OrderBy(g => g.Key);

            return ob.Select(g => (IEnumerable<IDeviceEffectorAlias>)g.ToArray()).ToArray();
        }

        public Block GetBlock(string p)
        {
            return this.InnerBlocks.FirstOrDefault(b => b.Name == p);
        }

        //public void InquiryStatusOfAllMotors()
        //{
        //    var devs = this.InnerBlocks
        //                        .Where(b => b.HasMotor)
        //                        .Select(b => b.info.Motor.Address)
        //                        .GroupBy(g => g.GetUniqueIdByBoard())
        //                        .Select(g => g.First());
        //    foreach (var d in devs)
        //    {
        //        var pack = PacketExtension.CreatePackedPacket(Kernel.InquiryState(d));
        //        this.Server.EnqueuePacket(pack.First());
        //    }
        //}

        public void SetUnlockedBlocksToDefault()
        {
            var blocks = this.InnerBlocks.Where(b =>
                                                    {
                                                        lock (b.lock_islocked)
                                                        {
                                                            return !b.IsLocked && (b.HasMotor || b.HasSwitch);
                                                        }
                                                    });
            foreach (var b in blocks)
            {
                if (b.HasMotor)
                {
                    b.MotorEffector.SetNoEffectMode();
                    b.MotorEffector.ExecuteCommand();
                }
                if (b.HasSwitch)
                {
                    b.SwitchEffector.SetAnyState();
                    b.SwitchEffector.ExecuteCommand();
                }
            }
        }

        public IEnumerable<Switch> AllSwitches
        {
            get
            {
                var devs = this.InnerBlocks
                                    .Where(b => b.HasSwitch)
                                    .SelectMany(b => b.SwitchEffector.Devices);
                return devs;
            }
        }

        public IEnumerable<Motor> AllMotors
        {
            get { return this.InnerBlocks.Where(b => b.HasMotor).SelectMany(b => b.MotorEffector.Devices); }
        }

        public void SyncSwitches()
        {
            this.InquiryDevices(this.AllSwitches);
        }

        public IEnumerable<IDevice<IDeviceState<IPacketDeviceData>>> AllDevices
        {
            get { var dev = this.InnerBlocks.SelectMany(d => d.Devices).Distinct(d => d.DeviceID); return dev; }
        }

        public void InquiryDevices()
        {
            this.InquiryDevices(this.AllDevices);
        }

        public void InquiryDevices(IEnumerable<Block> blocks)
        {
            var devs = blocks.SelectMany(d => d.Devices).Distinct(d => d.DeviceID);
            InquiryDevices(devs);

        }

        public void InquiryDevices(IEnumerable<IDevice<IDeviceState<IPacketDeviceData>>> devs)
        {
            var addrs = devs
                .GroupBy(d => d.DeviceID.GetUniqueIdByBoard())
                .Select(g => g.First().DeviceID);
            var kernels = addrs.Select(Kernel.InquiryState);

            //send inquiry state per board
            this.Server.EnqueueChunck(kernels.Select(k => k.ToDeviceChunck()));
        }


        public void PrepareVehicles()
        {
            // detection process succeeded
            var g = this.InnerBlocks.Where(b => b.HasMotor && b.IsDetectingTrain);





        }

    }
}
