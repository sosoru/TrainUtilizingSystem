using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using Tus.Communication.Device.AvrComposed;

namespace Tus.TransControl.Base
{
    public class SpeedFactory
    {
        protected float InitSpeed { get; private set; }
        protected float TargetSpeed { get; private set; }
        public float Accelation { get; set; }
        private DateTime startedtime;

        public float RawSpeed
        {
            get
            {
                return this.TargetSpeed;
            }
            set
            {
                var current = this.CurrentSpeed;
                this.InitSpeed = current;
                this.TargetSpeed = value;
                this.startedtime = DateTime.Now;
            }
        }

        public float Go
        {
            get { return this.CurrentSpeed; }
        }

        public float Caution
        {
            get { return this.CurrentSpeed * 0.75f; }
        }

        public float Stop
        {
            get { return this.CurrentSpeed * 0.3f; }
        }

        public float CurrentSpeed
        {
            get
            {
                var acceldir = (this.InitSpeed < TargetSpeed) ? 1.0f : -1.0f; // 加速度の向き
                var diff = (DateTime.Now - startedtime).TotalSeconds * acceldir * Accelation;
                var current = InitSpeed + diff;

                if (acceldir > 0.0f)
                {
                    if (current < InitSpeed)
                        current = InitSpeed;
                    else if (TargetSpeed < current)
                        current = TargetSpeed;
                }
                else // if acceldir is minus
                {
                    if (current < TargetSpeed)
                        current = TargetSpeed;
                    else if (InitSpeed < current)
                        current = InitSpeed;
                }

                return (float)current;
            }
        }
    }

    public interface IRouteLockPredicator
    {
        bool ShouldLockNext(Vehicle v);
        bool ShouldReleaseBefore(Vehicle v);

    }

    public class RouteLockPredicator : IRouteLockPredicator
    {
        public bool ShouldLockNext(Vehicle v)
        {
            if (v.IsStopped || !v.AssociatedRoute.IsReserving)
                return false;

            ControlUnit waitingunit = v.AssociatedRoute.ReservedUnit.Unit;

            //IsBlockedが変化してから200ミリ秒は待機
            if (waitingunit.ControlBlock.ElaspedMilisecondsFromBlockingChanged < 200)
                return false;

            var mtr = (Motor)waitingunit.ControlBlock.Devices.First();
            var detected = v.AssociatedRoute.RouteOrder.Polar == BlockPolar.Positive
                ? mtr.CurrentMemory == MotorMemoryStateEnum.Controlling
                : mtr.CurrentMemory == MotorMemoryStateEnum.Locked;

            if (v.AssociatedRoute.RouteOrder.Polar == BlockPolar.Negative)
            {
                Console.WriteLine(mtr.CurrentMemory);
            }

            return detected;
        }

        public bool ShouldReleaseBefore(Vehicle v)
        {
            var manyunitslocked = v.AssociatedRoute.PassedUnits.Count > v.Length;

            return manyunitslocked;
        }
    }

    [DataContract]
    public class Vehicle
    {
        private static int LastVehicleID;

        static Vehicle()
        {
            LastVehicleID = 0;
        }

        public Vehicle(BlockSheet sht, RouteOrder rt)
        {
            VehicleID = LastVehicleID++;

            Sheet = sht;
            AssociatedRoute = new Route(rt);

            Length = 2;
            Accelation = 0.1f;
            Halt = new List<Halt>();
            Predicators = new List<IRouteLockPredicator>();
            Predicators.Add(new RouteLockPredicator());
        }

