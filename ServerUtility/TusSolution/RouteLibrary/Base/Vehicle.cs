using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Tus.Communication.Device.AvrComposed;
using Tus.Diagnostics;

namespace Tus.TransControl.Base
{
    public class SpeedFactory
    {
        private DateTime startedtime;

        public float InitSpeed { get;  set; }
        protected float TargetSpeed { get; private set; }
        public float Accelation { get; set; }

        public SpeedFactory()
        {
            TargetSpeed = InitSpeed;
        }

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

        public float Go { get { return CurrentSpeed; } }
        public float Caution { get { return CurrentSpeed; } }
        public float Stop { get { return CurrentSpeed; } }

        public float CurrentSpeed
        {
            get
            {
                var target = this.TargetSpeed;

                float acceldir = (InitSpeed < target) ? 1.0f : -1.0f; // 加速度の向き
                double diff = (DateTime.Now - startedtime).TotalSeconds * acceldir * Accelation * 0.5f;
                double current = InitSpeed + diff;


                if (acceldir > 0.0f)
                {
                    if (current < InitSpeed)
                        current = InitSpeed;
                    else if (target < current)
                        current = target;
                }
                else // if acceldir is minus
                {
                    if (current < target)
                        current = target;
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
            bool detected = //!isearly // デッドタイムを越えた
                // Waitingを命令しているけれども，受け取った状態は変更済み
                //mtr.CurrentMemory == MotorMemoryStateEnum.Waiting &&
                 mtr.ReceivedMemory == memtobechanged
                && mtr.ReceivedMemory != MotorMemoryStateEnum.Unknown;

            //v.AssociatedRoute.RouteOrder.Polar == BlockPolar.Positive
            //                ? mtr.ReceivedMemory== MotorMemoryStateEnum.Locked
            //                : mtr.ReceivedMemory == MotorMemoryStateEnum.Controlling;

            if (detected)
                this._lastchnaged = DateTime.Now;

            //Logger.WriteLineAsTransInfo("detecting {0} {1} {2}", detected, mtr.CurrentMemory.ToString(), mtr.ReceivedMemory.ToString());
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

        [IgnoreDataMember] private readonly SpeedFactory stopSpeedfactory = new SpeedFactory();

        [DataMember]
        public float StopThreshold { get; set; }

        static Vehicle()
        {
            LastVehicleID = 0;
        }

        public Vehicle() // for test method
        {
        }

        public Vehicle(BlockSheet sht, RouteOrder rt)
        {
            VehicleID = LastVehicleID++;

            Sheet = sht;
            AssociatedRoute = new Route(rt);

            Length = 2;
            Accelation = 1.0f;
            Predicators = new List<IRouteLockPredicator>();
            Predicators.Add(new RouteLockPredicator());

            Halt = this.AssociatedRoute.RouteOrder.Blocks.Where(b => b.info.Haltable).Select(b => new Halt() { HaltBlock = b, Method = SensingMethod.None,})
                .ToList();
        }

        [DataMember]
        public int VehicleID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ShownName { get; set; }

        [DataMember]
        public IList<RouteOrder> AvailableRoutes { get; set; }

        [DataMember]
        public virtual Block CurrentBlock
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
            set { speedfactry.RawSpeed = value;
              stopSpeedfactory.RawSpeed = StopThreshold;
            }
<<<<<<< HEAD
=======
        }

        [DataMember]
        public virtual float CurrentSpeed
        {
            get
            {
                if (Distance < 0) return 0.0f; // distance property is not prepared
                else if (Distance == 1) return stopSpeedfactory.CurrentSpeed;
                else return speedfactry.CurrentSpeed;
            }
            set { throw new NotImplementedException(); }
>>>>>>> c25b11619efab8dcba4bcf1bb07df7dc075f702d
        }

        [DataMember]
        public float Accelation
        {
            get { return speedfactry.Accelation; }
<<<<<<< HEAD
            set { speedfactry.Accelation = value;
            stopSpeedfactory.Accelation = value * 2.0f;
=======
            set
            {
                if (speedfactry != null) speedfactry.Accelation = value;
                if (stopSpeedfactory != null) stopSpeedfactory.Accelation = value * 2.0f;
>>>>>>> c25b11619efab8dcba4bcf1bb07df7dc075f702d
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
            get { return Speed <= StopThreshold || ShouldHalt; }
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
        public virtual int Distance
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
            if (!this.IsHalted) Run(Speed);
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

            var beforespd = Speed;
            Speed = 0;
            Refresh();
            while (AssociatedRoute.ReleaseLastUnit()) ;
            AssociatedRoute.UnReserveHead();

            routeorder.IsRepeatable = AssociatedRoute.RouteOrder.IsRepeatable;

            AssociatedRoute = newrt;

            if (ignored)
            {
                this.RunIfIgnored(beforespd);
            }
            else
            {
                Run(beforespd, blockpos.ControlBlock);

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

        //閉塞遷移を行っている状態かどうかを推定
        public bool EstimateVehicleMoving()
        {
            //閉塞を作っていない状態の時は抜ける
            if (!this.AssociatedRoute.IsReserving)
            {
                return false;
            }

            ControlUnit[] lockedunits =
                AssociatedRoute.LockedUnits.Except(new[] { AssociatedRoute.ReservedUnit.Unit, }).ToArray();
            Block waitblk = AssociatedRoute.ReservedUnit.Unit.ControlBlock;
            Block cntblk = (this.AssociatedRoute.RouteOrder.Polar == BlockPolar.Positive)
                               ? lockedunits.First().ControlBlock
                               : lockedunits.Last().ControlBlock;

            var statetrans = new Func<MotorMemoryStateEnum, string>((s) =>
                    (s == MotorMemoryStateEnum.Waiting) ? "W"
                    : (s == MotorMemoryStateEnum.Controlling) ? "C"
                    : (s == MotorMemoryStateEnum.Locked) ? "L"
                    : (s == MotorMemoryStateEnum.NoEffect) ? "N"
                    : (s == MotorMemoryStateEnum.Unknown) ? "U"
                    : "_");
            Logger.WriteLineAsTransInfo("current state : {0}",
                                        statetrans(waitblk.MotorEffector.Devices.First().CurrentMemory) +
                                        new string(
                                            lockedunits.Select(
                                                u => u.ControlBlock.MotorEffector.Devices.First().CurrentMemory).SelectMany(statetrans)
                                                       .ToArray()));
            Logger.WriteLineAsTransInfo("received state : {0}", statetrans(waitblk.MotorEffector.Devices.First().ReceivedMemory)
                                                                +
                                                                new string(lockedunits.Select(u => u.ControlBlock.MotorEffector.Devices.First().ReceivedMemory)
                                                                                       .SelectMany(statetrans).ToArray()));

            var waitblkChanged = waitblk.MotorEffector.Devices.First().ReceivedMemory != MotorMemoryStateEnum.Waiting;
            //var cntblkChanged = cntblk.MotorEffector.Devices.First().ReceivedMemory != MotorMemoryStateEnum.Controlling;
            var lockedunitsChangedCount =
                lockedunits.Count(u => Enumerable.First<Motor>(u.ControlBlock.MotorEffector.Devices).ReceivedMemory != MotorMemoryStateEnum.Locked);

            //WaitingBlockが変わっていないけれども，lockedunitsが変わっている場合を閉塞遷移状態と判定
            return lockedunitsChangedCount >= 2 && !waitblkChanged; // 

        }

        public void RefreshStoppingSpeedBehavior()
        {
            //TODO: SpeedFactory のコンストラクタで初期化した方が良い
            this.stopSpeedfactory.Accelation = this.Accelation * 2.0f;
            this.stopSpeedfactory.RawSpeed = StopThreshold;
            this.stopSpeedfactory.InitSpeed = this.speedfactry.CurrentSpeed;
            Logger.WriteLineAsTransInfo("{0}({1}) : stop init={2} accr={3}",this.Name, this.ShownName, this.stopSpeedfactory.InitSpeed,
                                        this.stopSpeedfactory.Accelation);
        }

        public void RefreshRunningSpeedBehavior()
        {
            this.speedfactry.RawSpeed = this.speedfactry.RawSpeed;
            this.speedfactry.InitSpeed = this.stopSpeedfactory.CurrentSpeed;
            Logger.WriteLineAsTransInfo("{0}({1}) : run init={2} accr={3}",  this.Name, this.ShownName, this.speedfactry.InitSpeed,
                                        this.speedfactry.Accelation);
        }

        private bool isStoppedLast = true;
        public void Run(float spd)
        {
            Speed = spd;
            if (CurrentBlock == null)
            {
                Block blk = AssociatedRoute.RouteOrder.Blocks.First();
                AssociatedRoute.AllocateTrain(blk, Length);
            }
            if (this.IsHalted)
            {
                if (this.CanLeaveHere)
                    this.LeaveHere();
                else
                    return;
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

            // 閉塞遷移しつつある状態か推定
            //if (EstimateVehicleMoving())
            //{
            //    Logger.WriteLineAsTransInfo("Exit by estimated vehicle moving");
            //    return; // 遷移しつつある状態なら抜けて様子見
            //}

            // todo : halts support
            //todo : steady support and test with or without sensors 
            // verified i'm not halted and the next unit is not blocked by other vehicles
            if (Predicators.Any(l => l.ShouldLockNext(this)))
            {
                if (AssociatedRoute.LockNextUnit())
                {
                    Logger.WriteLineAsTransInfo("vehicle {0}({1}) moved : {2}", Name, ShownName, CurrentBlock.Name);
                }
            }

            var tail = AssociatedRoute.LockedUnits.First();
            if (Predicators.All(l => l.ShouldReleaseBefore(this)))
            {
                AssociatedRoute.ReleaseLastUnit();
                Logger.WriteLineAsTransInfo("vehicle {0}({1}) leaved : {2}", Name, ShownName, tail.ControlBlock.Name);
            }

            CommandFactory cmdfactory = null;
            SpeedFactory spdfactory = speedfactry;
            SpeedFactory stopspdfactory = stopSpeedfactory;

            Block[] lastlockedblocks = AssociatedRoute.LockedBlocks.ToArray();

            if (IsStopped)
            {
                if (!isStoppedLast) RefreshStoppingSpeedBehavior();

                if (AssociatedRoute.IsReserving)
                    AssociatedRoute.UnReserveHead();

                cmdfactory = CreateZeroCommand(stopSpeedfactory);
                isStoppedLast = true;
            }
            else
            {
                //neighbor check
                if (AssociatedRoute.IsReserving)
                    AssociatedRoute.UnReserveHead();
                int distance = AssociatedRoute.HeadContainer.GetDistanceOfBlockedUnit(3);
                if (AssociatedRoute.ReserveHead())
                {
                    if (isStoppedLast) RefreshRunningSpeedBehavior();

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

                    isStoppedLast = false;
                }
                else
                {
                    if (!isStoppedLast) RefreshStoppingSpeedBehavior();

                    cmdfactory = CreateZeroCommand(stopSpeedfactory);
                    isStoppedLast = true;
                }
            }
            Sheet.Effect(cmdfactory, AssociatedRoute.LockedBlocks.Concat(lastlockedblocks).Distinct(b => b.Name));
        }

        [DataMember]
        public bool IsHalted { get; private set; }

        public bool CanHaltHere
        {
            get { return this.Speed <= StopThreshold && this.AssociatedRoute.HeadContainer.Unit.HaltableBlocks.Any(); }
        }

        public bool CanLeaveHere
        {
            get
            {
                var head = this.AssociatedRoute.HeadContainer.Unit;
                // HaltしているUnit内の全てのBlockが解放状態でなければならない
                // 他の列車が進入している可能性あり
                if (head.Blocks.Except(head.HaltableBlocks).Any(b => b.IsLocked))
                    return false;

                // Length分のUnitをLockできるかどうかのチェック
                var units = new List<ControlUnitContainer>();
                var currentunit = this.AssociatedRoute.HeadContainer.Before;
                for (int i = 1; i < this.Length; ++i)
                {
                    if (currentunit == null)
                        break;

                    units.Add(currentunit);
                    currentunit = currentunit.Before;
                }

                if (units.Any(u => u.Unit.IsBlocked))
                    return false;

                return true;
            }
        }

        public void HaltHere()
        {
            if (this.IsHalted) throw new InvalidOperationException("すでにHaltしています．");
            if (!this.CanHaltHere) throw new InvalidOperationException("cannot halt here");

            try
            {
                //抑えているUnitが一つになるまでRelease
                while (this.AssociatedRoute.LockedUnits.Count() > 1) this.AssociatedRoute.ReleaseLastUnit();

                this.AssociatedRoute.LockAsHalted();
                this.IsHalted = true;
            }
            catch
            {
                throw;
            }
        }
        public void LeaveHere()
        {
            if (!this.IsHalted) throw new InvalidOperationException("Haltしていません");

            try
            {
                var head = this.AssociatedRoute.HeadContainer;
                this.AssociatedRoute.ReleaseAsHalted();

                //一旦すべてのblockを解放してから，取得し直す
                while (this.AssociatedRoute.ReleaseLastUnit()) ;
                this.AssociatedRoute.AllocateTrain(head.Unit.ControlBlock, this.Length);

                this.IsHalted = false;
            }
            catch { throw; }
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
            var a = object.Equals(A, null);
            var b = object.Equals(B, null);
            if (a && b) return true;
            else if (!a && !b) return  A.Equals(B);
            else return false; // A == null || B == null
        }

        public static bool operator !=(Vehicle A, Vehicle B)
        {
            return !(A == B);
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
            var blocks = rt.CalcBlocksAllocated(blk, 2).SelectMany(c => c.Unit.Blocks);

            return !blocks.Where(b => !this.AssociatedRoute.LockedBlocks.Contains(b)).Any(b => b.IsLocked);
        }
    }
}