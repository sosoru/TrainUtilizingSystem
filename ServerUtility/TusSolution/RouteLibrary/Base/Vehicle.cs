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

    [DataContract]
    public class Vehicle
    {
        private static int LastVehicleID;

        static Vehicle()
        {
            LastVehicleID = 0;
        }

        public Vehicle(BlockSheet sht, Route rt)
        {
            VehicleID = LastVehicleID++;

            Sheet = sht;
            Route = rt;

            CurrentBlock = Route.Blocks.First();
            Length = 2; //todo : 閉塞管理を見直して動的にLengthを変えた方が良いかも
            CurrentLength = Length;
            Halt = new List<Halt>();

            this.Run(0.0f);
        }

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

        [DataMember]
        public int CurrentLength { get; private set; }

        public bool IgnoreBlockage { get; set; }

        public bool ShouldHalt
        {
            get
            {
                return Halt != null
                       && Halt.Any(bh => Route.LockedBlocks.Contains(bh.HaltBlock) && bh.HaltState);
            }
        }

        public bool CanHalt
        {
            get
            {
                return Halt != null
                       && Halt.Any(bh => Route.LockedBlocks.Contains(bh.HaltBlock));
            }
        }

        public void Refresh()
        {
            if (ShouldHalt)
            {
                Speed = 0.0f;
            }
            if (IgnoreBlockage)
            {
                RunIfIgnored(Speed);
                return;
            }

            // todo : halts support
            //todo : steady support and test with or without sensors 
            //if (this.Route.LockedUnits.Count > 1)
            //{
            // verified i'm not halted and the next unit is not blocked by other vehicles

            ControllingUnit waitingunit = Route.LockedUnits.Last();
            if (waitingunit.ControlBlock.IsMotorDetectingTrain)
            {
                CurrentBlock = waitingunit.ControlBlock;
                Console.WriteLine("vehicle {0} moved : {1}", Name, CurrentBlock.Name);
                //this.CurrentBlock.Neighbors.ForEach(b => Console.WriteLine(b.MotorEffector.Device.ToString()));
                //Console.WriteLine(this.CurrentBlock.MotorEffector.Device.ToString());
            }
            //}

            Run(Speed, CurrentBlock);
        }

        public void ChangeRoute(Route rt)
        {
            //todo : check the routes can be changed
            Speed = 0;
            Refresh();
            while (Route.ReleaseBeforeUnit()) ;

            rt.IsRepeatable = Route.IsRepeatable;

            Route = rt;
            Refresh();
        }

        public void RunIfIgnored(float spd)
        {
            var spdfact = new SpeedFactory { RawSpeed = spd };

            //all alocate
            while (Route.LockNextUnit()) ;

            CommandFactory cmdfact = null;

            cmdfact = CreateBlockageIgnoreCommand(() => spdfact.Go);

            Sheet.Effect(cmdfact, Route.Blocks.ToList().Distinct());
        }

        public void Run()
        {
            Run(0.5f);
        }

        public void Run(float spd, Block blk)
        {
            CurrentBlock = blk;
            Run(spd);
        }

        public void Run(float spd)
        {
            CommandFactory cmdfactory = null;
            var spdfactory = new SpeedFactory { RawSpeed = spd };

            Block[] lastlockedblocks = Route.LockedBlocks.ToArray();

            //temporalily allocate
            Route.AllocateTrain(CurrentBlock, 1);
            var distance = Route.GetDistanceOfBlockedUnit(3);

            if (this.ShouldHalt || distance == 1)
            {
                cmdfactory = CanHalt
                                 ? CreateHaltCommand(spdfactory)
                                 : CreateZeroCommand(spdfactory);
                CurrentLength = Length;
            }
            else if (distance == 2)
            {
                cmdfactory = Create1stCommand(spdfactory);
                CurrentLength = Length + 1;
            }
            else if (distance == 3)
            {
                cmdfactory = Create2ndCommand(spdfactory);
                CurrentLength = Length + 1;
            }
            else
            {
                cmdfactory = CreateNthCommand(spdfactory);
                CurrentLength = Length + 1;
            }

            if (this.CurrentLength > Route.BlockPassedCount)
                this.CurrentLength = Route.BlockPassedCount;

            Route.AllocateTrain(this.CurrentBlock, this.CurrentLength);
            Sheet.Effect(cmdfactory, Route.LockedBlocks.Concat(lastlockedblocks).Distinct());
        }

        private BlockPolar getBlockPolar(CommandInfo cmd, Block blk)
        {
            Route rt = cmd.Route;

            Block pos = blk.Neighbors.First();
            Block neg = blk.Neighbors.Last();
            int posind = rt.Blocks.IndexOf(pos);
            int negind = rt.Blocks.IndexOf(neg);

            //todo : fix to clockwise polar
            if (posind > negind) // positive
                return BlockPolar.Positive;
            else
                return BlockPolar.Negative;
        }

        #region "CreateCommandMethods"

        private CommandFactory CreateWithWaitingCommand(Func<float> cntspdFactory, Func<float> waitspdFactory)
        {
            var func = new Func<Block, CommandInfo>
                (blk =>
                     {
                         var cmd = new CommandInfo
                                       {
                                           Route = Route,
                                       };
                         ControllingUnit[] lockedunits = Route.LockedUnits.ToArray();
                         Block cntblk = (cmd.Route.Polar == BlockPolar.Positive)
                                            ? lockedunits.First().ControlBlock
                                            : lockedunits[lockedunits.Length - 2]
                                                  .ControlBlock;
                         Block waitblk = lockedunits.Last().ControlBlock;

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
                                           Route = Route,
                                       };

                         if (blk == Route.LockedUnits.Last().ControlBlock)
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
                                           Route = Route,
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