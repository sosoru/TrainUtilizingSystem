﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using Tus.Communication;
using Tus.Communication.Device.AvrComposed;
using IDeviceEffectorAlias = Tus.TransControl.Base.IDeviceEffector<
    Tus.Communication.IDevice<Tus.Communication.IDeviceState<Tus.Communication.IPacketDeviceData>>, Tus.TransControl.Base.DeviceInfo>;

namespace Tus.TransControl.Base
{
    public class CommandInfo
    {
        public Route Route { get; set; }
        public MotorMemoryStateEnum MotorMode { get; set; }
        public float Speed { get; set; }
        public bool AnyToDefault { get; set; }

    }

    public class CommandFactory
    {
        public Func<Block, CommandInfo> CreateCommand { get; set; }
    }

    public enum BlockPolar
    {
        Positive,
        Negative,
        Any,
    }

    public class SensorDetector
    {
        public Block ParentBlock { get; private set; }
        public SensorInfo Info { get; private set; }
        private List<UsartSensor> devices { get; set; }
        public ReadOnlyCollection<UsartSensor> Devices { get { return new ReadOnlyCollection<UsartSensor>(this.devices); } }

        public SensorDetector() { }

        public SensorDetector(SensorInfo info, Block block)
        {
            this.ParentBlock = block;
            this.Info = info;

            var devs = info.Addresses.Select(id => new UsartSensor() { DeviceID = id, ReceivingServer = this.ParentBlock.Sheet.Server }).ToList();
            devs.ForEach(d => d.Observe(block.Sheet.Dispatcher));
            this.devices = devs;
        }

        public virtual bool IsDetected
        {
            get { return this.devices.Any(d => d.IsDetected); }
        }
    }

    /// <summary>
    /// 経路を構成するブロック．通常，1or0個のEffectorもしくはisolatorを所持．これをつなぎ合わせてRouteを形成
    /// 現実の閉塞に相当するのはControllingUnit
    /// </summary>
    [DataContract]
    public class Block
    {
        public BlockInfo info { get; private set; }

        [DataMember]
        public virtual string Name { get; private set; }
        public BlockSheet Sheet { get; private set; }

        public IList<IDeviceEffectorAlias> Effectors { get; set; }
        public SensorDetector Detector { get; set; }

        [DataMember(IsRequired = false)]
        public IEnumerable<IDevice<IDeviceState<IPacketDeviceData>>> Devices
        {
            get
            {
                if (this.Effectors == null) return new IDevice<IDeviceState<IPacketDeviceData>>[] { };

                var dev = this.Effectors.SelectMany(e => e.Devices);
                if (this.Detector != null)
                    dev = dev.Concat(this.Detector.Devices);
                return dev.Distinct(d => d.DeviceID);
            }
        }

        public Block() // for test purpose
        {
        }

        public Block(BlockInfo info, BlockSheet sheet)
        {
            this.info = info;
            this.Name = info.Name;
            this.Sheet = sheet;

            var effs = new List<IDeviceEffectorAlias>();
            if (info.Motor != null)
                effs.Add(new MotorEffector(info.Motor, this));

            if (info.Switch != null)
                effs.Add(new SwitchEffector(info.Switch, this));

            this.Effectors = new ReadOnlyCollection<IDeviceEffectorAlias>(effs);

            if (info.Sensor != null)
                this.Detector = new SensorDetector(info.Sensor, this);

            this.IsIsolated = info.IsIsolated;

        }

        public MotorEffector MotorEffector
        {
            get
            {
                return this.Effectors.Where(e => e is MotorEffector)
                    .Cast<MotorEffector>()
                    .First();
            }
        }

        public SwitchEffector SwitchEffector
        {
            get
            {
                return this.Effectors.Where(e => e is SwitchEffector)
                    .Cast<SwitchEffector>()
                    .First();
            }
        }

        public IEnumerable<IDeviceEffectorAlias> Effect(IEnumerable<CommandFactory> infos)
        {
            var efs = this.Effectors.ToArray();

            infos.ForEach(info => efs.ForEach(e => e.ApplyCommand(info)));

            return efs;
        }

