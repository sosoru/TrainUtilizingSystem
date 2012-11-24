﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using SensorLibrary.Devices.TusAvrDevices;

using System.Reactive;
using System.Reactive.Linq;

namespace RouteLibrary.Base
{
    public class SpeedFactory
    {
        public float RawSpeed { get; set; }
        public float Go { get { return RawSpeed; } }
        public float Caution { get { return RawSpeed * 0.8f; } }
        public float Stop { get { return this.RawSpeed * 0.3f; } }
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
        public Route Route { get; private set; }
        public BlockSheet Sheet { get; set; }

        [DataMember]
        public IList<Halt> Halt { get; private set; }

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
            this.Length = 1;
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
            if (this.Route.LockedUnits.Count > 1)
            {
                // verified i'm not halted and the next unit is not blocked by other vehicles

                var waitingunit = this.Route.LockedUnits.Last();
                if (waitingunit.ControlBlock.IsMotorDetectingTrain)
                {
                    this.CurrentBlock = waitingunit.ControlBlock;
                    Console.WriteLine("vehicle moved : {0}", this.CurrentBlock.Name);
                    Console.WriteLine(this.CurrentBlock.MotorEffector.Device.ToString());

                }
            } 

            Run(this.Speed, this.CurrentBlock);
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

        public void Run()
        {
            this.Run(0.5f);
        }

        public void ChangeRoute(Route rt)
        {
            this.Route = rt;
            this.Refresh();
        }

        public CommandFactory CreateWithWaitingCommand(Func<float> cntspdFactory, Func<float> waitspdFactory)
        {
            var func = new Func<Block, CommandInfo>(blk =>
                {
                    var cmd = new CommandInfo()
                    {
                        Route = this.Route,
                    };
                    var lockedunits = this.Route.LockedUnits.ToArray();

                    if (blk == lockedunits[lockedunits.Length - 2].ControlBlock)
                    {
                        cmd.MotorMode = MotorMemoryStateEnum.Controlling;
                        cmd.Speed = cntspdFactory();
                    }
                    else if (blk == lockedunits.Last().ControlBlock)
                    {
                        cmd.MotorMode = MotorMemoryStateEnum.Waiting;
                        cmd.Speed = waitspdFactory();
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

        public CommandFactory CreateControlCommand(Func<float> cntspdFactory)
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

        public CommandFactory CreateBlockageIgnoreCommand(Func<float> cntspdfact)
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

        public CommandFactory CreateNthCommand(SpeedFactory spdfactory)
        {
            return CreateWithWaitingCommand(() => spdfactory.Go, () => spdfactory.Go);
        }

        public CommandFactory Create2ndCommand(SpeedFactory spdfactory)
        {
            return CreateWithWaitingCommand(() => spdfactory.Go, () => spdfactory.Caution);
        }

        public CommandFactory Create1stCommand(SpeedFactory spdfactory)
        {
            return CreateWithWaitingCommand(() => spdfactory.Caution, () => spdfactory.Stop);
        }

        public CommandFactory CreateZeroCommand(SpeedFactory spdfactory)
        {
            return CreateControlCommand(() => spdfactory.Stop);
        }

        public CommandFactory CreateHaltCommand(SpeedFactory spdfactory)
        {
            return CreateControlCommand(() => spdfactory.Caution);
        }

        public void Run(float spd, Block blk)
        {
            this.CurrentBlock = blk;
            this.Run(spd);
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

        public void Run(float spd)
        {
            CommandFactory cmdfactory = null;
            var spdfactory = new SpeedFactory() { RawSpeed = spd };

            var lastlockedblocks = this.Route.LockedBlocks.ToArray();
            try
            {
                this.Route.AllocateTrain(this.CurrentBlock, this.Length);
            }
            catch (InvalidOperationException ex) { return; }

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
    }
}
