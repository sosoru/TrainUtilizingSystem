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
            AssociatedRoute = rt;

            CurrentBlock = AssociatedRoute.RouteOrder.Blocks.First();
            Length = 1;
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
        public Route AssociatedRoute { get; set; }

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
                       && Halt.Any(bh => AssociatedRoute.LockedBlocks.Contains(bh.HaltBlock) && bh.HaltState);
            }
        }

        public bool CanHalt
        {
            get
            {
                return Halt != null
                       && Halt.Any(bh => AssociatedRoute.LockedBlocks.Contains(bh.HaltBlock));
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
            if (this.AssociatedRoute.LockedUnits.Count > 2)
            {
                // verified i'm not halted and the next unit is not blocked by other vehicles

                ControlUnit waitingunit = AssociatedRoute.LockedUnits.Last();
                if (waitingunit.ControlBlock.IsMotorDetectingTrain)
                {
                    var units = AssociatedRoute.LockedUnits.ToArray();
                    this.CurrentBlock = units[units.Length - 2].ControlBlock;
                    Console.WriteLine("vehicle {0} moved : {1}", Name, CurrentBlock.Name);
                    //this.CurrentBlock.Neighbors.ForEach(b => Console.WriteLine(b.MotorEffector.Device.ToString()));
                    //Console.WriteLine(this.CurrentBlock.MotorEffector.Device.ToString());
                }
            }

            Run(Speed, CurrentBlock);
        }

        public void ChangeRoute(Route rt)
        {
            //todo : check the routes can be changed
            Speed = 0;
            Refresh();
            while (AssociatedRoute.ReleaseLastUnit()) ;

            rt.RouteOrder.IsRepeatable = AssociatedRoute.RouteOrder.IsRepeatable;

            AssociatedRoute = rt;
            Refresh();
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

        public void Run(float spd, Block blk)
        {
            CurrentBlock = blk;
            Run(spd);
        }

        public void Run(float spd)
        {
            CommandFactory cmdfactory = null;
            var spdfactory = new SpeedFactory { RawSpeed = spd };

            Block[] lastlockedblocks = AssociatedRoute.LockedBlocks.ToArray();

            //temporalily allocate
            AssociatedRoute.AllocateTrain(CurrentBlock, 1);
            var distance = AssociatedRoute.HeadContainer.GetDistanceOfBlockedUnit(3);

            this.Speed = spd;
            if (this.Speed == 0.0f || distance == 1)
                this.CurrentLength = this.Length;
            else
            {
                if (this.CurrentLength < this.Length + 2)
                    this.CurrentLength++;
            }

            AssociatedRoute.AllocateTrain(CurrentBlock, this.CurrentLength);

            if (this.ShouldHalt || this.Speed == 0.0f || distance == 1)
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

            if (this.CurrentLength > AssociatedRoute.HeadContainer.BlockPassedCount)
                this.CurrentLength = AssociatedRoute.HeadContainer.BlockPassedCount;

            AssociatedRoute.AllocateTrain(this.CurrentBlock, this.CurrentLength);
            Sheet.Effect(cmdfactory, AssociatedRoute.LockedBlocks.Concat(lastlockedblocks).Distinct());
        }

        private BlockPolar getBlockPolar(CommandInfo cmd, Block blk)
        {
            Route rt = cmd.Route;

            Block pos = blk.Neighbors.First();
            Block neg = blk.Neighbors.Last();
            int posind = rt.RouteOrder.Blocks.IndexOf(pos);
            int negind = rt.RouteOrder.Blocks.IndexOf(neg);

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
                                           Route = AssociatedRoute,
                                       };
                         ControlUnit[] lockedunits = AssociatedRoute.LockedUnits.ToArray();
                         Block cntblk = (cmd.Route.RouteOrder.Polar == BlockPolar.Positive)
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
                                           Route = AssociatedRoute,
                                       };

                         if (blk == AssociatedRoute.LockedUnits.Last().ControlBlock)
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