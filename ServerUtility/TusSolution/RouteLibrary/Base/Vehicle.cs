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
        public float Go { get { return RawSpeed; } }
        public float Caution { get { return RawSpeed * 0.5f; } }
        public float Stop { get { return RawSpeed * 0f; } }
    }

    [DataContract]
    public class Vehicle
    {
        private static int LastVehicleID;

        [DataMember]
        public int VehicleID { get; private set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public IList<Route> AvailableRoutes { get; set; }

        [DataMember]
        public Block CurrentBlock { get; set; }

        [DataMember]
        public Route Route { get; set; }
        public BlockSheet Sheet { get; set; }

        [DataMember]
        public IList<Halt> Halt { get; set; }

        [DataMember]
        public float Speed { get; set; }

        [DataMember]
        public int Length { get; set; }

        public bool IgnoreBlockage { get; set; }

        static Vehicle()
        {
            LastVehicleID = 0;
        }

        public Vehicle(BlockSheet sht, Route rt)
        {
            this.VehicleID = LastVehicleID++;

            this.Sheet = sht;
            this.Route = rt;

            this.CurrentBlock = this.Route.Blocks.First();
            this.Length = 2;
            this.Halt = new List<Halt>();
        }

        public bool ShouldHalt
        {
            get
            {
                return this.Halt != null
                    && this.Halt.Any(bh => this.Route.LockedBlocks.Contains(bh.HaltBlock) && bh.HaltState);
            }
        }

        public bool CanHalt
        {
            get
            {
                return this.Halt != null
                    && this.Halt.Any(bh => this.Route.LockedBlocks.Contains(bh.HaltBlock));
            }
        }

        public void Refresh()
        {
            if (this.ShouldHalt)
            {
                this.Speed = 0.0f;
            }
            if (this.IgnoreBlockage)
            {
                this.RunIfIgnored(this.Speed);
                return;
            }

            // todo : halts support
            //todo : steady support and test with or without sensors 
            if (this.Route.LockedUnits.Count > 1)
            {
                // verified i'm not halted and the next unit is not blocked by other vehicles

                var waitingunit = this.Route.LockedUnits.Last();
                if (waitingunit.ControlBlock.IsMotorDetectingTrain)
                {
                    this.CurrentBlock = waitingunit.ControlBlock;
                    Console.WriteLine("vehicle {0} moved : {1}", this.Name, this.CurrentBlock.Name);
                    //this.CurrentBlock.Neighbors.ForEach(b => Console.WriteLine(b.MotorEffector.Device.ToString()));
                    //Console.WriteLine(this.CurrentBlock.MotorEffector.Device.ToString());

                }
            }

            Run(this.Speed, this.CurrentBlock);
        }

        public void ChangeRoute(Route rt)
        {
            //todo : check the routes can be changed
            this.Speed = 0;
            this.Refresh();
            while (this.Route.ReleaseBeforeUnit()) ;

            rt.IsRepeatable = this.Route.IsRepeatable;

            this.Route = rt;
            this.Refresh();
        }

        public void RunIfIgnored(float spd)
        {
            var spdfact = new SpeedFactory { RawSpeed = spd };

            //all alocate
            while (this.Route.LockNextUnit()) ;

            CommandFactory cmdfact = null;

            cmdfact = CreateBlockageIgnoreCommand(() => spdfact.Go);

            this.Sheet.Effect(cmdfact, this.Route.Blocks.ToList().Distinct());
        }

        public void Run()
        {
            this.Run(0.5f);
        }

        public void Run(float spd, Block blk)
        {
            this.CurrentBlock = blk;
            this.Run(spd);
        }

        public void Run(float spd)
        {
            CommandFactory cmdfactory = null;
            var spdfactory = new SpeedFactory() { RawSpeed = spd };

            var lastlockedblocks = this.Route.LockedBlocks.ToArray();
            try
            {
                this.Route.AllocateTrain(this.CurrentBlock, this.Length);
            }
            catch { throw; }

            if (!this.Route.LockNextUnit())
            {
                if (this.CanHalt)
                {
                    cmdfactory = CreateHaltCommand(spdfactory);
                }
                else
                {
                    cmdfactory = CreateZeroCommand(spdfactory);
                }
            }
            else if (!this.Route.TryLockNeighborUnit(1))
            {
                cmdfactory = Create1stCommand(spdfactory);
            }
            else if (!this.Route.TryLockNeighborUnit(2))
            {
                cmdfactory = Create2ndCommand(spdfactory);
            }
            else
            {
                cmdfactory = CreateNthCommand(spdfactory);
            }

            this.Sheet.Effect(cmdfactory, this.Route.LockedBlocks.Concat(lastlockedblocks).Distinct());
        }

        public void Reverse()
        {
            //stop vehicle
            this.Run(0);

            var cnt = this.CurrentBlock;
            this.Route.Reverse();
            while (this.Route.ReleaseBeforeUnit()) ;

            this.Route.AllocateTrain(cnt, this.Length);

        }

        private BlockPolar getBlockPolar(CommandInfo cmd, Block blk)
        {
            var rt = cmd.Route;

            var pos = blk.Neighbors.First();
            var neg = blk.Neighbors.Last();
            var posind = rt.Blocks.IndexOf(pos);
            var negind = rt.Blocks.IndexOf(neg);

            //todo : fix to clockwise polar
            if (posind > negind) // positive
                return BlockPolar.Positive;
            else
                return BlockPolar.Negative;
        }

        #region "CreateCommandMethods"
        private CommandFactory CreateWithWaitingCommand(Func<float> cntspdFactory, Func<float> waitspdFactory)
        {
            var func = new Func<Block, CommandInfo>(blk =>
                {
                    var cmd = new CommandInfo()
                    {
                        Route = this.Route,
                    };
                    var lockedunits = this.Route.LockedUnits.ToArray();
                    var cntblk = (cmd.Route.Polar == BlockPolar.Positive)
                                     ? lockedunits.First().ControlBlock
                                     : lockedunits[lockedunits.Length - 2].ControlBlock;
                    var waitblk = lockedunits.Last().ControlBlock;

                    if (blk == cntblk)// lockedunits[lockedunits.Length - 2].ControlBlock)
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
            return new CommandFactory() { CreateCommand = func };
        }

        private CommandFactory CreateControlCommand(Func<float> cntspdFactory)
        {
            var func = new Func<Block, CommandInfo>(blk =>
                {
                    var cmd = new CommandInfo()
                    {
                        Route = this.Route,
                    };

                    if (blk == this.Route.LockedUnits.Last().ControlBlock)
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
            return new CommandFactory() { CreateCommand = func };
        }

        private CommandFactory CreateBlockageIgnoreCommand(Func<float> cntspdfact)
        {
            var fun = new Func<Block, CommandInfo>(blk =>
                {
                    var cmd = new CommandInfo()
                    {
                        Route = this.Route,
                        Speed = cntspdfact(),
                        MotorMode = MotorMemoryStateEnum.Controlling
                    };

                    return cmd;
                });
            return new CommandFactory() { CreateCommand = fun };
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
            return this.VehicleID.Equals(((Vehicle)obj).VehicleID);
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
            return this.VehicleID.GetHashCode();
        }

        #endregion
    }
}
