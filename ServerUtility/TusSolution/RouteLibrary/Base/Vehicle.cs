using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Tus.Communication.Device.AvrComposed;

namespace Tus.TransControl.Base
{
    public class SpeedFactory
    {
        private DateTime startedtime;
        protected float InitSpeed { get; private set; }
        protected float TargetSpeed { get; private set; }
        public float Accelation { get; set; }

        public float RawSpeed
        {
            get { return TargetSpeed; }
            set
            {
                float current = CurrentSpeed;
                InitSpeed = current;
                TargetSpeed = value;
                startedtime = DateTime.Now;
            }
        }

        public float Go
        {
            get { return CurrentSpeed; }
        }

        public float Caution
        {
            get { return CurrentSpeed; }
        }

        public float Stop
        { // 0.1f だとWaitingUnitに電流が流れて誤検知する？
            get { return 0f; }
        }

        public float CurrentSpeed
        {
            get
            {
                float acceldir = (InitSpeed < TargetSpeed) ? 1.0f : -1.0f; // 加速度の向き
                double diff = (DateTime.Now - startedtime).TotalSeconds * acceldir * Accelation;
                double current = InitSpeed + diff;

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
        private DateTime _lastchnaged = DateTime.MinValue;

        // REVのときにControlBlockを進めないこと
        public bool ShouldLockNext(Vehicle v)
        {
            if (v.IsStopped || !v.AssociatedRoute.IsReserving)
                return false;

            ControlUnit waitingunit = v.AssociatedRoute.ReservedUnit.Unit;

            var mtr = (Motor)waitingunit.ControlBlock.Devices.First();
            // 最後に状態をLockした日時に気をつけること
            bool isearly = (DateTime.Now - _lastchnaged).TotalSeconds < 1.0f;

            var memtobechanged = (v.AssociatedRoute.RouteOrder.Polar == BlockPolar.Positive)
                ? MotorMemoryStateEnum.Locked
                : MotorMemoryStateEnum.Controlling;
            bool detected = !isearly // デッドタイムを越えた
                // Waitingを命令しているけれども，受け取った状態は変更済み
                && (mtr.CurrentMemory == MotorMemoryStateEnum.Waiting && mtr.ReceivedMemory == memtobechanged)
                && mtr.ReceivedMemory != MotorMemoryStateEnum.Unknown;

            //v.AssociatedRoute.RouteOrder.Polar == BlockPolar.Positive
            //                ? mtr.ReceivedMemory== MotorMemoryStateEnum.Locked
            //                : mtr.ReceivedMemory == MotorMemoryStateEnum.Controlling;

            if (detected)
                this._lastchnaged = DateTime.Now;

            return detected;
        }

        public bool ShouldReleaseBefore(Vehicle v)
        {
            bool manyunitslocked = v.AssociatedRoute.PassedUnits.Count > v.Length;

            return manyunitslocked;
        }
    }

    [DataContract]
    public class Vehicle
    {
        private static int LastVehicleID;
        [IgnoreDataMember]
        private readonly SpeedFactory speedfactry = new SpeedFactory();

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
            Accelation = 1.0f;
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
                ControlUnit unit = AssociatedRoute.CenterUnit;
                if (unit != null) return unit.ControlBlock;
                else return null;
            }
            set { throw new NotImplementedException(); }
        }

        [DataMember]
        public Route AssociatedRoute { get; set; }

        public BlockSheet Sheet { get; set; }

        [DataMember]
        public IList<Halt> Halt { get; set; }

        [DataMember]
        public float Speed
        {
            get { return speedfactry.RawSpeed; }
            set { speedfactry.RawSpeed = value; }
        }

        [DataMember]
        public float Accelation
        {
            get { return speedfactry.Accelation; }
            set { speedfactry.Accelation = value; }
        }

        [DataMember]
        public int Length { get; set; }

        public bool IgnoreBlockage { get; set; }

