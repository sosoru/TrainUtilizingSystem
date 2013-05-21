using System;
using System.Linq;
using System.Collections.Generic;
using Tus.Communication;
using Tus.Communication.Device.AvrComposed;

namespace Tus.TransControl.Base
{
    //public interface IDeviceEffector
    //{
    //    void ExecuteCommand();
    //    void ApplyCommand(CommandFactory factory);
    //    bool IsNeededExecution { get; }
    //}

    public interface IDeviceEffector<out TDev, out TInfo>
        where TDev : IDevice<IDeviceState<IPacketDeviceData>>
        where TInfo : DeviceInfo
    {
        Block ParentBlock { get; }
        TInfo Info { get; }
        TDev Device { get; }
        bool IsNeededExecution { get; set; }
        void ExecuteCommand();
        void ApplyCommand(CommandFactory factory);
    }

    public abstract class DeviceEffector<TDev, TInfo, TState>
    : IDeviceEffector<TDev, TInfo>
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
                DeviceID = info.Address,
                ReceivingServer = this.ParentBlock.Sheet.Server,
            };

            this.Device.Observe(block.Sheet.Dispatcher);
            this.IsNeededExecution = false;
        }

        private DateTime before_send;
        public void ExecuteCommand()
        {
            this.Device.ReceivingServer = ParentBlock.Sheet.Server;
            this.Device.SendState();
            this.before_send = DateTime.Now;

            this.IsNeededExecution = false;
        }

        public virtual bool IsNeededExecution { get; set; }
        public abstract void ApplyCommand(CommandFactory factory);
    }

    static class BlockExtension
    {
        public static Block BeforeBlockHavingMotor(this Block blk, Route route)
        {
            var locked = route.LockedBlocks.Where(s => s.HasMotor);
            var before = locked.Reverse().SkipWhile(b => b == blk).FirstOrDefault();

            return before;
        }

        public static Block NextBlockHavingMotor(this Block blk, Route route)
        {
            var locked = route.LockedBlocks.Where(s => s.HasMotor);
            var next = locked.SkipWhile(s => s == blk).FirstOrDefault();
            return next;
        }
    }

    public class MotorEffector
        : DeviceEffector<Motor, MotorInfo, MotorState>
    {
        public MotorEffector(MotorInfo info, Block block)
            : base(info, block)
        {
        }

        #region "Create State Methods"
        private MotorDirection _before_dir = MotorDirection.Standby;
        private float _before_duty = 2.0f;
        private MotorState CreateMotorState(MotorDirection dir, float duty)
        {
            var state = new MotorState()
            {
                ControlMode = MotorControlMode.DutySpecifiedMode,
                Duty = duty,
                Direction = dir,
            };

            if (this._before_dir != dir || this._before_duty != duty)
                this.IsNeededExecution = true;

            this._before_dir = dir;
            this._before_duty = duty;

            return state;
        }

        private MotorState CreateMotorState(CommandInfo cmd)
        {
            MotorDirection dir = MotorDirection.Standby;
            float duty = 0f;
            var locked = cmd.Route.RouteOrder.GetLockingControlingRoute(this.ParentBlock);

            if (locked == null)
            {
                return CreateNoEffectState();
            }

            var seg = cmd.Route.RouteOrder.GetSegment(this.ParentBlock);
            dir = this.Info.SelectDirection(seg.Info);
            duty = cmd.Speed;

            return CreateMotorState(dir, duty);
        }

        private MotorState CreateNoEffectState()
        {
            var state = new MotorState()
            {
                Direction = MotorDirection.Positive,
                Duty = 0,
                ControlMode = MotorControlMode.DutySpecifiedMode,
            };
            return state;
        }

        private MotorState CreateLockedState()
        {
            var state = new MotorState()
            {
                Direction = MotorDirection.Standby,
                Duty = 0,
                ControlMode = MotorControlMode.DutySpecifiedMode,
            };
            return state;
        }

        private MotorState CreateWaitingState(BlockPolar polar, Motor beforemtr)
        {
            var threshold = 0.05f;
            MotorState state;
            if (polar == BlockPolar.Positive)
            {
                state = new MotorState()
                            {
                                ControlMode = MotorControlMode.WaitingPulseMode,
                                MemoryWhenEntered = MotorMemoryStateEnum.Locked,
                                DestinationID = beforemtr.DeviceID,
                                DestinationMemory = MotorMemoryStateEnum.Controlling,
                                ThresholdCurrent = threshold,
                            };
            }
            else if (polar == BlockPolar.Negative)
            {
                state = new MotorState()
                            {
                                ControlMode = MotorControlMode.WaitingPulseMode,
                                MemoryWhenEntered = MotorMemoryStateEnum.Controlling,
                                DestinationID = beforemtr.DeviceID,
                                DestinationMemory = MotorMemoryStateEnum.Locked,
                                ThresholdCurrent = threshold,
                            };
            }
            else
                throw new NotImplementedException("polar undecidable");

            return state;
        }

        #endregion

        public override bool IsNeededExecution
        {
            get;
            set;
        }

        public void SetDetectingMode(float duty)
        {
            var mode = MotorMemoryStateEnum.Controlling;
            var states = new Dictionary<MotorMemoryStateEnum, MotorState>()
            {
                {mode, CreateMotorState(MotorDirection.Positive, duty)}
            };

            this.Device.States = states;
            this.Device.CurrentMemory = mode;

            this.Device.SendState();
        }

        private MotorState _before_mtr_state;
        private MotorMemoryStateEnum _before_state = MotorMemoryStateEnum.Unknown;
        public override void ApplyCommand(CommandFactory factory)
        {
            if (this.IsNeededExecution)
                return;

            if (_before_mtr_state == null)
                _before_mtr_state = new MotorState();

            var cmd = factory.CreateCommand(this.ParentBlock);
            var states = new Dictionary<MotorMemoryStateEnum, MotorState>();

            switch (cmd.MotorMode)
            {
                case MotorMemoryStateEnum.Controlling:
                    if (_before_state != MotorMemoryStateEnum.Controlling)
                    {
                        states.Add(MotorMemoryStateEnum.Locked, CreateLockedState());
                    }
                    states.Add(MotorMemoryStateEnum.Controlling, CreateMotorState(cmd));
                    this.IsNeededExecution = true;
                    break;
                case MotorMemoryStateEnum.Waiting:
                    var polar = cmd.Route.RouteOrder.Polar;
                    var cntstate = CreateMotorState(cmd);
                    var waitingblock = this.ParentBlock.BeforeBlockHavingMotor(cmd.Route);

                    states.Add(MotorMemoryStateEnum.Controlling, CreateMotorState(cmd));
                    states.Add(MotorMemoryStateEnum.Waiting, CreateWaitingState(polar, waitingblock.MotorEffector.Device));
                    if (this.Device.States.ContainsKey(MotorMemoryStateEnum.Controlling) &&
                        this.Device.States[MotorMemoryStateEnum.Controlling].Duty == cntstate.Duty)
                    {
                        this.IsNeededExecution = false;
                    }
                    else
                    {
                        this.IsNeededExecution = true;
                    }

                    break;
                case MotorMemoryStateEnum.NoEffect:
                    states.Add(MotorMemoryStateEnum.NoEffect, CreateNoEffectState());
                    break;
                case MotorMemoryStateEnum.Locked:
                    states.Add(MotorMemoryStateEnum.Locked, CreateLockedState());
                    break;

                case MotorMemoryStateEnum.Unknown:
                default:
                    throw new InvalidOperationException("invalid mode applied");
            }

            this.Device.CurrentMemory = cmd.MotorMode;
            this.Device.States = states;

            if (_before_state != cmd.MotorMode)
                this.IsNeededExecution = true;

            this._before_state = cmd.MotorMode;
        }
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
                        Position = PointStateEnum.Straight,
                        DeadTime = 300,
                        ChangingTime = 250,
                    };
                return state;
            }
        }

        private PointStateEnum _before_position = PointStateEnum.Any;
        public override void ApplyCommand(CommandFactory factory)
        {
            if (this.IsNeededExecution)
                return;

            var cmd = factory.CreateCommand(this.ParentBlock);
            if (cmd.Route == null)
                return;

            var segment = cmd.Route.RouteOrder.GetSegment(this.ParentBlock);

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
    }

}