        #region implementation of IEqualable
        public static bool operator ==(Block A, Block B)
        {
            if (ReferenceEquals(A, B))
                return true;
            else if ((object)A == null || (object)B == null)
                return false;
            else
                return (A.Name == B.Name) && (A.Sheet.Name == B.Sheet.Name);
        }
        public static bool operator !=(Block A, Block B) { return !(A == B); }

        public bool Equals(Block other)
        {
            return (this == other);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() ^ this.Sheet.Name.GetHashCode();
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
            return this.Equals((Block)obj);
        }

        #endregion

        private IList<Block> neighbors_;
        public IEnumerable<Block> Neighbors
        {
            get
            {
                if (neighbors_ == null)
                {
                    neighbors_ = new List<Block>();

                    var neighbornames = this.info.Route.SelectMany(b => new[] { b.From, b.To })
                                                        .Select(b => b.Name)
                                                        .Distinct();
                    neighbornames.Select(s => this.Sheet.GetBlock(s))
                        .ForEach(neighbors_.Add);

                }
                return neighbors_;
            }
        }

        public bool HasMotor
        {
            get
            {
                return this.info.Motor != null;
            }
        }

        public bool HasSensor
        {
            get { return this.info.Sensor != null; }
        }

        public bool HasSwitch
        {
            get { return this.info.Switch != null; }
        }

        public bool IsIsolated { get; private set; }

        public bool IsDetectingTrain
        {
            get
            {
                //if (!this.HasSensor)
                //{
                //    throw new InvalidOperationException("this block is not allocated a sensor module");
                //}

                if (this.Detector == null)
                    return false;
                else
                    return this.Detector.IsDetected;
            }
        }

        //public bool IsMotorDetectingTrain
        //{
        //    get
        //    {
        //        if (this.HasMotor)
        //        {
        //            var dev = this.MotorEffector.Devices.First();
        //            return dev.CurrentMemory == MotorMemoryStateEnum.Controlling ||
        //                   dev.CurrentMemory == MotorMemoryStateEnum.Locked;
        //                   //dev.IsDetected;
        //        }
        //        else
        //            return false;
        //    }
        //}

        //public bool IsLocked
        //{
        //    get
        //    {
        //        if (!this.IsHaltable)
        //        {
        //            throw new InvalidOperationException("IsLocked property requires Haltable state");
        //        }

        //        return this.MotorEffector.Device.IsDetected;
        //        //return this.Detector.IsDetected;
        //    }
        //}

        public override string ToString()
        {
            var str = string.Format("({0}) ", this.Name);
            if (this.IsLocked)
                str += "|Locked|";

            if (this.HasMotor)
                str += this.MotorEffector.Devices.Aggregate("", (ac, dev) => ac + dev.ToString() + " " + dev.ReceivedMemory);

            if (this.HasSwitch)
                str += this.SwitchEffector.Devices.Aggregate("", (ac, dev) => ac + dev.ToString());

            if (this.HasSensor)
                str += this.Detector.Devices.Aggregate("", (s, d) => s + d.ToString());

            return str;
        }

        private Stopwatch lockingWatch = new Stopwatch();
        private bool isLocked;
        public object lock_islocked = new object();
        public bool IsLocked
        {
            get { return this.isLocked; }
            set
            {
                if (this.isLocked != value)
                {
                    //lockingWatch.Restart();
                }
                this.isLocked = value;
            }
        }

        //public long ElaspedMilisecondsFromBlockingChanged
        //{
        //    get
        //    {
        //        return this.lockingWatch.ElapsedMilliseconds;
        //    }
        //}

        public BlockSendingObject ToSendingObject()
        {
            return
                new BlockSendingObject()
                {
                    Name = this.Name,
                    IsLocked = this.isLocked,
                };
        }
    }

    public class BlockSendingObject
    {
        public string Name { get; set; }
        public bool IsLocked { get; set; }
    }
}
