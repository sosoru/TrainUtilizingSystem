using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary.Devices.TusAvrDevices;

namespace RouteLibrary.Base
{
    public class SpeedFactory
    {
        public float RawSpeed { get; set; }
        public float Go { get { return RawSpeed; } }
        public float Caution { get { return RawSpeed / 2.0f; } }
        public float Stop { get { return 0.0f; } }
    }

    public class Vehicle
    {
        private static int LastVehicleID;
        public int VehicleID { get; private set; }

        public Block CurrentBlock { get; set; }
        public Route Route { get; set; }
        public BlockSheet Sheet { get; set; }

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
        }

        public bool Refresh()
        {
            bool is_refreshed = false;

            if (this.Route.IsSectionFinished)
            {
                this.Route.LockNextUnit();
                is_refreshed = true;
            }

            if (this.Route.IsLeftSectionFirst)
            {
                this.Route.ReleaseBeforeUnit();
                is_refreshed = true;
            }

            return is_refreshed;
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

        public CommandFactory CreateWithWaitingCommand(Func<float> cntspdFactory, Func<float> waitspdFactory)
        {
            var func = new Func<Block, CommandInfo>(blk =>
                {
                    var cmd = new CommandInfo()
                    {
                        Route = this.Route,
                    };

                    if (blk == this.Route.LockedUnits.First().ControlBlock)
                    {
                        cmd.MotorMode = MotorMemoryStateEnum.Controlling;
                        cmd.Speed = cntspdFactory();
                    }

                    else if (blk == this.Route.LockedUnits.Last().ControlBlock)
                    {
                        cmd.MotorMode = MotorMemoryStateEnum.Waiting;
                        cmd.Speed = waitspdFactory();
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
                        MotorMode = MotorMemoryStateEnum.Controlling,
                        Speed = cntspdFactory(),
                    };
                    return cmd;
                });
            return new CommandFactory() { CreateCommand = func };
        }

        public CommandFactory CreateNthCommand(SpeedFactory spdfactory)
        {
            return CreateWithWaitingCommand(() => spdfactory.Go,() => spdfactory.Go);
        }

        public CommandFactory Create2ndCommand(SpeedFactory spdfactory)
        {
            return CreateWithWaitingCommand(() => spdfactory.Go, () =>spdfactory.Caution);
        }

        public CommandFactory Create1stCommand(SpeedFactory spdfactory)
        {
            return CreateWithWaitingCommand(() => spdfactory.Caution, () => spdfactory.Stop);
        }

        public CommandFactory CreateZeroCommand(SpeedFactory spdfactory)
        {
            return CreateControlCommand(() => spdfactory.Stop);
        }

        public void Run(float spd, Block blk)
        {
            this.CurrentBlock = blk;
            this.Run(spd);
        }

        public void Run(float spd)
        {
            CommandFactory cmdfactory = null;
            var spdfactory = new SpeedFactory() { RawSpeed = spd};

            this.Route.AllocateTrain(this.CurrentBlock, 1);
            
            if (!this.Route.LockNextUnit())
            {
                cmdfactory = CreateZeroCommand(spdfactory);
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

            this.Sheet.Effect(cmdfactory, this.Route.LockedBlocks);
        }
    }
}
