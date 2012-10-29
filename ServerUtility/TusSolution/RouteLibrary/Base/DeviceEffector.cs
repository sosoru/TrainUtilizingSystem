using System;
using System.Linq;

using SensorLibrary;
using SensorLibrary.Devices;
using SensorLibrary.Devices.TusAvrDevices;
using SensorLibrary.Packet;
using SensorLibrary.Packet.Data;

namespace RouteLibrary.Base
{
    public interface IDeviceEffector
    {
        void ExecuteCommand();
        void ApplyCommand(CommandInfo cmd);
        void ExecuteDefaultCommand();
    }

    public abstract class DeviceEffector<TDev, TInfo, TState>
    : IDeviceEffector
        where TDev : IDevice<IDeviceState<IPacketDeviceData>>, new()
        where TInfo : DeviceInfo
        where TState : class, IDeviceState<IPacketDeviceData>, new()
    {
        public Block ParentBlock { get; private set; }
        public TInfo Info { get; private set; }
        public TDev Device { get; private set; }

        public DeviceEffector(TInfo info, Block block)
        {
            this.Info = info;
            this.ParentBlock = block;

            this.Device = new TDev()
            {
                DeviceID = info.Address
            };

            //this.Device.CurrentState.BasePacket = this.DefaultState.BasePacket;

            this.Device.Observe(block.Sheet.Dispatcher);
        }

        private DateTime before_send;
        public void ExecuteCommand()
        {
            //if ((DateTime.Now - before_send).Seconds > 5 ||
            //    (this.BeforeState == null
            //    || !CheckAll(this.BeforeState, this.Device.CurrentState as TState)))
            {
                this.Device.CurrentState.ReceivingServer = ParentBlock.Sheet.Server;
                this.Device.SendState();
                this.before_send = DateTime.Now;

                this.BeforeState = new TState()
                                       {
                                           //BasePacket = this.Device.CurrentState.BasePacket,
                                       };
            }
        }

        public void ExecuteDefaultCommand()
        {
            //this.Device.CurrentState.BasePacket = this.DefaultState.BasePacket;
            this.BeforeState = null;

            ExecuteCommand();
        }

        public abstract bool IsNeededExecution { get; set;}

        public abstract void ApplyCommand(CommandInfo cmd);

        //public TState BeforeState { get; private set; }
        //public abstract TState DefaultState { get; }

        //protected bool CheckBefore(Func<TState, object> f)
        //{
        //    return this.BeforeState == null
        //        || f(this.BeforeState).Equals(f((TState)this.Device.CurrentState));
        //}

        //protected virtual bool CheckAll(TState a, TState b)
        //{
        //    return false;
        //}
    }

