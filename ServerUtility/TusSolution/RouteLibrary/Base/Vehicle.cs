using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary.Devices.TusAvrDevices;

namespace RouteLibrary.Base
{

    public class Vehicle
    {
        private static int LastVehicleID;
        public int VehicleID { get; private set; }

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

        public CommandFactory CreateNthCaseCommand(float speed)
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
                        cmd.Speed = speed;
                    }
                    else if (blk == this.Route.LockedUnits.Last().ControlBlock)
                    {
                        cmd.MotorMode = MotorMemoryStateEnum.Waiting;
                        cmd.Speed = speed;
                    }

                    return new CommandFactory() { CreateCommand = func };
                });
        }
        public CommandFactory Create2ndCaseCommand(float speed)
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
                    cmd.Speed = speed;
                }
                else if (blk == this.Route.LockedUnits.Last().ControlBlock)
                {
                    cmd.MotorMode = MotorMemoryStateEnum.Waiting;
                    cmd.Speed = speed / 2.0f;
                }

                return new CommandFactory() { CreateCommand = func };
            });
        }
        public CommandFactory Create2ndCaseCommand(float speed)
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
                    cmd.Speed = speed / 2.0f;
                }
                else if (blk == this.Route.LockedUnits.Last().ControlBlock)
                {
                    cmd.MotorMode = MotorMemoryStateEnum.Waiting;
                    cmd.Speed = 0.0f;
                }

                return new CommandFactory() { CreateCommand = func };
            });
        }
        public CommandFactory Create1stCaseCommand(float speed)
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
                    cmd.Speed = speed / 2.0f;
                }
                else if (blk == this.Route.LockedUnits.Last().ControlBlock)
                {
                    cmd.MotorMode = MotorMemoryStateEnum.Waiting;
                    cmd.Speed = 0.0f;
                }

                return new CommandFactory() { CreateCommand = func };
            });
        }
        public CommandFactory CreateZeroCaseCommand(float speed)
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
                    cmd.Speed = 0.0f;
                }

                return new CommandFactory() { CreateCommand = func };
            });
        }


        public void Run(float spd)
        {

            //todo : check blocks locked
            this.Route.LockNextUnit();
            this.Route.LockNextUnit();

            var createfunc = new Func<Block, CommandInfo>((block) =>
                {
                    var basecmd = new CommandInfo()
                    {
                        Route = this.Route,
                        Speed = spd,
                    };


                    if (block == this.Route.LockedUnits.First().ControlBlock)
                        basecmd.MotorMode = MotorMemoryStateEnum.Controlling;
                    else if (block == this.Route.LockedUnits.Last().ControlBlock)
                        basecmd.MotorMode = MotorMemoryStateEnum.Waiting;

                    return basecmd;
                });

            var factory = new CommandFactory()
            {
                CreateCommand = createfunc
            };

            this.Sheet.Effect(factory, this.Route.LockedBlocks);


        }
    }
}