        public bool ShouldHalt
        {
            get
            {
                return Halt != null
                       &&
                       Halt.Any(bh => AssociatedRoute.HeadContainer.Unit.Blocks.Contains(bh.HaltBlock) && bh.HaltState);
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
            get { return Speed == 0.0f || ShouldHalt; }
        }

        [DataMember]
        // length == 2の時に対応
        public IEnumerable<string> ReallocatableBlockNames
        {
            get
            {
                var order = this.AssociatedRoute.RouteOrder;
                ControlUnit[] myunits = AssociatedRoute.LockedUnits.ToArray();
                ControlUnit[] otherslockedunits =
                    order.Units.Where(u => u.IsBlocked).Except(myunits).ToArray();
                return
                    order.Units.Except(otherslockedunits)
                           .Except(
                               otherslockedunits.Select(AssociatedRoute.RouteOrder.CreateContainer)
                                                .Select(uc => uc.Next.Unit)) //後ろの余裕が1しかとれないUnitはAllocateできない
                                                .Select(u => (u.ControlBlock == null) ? "!control" : u.ControlBlock.Name);
            }
            set { throw new NotImplementedException(); }
        }

        [DataMember]
        public int Distance
        {
            get
            {
                if (AssociatedRoute != null && AssociatedRoute.HeadContainer != null)
                {
                    if (AssociatedRoute.IsReserving)
                        return AssociatedRoute.ReservedUnit.GetDistanceOfBlockedUnit(2) + 1; //resrevedunitはallocate済み，reserved以降をチェック
                    else
                        return AssociatedRoute.HeadContainer.GetDistanceOfBlockedUnit(3);
                }
                else
                    return -1; // not prepared
            }
            set { throw new NotImplementedException(); }
        }

        public IList<IRouteLockPredicator> Predicators { get; set; }
        public bool ReleaseBlockage { get; set; }

        public void Refresh()
        {
            Run(Speed);
        }

        public void ChangeRoute(string routename, string blockname)
        {
            RouteOrder routeorder = AvailableRoutes.FirstOrDefault(rt => rt.Name == routename);
            if (routeorder == null)
                throw new InvalidOperationException("route order is not found");

            ControlUnit blockpos = routeorder.Units.FirstOrDefault(ut => ut.ControlBlock.Name == blockname);
            if (blockpos == null)
                throw new InvalidOperationException("blockname is not found");
    
            var newrt = new Route(routeorder);
            if (!CanLockRoute(newrt, blockpos.ControlBlock))
                throw new InvalidOperationException("full block");

            var ignored = this.AssociatedRoute.LockedUnits.Count() > 3;

            Speed = 0;
            Refresh();
            while (AssociatedRoute.ReleaseLastUnit()) ;
            AssociatedRoute.UnReserveHead();

            routeorder.IsRepeatable = AssociatedRoute.RouteOrder.IsRepeatable;

            AssociatedRoute = newrt;

            if (ignored)
            {
                this.RunIfIgnored(0.0f);
            }
            else
            {
                Run(0.0f, blockpos.ControlBlock);

            }
        }

        public void RunRelease()
        {
            while (AssociatedRoute.LockNextUnit()) ;
            CommandFactory cmdfact = CreateBlockageReleaseCommand(() => speedfactry.Stop);
            Sheet.Effect(cmdfact, AssociatedRoute.RouteOrder.Blocks.ToList().Distinct());
        }

        public void RunIfIgnored(float spd)
        {
            SpeedFactory spdfact = speedfactry;

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
            Block blk = Sheet.GetBlock(name);
            Run(spd, blk);
        }

        public void Run(float spd, Block blk)
        {
            if (CurrentBlock != blk)
            {
                AssociatedRoute.AllocateTrain(blk, Length);
            }
            Run(spd);
        }

        public void Run(float spd)
        {
            Speed = spd;
            if (CurrentBlock == null)
            {
                Block blk = AssociatedRoute.RouteOrder.Blocks.First();
                AssociatedRoute.AllocateTrain(blk, Length);
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
            if (Predicators.Any(l => l.ShouldLockNext(this)))
            {
                if (AssociatedRoute.LockNextUnit())
                {
                    Console.WriteLine("vehicle {0} moved : {1}", Name, CurrentBlock.Name);
                }
            }

            var tail = AssociatedRoute.LockedUnits.First();
            if (Predicators.All(l => l.ShouldReleaseBefore(this)))
            {
                AssociatedRoute.ReleaseLastUnit();
                Console.WriteLine("vehicle {0} leaved : {1}", Name, tail.ControlBlock.Name);
            }

            CommandFactory cmdfactory = null;
            SpeedFactory spdfactory = speedfactry;
            Block[] lastlockedblocks = AssociatedRoute.LockedBlocks.ToArray();

            if (IsStopped)
            {
                if (AssociatedRoute.IsReserving)
                    AssociatedRoute.UnReserveHead();

                cmdfactory = CanHalt
                                 ? CreateHaltCommand(spdfactory)
                                 : CreateZeroCommand(spdfactory);
            }
            else
            {
                //neighbor check
                if (AssociatedRoute.IsReserving)
                    AssociatedRoute.UnReserveHead();
                int distance = AssociatedRoute.HeadContainer.GetDistanceOfBlockedUnit(3);
                if (AssociatedRoute.ReserveHead())
                {
                    if (distance <= 1)
                    {
                        cmdfactory = Create1stCommand(spdfactory);
                    }
                    else if (distance == 2)
                    {
                        cmdfactory = Create2ndCommand(spdfactory);
                    }
                    else
                    {
                        cmdfactory = CreateNthCommand(spdfactory);
                    }
                }
                else
                {
                    cmdfactory = CanHalt
                                     ? CreateHaltCommand(spdfactory)
                                     : CreateZeroCommand(spdfactory);
                }
            }
            Sheet.Effect(cmdfactory, AssociatedRoute.LockedBlocks.Concat(lastlockedblocks).Distinct(b => b.Name));
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

        public bool CanLockRoute(Block blk)
        {
            return CanLockRoute(this.AssociatedRoute, blk);
        }

        public bool CanLockRoute(Route rt, Block blk)
        {
            try
            {
                AssociatedRoute.AllocateTrain(blk, Length);
                return true;
            }
            catch (InvalidOperationException ex)
            {
                return false;
            }
            finally
            {
                while (AssociatedRoute.ReleaseLastUnit()) ;
            }
        }
    }
}