        [DataMember]
        public int VehicleID { get; private set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public IList<RouteOrder> AvailableRoutes { get; set; }

        [DataMember]
        public Block CurrentBlock
        {
            get
            {
                var unit = this.AssociatedRoute.CenterUnit;
                if (unit != null) return unit.ControlBlock;
                else return null;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [DataMember]
        public Route AssociatedRoute { get; set; }

        public BlockSheet Sheet { get; set; }

        [DataMember]
        public IList<Halt> Halt { get; set; }

        [IgnoreDataMember]
        private SpeedFactory speedfactry = new SpeedFactory();
        [DataMember]
        public float Speed
        {
            get
            {
                return this.speedfactry.RawSpeed;
            }
            set
            {
                this.speedfactry.RawSpeed = value;
            }
        }
        [DataMember]
        public float Accelation
        {
            get
            {
                return this.speedfactry.Accelation;
            }
            set
            {
                this.speedfactry.Accelation = value;
            }
        }

        [DataMember]
        public int Length { get; set; }

        public bool IgnoreBlockage { get; set; }

        public bool ShouldHalt
        {
            get
            {
                return Halt != null
                       && Halt.Any(bh => AssociatedRoute.HeadContainer.Unit.Blocks.Contains(bh.HaltBlock) && bh.HaltState);
            }
        }

        public bool CanHalt
        {
            get
            {
                return Halt != null
                       && Halt.Any(bh => AssociatedRoute.HeadContainer.Unit.Blocks.Contains(bh.HaltBlock));
            }
        }

        public bool IsStopped
        {
            get
            {
                return this.Speed == 0.0f || this.ShouldHalt;
            }
        }

        public IList<IRouteLockPredicator> Predicators { get; set; }

        public void Refresh()
        {
            Run(Speed);
        }

        public void ChangeRoute(RouteOrder rt)
        {
            //todo : check the routes can be changed
            Speed = 0;
            Refresh();
            var currentpos = this.CurrentBlock;
            while (AssociatedRoute.ReleaseLastUnit()) ;
            AssociatedRoute.UnReserveHead();

            rt.IsRepeatable = AssociatedRoute.RouteOrder.IsRepeatable;

            AssociatedRoute = new Route(rt);
            Run(0.0f, currentpos);
        }

        public void RunRelease()
        {
            while (AssociatedRoute.LockNextUnit()) ;
            var cmdfact = CreateBlockageReleaseCommand(() => this.speedfactry.Stop);
            Sheet.Effect(cmdfact, AssociatedRoute.RouteOrder.Blocks.ToList().Distinct());
        }

        public void RunIfIgnored(float spd)
        {
            var spdfact = this.speedfactry;

            //all alocate
            while (AssociatedRoute.LockNextUnit()) ;

            CommandFactory cmdfact = null;
            cmdfact = CreateBlockageIgnoreCommand(() => spdfact.Go);
            Sheet.Effect(cmdfact, AssociatedRoute.RouteOrder.Blocks.ToList().Distinct());
        }

        public void Run()
        {
            Run(0.5f);
        }

        public void Run(float spd, string name)
        {
            var blk = this.Sheet.GetBlock(name);
            this.Run(spd, blk);
        }

        public void Run(float spd, Block blk)
        {
            if (this.CurrentBlock != blk)
            {
                this.AssociatedRoute.AllocateTrain(blk, this.Length);
            }
            Run(spd);
        }

        public void Run(float spd)
        {
            this.Speed = spd;
            if (this.CurrentBlock == null)
            {
                var blk = this.AssociatedRoute.RouteOrder.Blocks.First();
                this.AssociatedRoute.AllocateTrain(blk, this.Length);
            }
            if (IgnoreBlockage)
            {
                RunIfIgnored(Speed);
                return;
            }
            if (ReleaseBlockage)
            {
                RunRelease();
                return;
            }

            if (ShouldHalt)
            {
                Speed = 0.0f;
            }

            // todo : halts support
            //todo : steady support and test with or without sensors 
            // verified i'm not halted and the next unit is not blocked by other vehicles
            if (this.Predicators.Any(l => l.ShouldLockNext(this)))
            {
                try
                {
                    this.AssociatedRoute.LockNextUnit();
                }
                catch (InvalidOperationException) { }
                Console.WriteLine("vehicle {0} moved : {1}", Name, CurrentBlock.Name);
            }

            if (this.Predicators.All(l => l.ShouldReleaseBefore(this)))
            {
                try
                {
                    this.AssociatedRoute.ReleaseLastUnit();
                }
                catch (InvalidOperationException) { }
            }

            CommandFactory cmdfactory = null;
            var spdfactory = this.speedfactry;
            var lastlockedblocks = AssociatedRoute.LockedBlocks.ToArray();

            if (this.IsStopped)
            {
                this.AssociatedRoute.UnReserveHead();
                cmdfactory = CanHalt
                    ? CreateHaltCommand(spdfactory)
                    : CreateZeroCommand(spdfactory);
            }
            else
            {
                int distance = 1; // stops
                //neighbor check
                try
                {
                    this.AssociatedRoute.UnReserveHead();
                    this.AssociatedRoute.ReserveHead();
                    distance = AssociatedRoute.ReservedUnit.GetDistanceOfBlockedUnit(4);
                    if (distance == 2)
                    {
                        cmdfactory = Create1stCommand(spdfactory);
                    }
                    else if (distance == 3)
                    {
                        cmdfactory = Create2ndCommand(spdfactory);
                    }
                    else
                    {
                        cmdfactory = CreateNthCommand(spdfactory);
                    }
                }
                catch (InvalidOperationException)
                {
                    cmdfactory = CanHalt
                                    ? CreateHaltCommand(spdfactory)
                                    : CreateZeroCommand(spdfactory);
                }
            }
            Sheet.Effect(cmdfactory, AssociatedRoute.LockedBlocks);
        }

        #region "CreateCommandMethods"

        private CommandFactory CreateWithWaitingCommand(Func<float> cntspdFactory, Func<float> waitspdFactory)
        {
            var func = new Func<Block, CommandInfo>
                (blk =>
                     {
                         var cmd = new CommandInfo
                                       {
                                           Route = AssociatedRoute,
                                       };
                         ControlUnit[] lockedunits =
                             AssociatedRoute.LockedUnits.Except(new[] { AssociatedRoute.ReservedUnit.Unit }).ToArray();
                         Block waitblk = AssociatedRoute.ReservedUnit.Unit.ControlBlock;
                         Block cntblk = (cmd.Route.RouteOrder.Polar == BlockPolar.Positive)
                                             ? lockedunits.First().ControlBlock
                                             : lockedunits.Last().ControlBlock;

                         if (blk == cntblk)
                         // lockedunits[lockedunits.Length - 2].ControlBlock)
                         {
                             cmd.MotorMode = MotorMemoryStateEnum.Controlling;
                             cmd.Speed = cntspdFactory();
                         }
                         else if (blk == waitblk)
                         {
                             cmd.MotorMode = MotorMemoryStateEnum.Waiting;
                             cmd.Speed = waitspdFactory();
                         }
                         else if (lockedunits.Any(cntrt => cntrt.ControlBlock == blk))
                         {
                             cmd.MotorMode = MotorMemoryStateEnum.Locked;
                             cmd.Speed = 0.0f;
                         }
                         else
                         {
                             cmd.MotorMode = MotorMemoryStateEnum.NoEffect;
                             cmd.Speed = 0.0f;
                         }
                         return cmd;
                     });
            return new CommandFactory { CreateCommand = func };
        }

        private CommandFactory CreateControlCommand(Func<float> cntspdFactory)
        {
            var func = new Func<Block, CommandInfo>
                (blk =>
                     {
                         var cmd = new CommandInfo
                                       {
                                           Route = AssociatedRoute,
                                       };
                         ControlUnit[] lockedunits = AssociatedRoute.LockedUnits.ToArray();
                         Block cntblk = (cmd.Route.RouteOrder.Polar == BlockPolar.Positive)
                                           ? lockedunits.First().ControlBlock
                                           : lockedunits.Last().ControlBlock;

                         if (blk == cntblk)
                         {
                             cmd.MotorMode = MotorMemoryStateEnum.Controlling;
                             cmd.Speed = cntspdFactory();
                         }
                         else if (lockedunits.Any(u => u.ControlBlock.Name == blk.Name))
                         {
                             cmd.MotorMode = MotorMemoryStateEnum.Locked;
                             cmd.Speed = 0.0f;
                         }
                         else
                         {
                             cmd.MotorMode = MotorMemoryStateEnum.NoEffect;
                             cmd.Speed = 0.0f;
                         }
                         return cmd;
                     });
            return new CommandFactory { CreateCommand = func };
        }

        private CommandFactory CreateBlockageIgnoreCommand(Func<float> cntspdfact)
        {
            var fun = new Func<Block, CommandInfo>
                (blk =>
                     {
                         var cmd = new CommandInfo
                                       {
                                           Route = AssociatedRoute,
                                           Speed = cntspdfact(),
                                           MotorMode =
                                               MotorMemoryStateEnum.Controlling
                                       };

                         return cmd;
                     });
            return new CommandFactory { CreateCommand = fun };
        }

        private CommandFactory CreateBlockageReleaseCommand(Func<float> cntspdfact)
        {
            var fun = new Func<Block, CommandInfo>
            (blk =>
                 {
                     var cmd = new CommandInfo
                                   {
                                       Route = AssociatedRoute,
                                       Speed = cntspdfact(),
                                       MotorMode = MotorMemoryStateEnum.Locked
                                   };
                     return cmd;
                 }
            );
            return new CommandFactory { CreateCommand = fun };
        }

        private CommandFactory CreateNthCommand(SpeedFactory spdfactory)
        {
            return CreateWithWaitingCommand(() => spdfactory.Go, () => spdfactory.Go);
        }

        private CommandFactory Create2ndCommand(SpeedFactory spdfactory)
        {
            return CreateWithWaitingCommand(() => spdfactory.Go, () => spdfactory.Caution);
        }

        private CommandFactory Create1stCommand(SpeedFactory spdfactory)
        {
            return CreateWithWaitingCommand(() => spdfactory.Caution, () => spdfactory.Stop);
        }

        private CommandFactory CreateZeroCommand(SpeedFactory spdfactory)
        {
            return CreateControlCommand(() => spdfactory.Stop);
        }

        private CommandFactory CreateHaltCommand(SpeedFactory spdfactory)
        {
            return CreateControlCommand(() => spdfactory.Caution);
        }

        #endregion

        #region "Equal Methods"

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
            return VehicleID.Equals(((Vehicle)obj).VehicleID);
        }

        public static bool operator ==(Vehicle A, Vehicle B)
        {
            return A.Equals(B);
        }

        public static bool operator !=(Vehicle A, Vehicle B)
        {
            return !(A.Equals(B));
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return VehicleID.GetHashCode();
        }

        #endregion

        public bool ReleaseBlockage { get; set; }
    }
}