    public class MotorEffector
        : DeviceEffector<Motor, MotorInfo, MotorState>
    {
        public MotorEffector(MotorInfo info, Block block)
            : base(info, block)
        {
        }

        private MotorState default_state;
        public MotorState DefaultState
        {
            get
            {
                var state = new MotorState()
                {
                    ReceivingServer = this.ParentBlock.Sheet.Server,
                    Direction = MotorDirection.Standby,
                    Duty = 0,
                    ControlMode = MotorControlMode.DutySpecifiedMode,
                };
                //state.FlushDataState();
                return state;
            }
        }

        public MotorState NoEffectState
        {
            get
            {
                var state = new MotorState()
                {
                    ReceivingServer = this.ParentBlock.Sheet.Server,
                    Direction = MotorDirection.Standby,
                    Duty = 0,
                    ControlMode = MotorControlMode.DutySpecifiedMode,
                };
                return state;
            }
        }

        public MotorState CreateWaitingState(Motor beforemtr)
        {
            var state = new MotorState()
            {
                ReceivingServer = this.ParentBlock.Sheet.Server,
                ControlMode = MotorControlMode.WaitingPulseMode,
                MemoryWhenEntered = MotorMemoryStateEnum.Controlling,
                DestinationID = beforemtr.DeviceID,
                ThresholdCurrent = 0.01f,
            };

            return state;
        }

        public MotorState CreateMotorState(MotorDirection dir, float duty)
        {
            var state = new MotorState()
            {
                ReceivingServer = this.ParentBlock.Sheet.Server,
                ControlMode = MotorControlMode.DutySpecifiedMode,
                Duty = duty,
                Direction = dir,
            };

            return state;
        }

        public MotorState CreateMotorState(CommandInfo info)
        {
            MotorDirection dir;
            float duty;
            var locked = cmd.Route.LockedSegments;

            if (!locked.ContainsKey(this.ParentBlock))
            {
                if (cmd.AnyToDefault)
                {
                    this.Device.CurrentState.Data = this.DefaultState.Data;
                }
                return;
            }

            var seg = locked[this.ParentBlock];
            if ((seg.IsFromAny || seg.From.Name == this.Info.RoutePositive.From.Name)
                    && (seg.IsToAny || seg.To.Name == this.Info.RoutePositive.To.Name))
            {
                dir = MotorDirection.Positive;
            }
            else if ((seg.IsFromAny || seg.From.Name == this.Info.RouteNegative.From.Name)
                        && (seg.IsToAny || seg.To.Name == this.Info.RouteNegative.To.Name))
            {
                dir = MotorDirection.Negative;
            }
            duty = cmd.Speed;

            return CreateMotorState(dir, duty);
        }

        public override bool IsNeededExecution
        {
            get;
            set;
        }

        public Block BeforeBlockHavingMotor(CommandInfo cmd)
        {
            var locked = cmd.Route.LockedBlocks.Where(s => s.HasMotor);
            var before = locked.Reverse().SkipWhile(b =>  b == this.ParentBlock).FirstOrDefault();

            return before;
        }

        public Block NextBlockHavingMotor(CommandInfo cmd)
        {
            var locked = cmd.Route.LockedBlocks.Where(s => s.HasMotor);
            var next = locked.SkipWhile(s => s == this.ParentBlock).FirstOrDefault();
            return next;
        }

        public MotorMemoryStateEnum SelectCurrentMemory(CommandInfo cmd)
        {
            var next = NextBlockHavingMotor(cmd);

            if (next != null)
            {
                var nextmtr = next.MotorEffector;

                if (nextmtr.Device.CurrentMemory == MotorMemoryStateEnum.Waiting)
                {
                    return MotorMemoryStateEnum.Controlling;
                }
                else if (nextmtr.Device.CurrentMemory == MotorMemoryStateEnum.Controlling)
                {
                    return MotorMemoryStateEnum.NoEffect;
                }
                else
                {
                    return MotorMemoryStateEnum.Unknown;
                }
            }
            else
            {
                // this block is the end of locked blocks
                return MotorMemoryStateEnum.Waiting;
            }
        }

        private MotorMemoryStateEnum _before_mode = MotorMemoryStateEnum.Unknown;
        public override void ApplyCommand(CommandInfo cmd)
        {
            var mode = SelectCurrentMemory(cmd);

            if (mode != _before_mode)
                this.IsNeededExecution = true;

            _before_mode = mode;

            switch (mode)
            {
                case MotorMemoryStateEnum.Controlling:
                    this.Device.StateWhenControlling = CreateMotorState(cmd);
                    break;
                case MotorMemoryStateEnum.Waiting:
                    var before = BeforeBlockHavingMotor(cmd).MotorEffector;
                    this.Device.StateWhenWaiting = CreateWaitingState(before);
                    break;
                case MotorMemoryStateEnum.NoEffect:
                default:
                    this.Device.StateWhenNoEffect = NoEffectState;
                    break;
            }    
        }

        //protected override bool CheckAll(MotorState a, MotorState b)
        //{
        //    return false; //return a.Data.Direction == b.Data.Direction && a.Data.Duty == b.Data.Duty;
        //}
    }

    public class SwitchEffector
        : DeviceEffector<Switch, SwitchInfo, SwitchState>
    {
        public SwitchEffector(SwitchInfo info, Block block)
            : base(info, block) { }

        private SwitchState default_state;
        public SwitchState DefaultState
        {
            get
            {
                var state = new SwitchState()
                    {
                        ReceivingServer = this.ParentBlock.Sheet.Server,
                        Position = PointStateEnum.Straight,
                        DeadTime = 100,
                        ChangingTime = 250,
                    };
                return state;
            }
        }

        public override bool IsNeededExecution
        {
            get;
            set;
        }

        private PointStateEnum _before_position = PointStateEnum.Any;
        public override void ApplyCommand(CommandInfo cmd)
        {
            //if (this.CheckBefore(s => s.Position))
            //{
            var locked = cmd.Route.LockedSegments;

            if (!locked.ContainsKey(this.ParentBlock))
                return;

            var segment = locked[this.ParentBlock];

            if ((segment.IsFromAny || this.Info.DirStraight.Any(i => i.From.Name == segment.From.Name))
                    && (segment.IsToAny || this.Info.DirStraight.Any(i => i.To.Name == segment.To.Name)))
            {
                this.Device.CurrentState.Position = PointStateEnum.Straight;
            }
            else if ((segment.IsFromAny || this.Info.DirCurved.Any(i => i.From.Name == segment.From.Name))
                        && (segment.IsToAny || this.Info.DirCurved.Any(i => i.To.Name == segment.To.Name)))
            {
                this.Device.CurrentState.Position = PointStateEnum.Curve;
            }
            else
            {
                this.Device.CurrentState.Position = PointStateEnum.Any;
            }
            //}

            if (_before_position != this.Device.CurrentState.Position)
                this.IsNeededExecution = true;

            _before_position = this.Device.CurrentState.Position;
        }

        //protected override bool CheckAll(SwitchState a, SwitchState b)
        //{
        //    return a.Data.Position == b.Data.Position;
        //}
    }

}
