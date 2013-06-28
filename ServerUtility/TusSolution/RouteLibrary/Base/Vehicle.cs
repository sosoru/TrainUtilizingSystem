using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Tus.Communication.Device.AvrComposed;

namespace Tus.TransControl.Base
{
    public class SpeedFactory
    {
        public float RawSpeed { get; set; }

        public float Go
        {
            get { return RawSpeed; }
        }

        public float Caution
        {
            get { return RawSpeed * 1.0f; }
        }

        public float Stop
        {
            get { return RawSpeed * 0f; }
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
            var detected = waitingunit.ControlBlock.IsMotorDetectingTrain;
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

            Length = 1;
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

        [DataMember]
        public float Speed { get; set; }

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

            rt.IsRepeatable = AssociatedRoute.RouteOrder.IsRepeatable;

            AssociatedRoute = new Route(rt);
            Run(0.0f, currentpos);
        }

        public void RunIfIgnored(float spd)
        {
            var spdfact = new SpeedFactory { RawSpeed = spd };

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

            // todo : halts support
            //todo : steady support and test with or without sensors 
            // verified i'm not halted and the next unit is not blocked by other vehicles
            if (this.Predicators.Any(l => l.ShouldLockNext(this)))
            {
                this.AssociatedRoute.LockNextUnit();
                Console.WriteLine("vehicle {0} moved : {1}", Name, CurrentBlock.Name);
            }

            if (ShouldHalt)
            {
                Speed = 0.0f;
            }

            if (this.IsStopped)
            {
                this.AssociatedRoute.UnReserveHead();
            }
            else
                this.AssociatedRoute.ReserveHead();

            CommandFactory cmdfactory = null;
            var spdfactory = new SpeedFactory { RawSpeed = spd };

            Block[] lastlockedblocks = AssociatedRoute.LockedBlocks.ToArray();
            var distance = AssociatedRoute.HeadContainer.GetDistanceOfBlockedUnit(3);

            if (this.Predicators.All(l => l.ShouldReleaseBefore(this)))
            {
                this.AssociatedRoute.ReleaseLastUnit();
            }

            if (this.IsStopped || distance == 1)
            {
                cmdfactory = CanHalt
                                 ? CreateHaltCommand(spdfactory)
                                 : CreateZeroCommand(spdfactory);
            }
            else if (distance == 2)
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

            Sheet.Effect(cmdfactory, AssociatedRoute.LockedBlocks.Concat(lastlockedblocks).Distinct());
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
                         ControlUnit[] lockedunits = AssociatedRoute.LockedUnits.ToArray();
                         Block cntblk = (cmd.Route.RouteOrder.Polar == BlockPolar.Positive)
                                            ? lockedunits.First().ControlBlock
                                            : lockedunits.Last().ControlBlock;
                         Block waitblk = AssociatedRoute.ReservedUnit.Unit.ControlBlock;

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
    }
}