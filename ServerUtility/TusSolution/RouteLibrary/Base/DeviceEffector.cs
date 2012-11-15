using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

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
        void ApplyCommand(CommandFactory factory);
        bool IsNeededExecution { get; }        
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

    public class MotorEffector
        : DeviceEffector<Motor, MotorInfo, MotorState>
    {
        public MotorEffector(MotorInfo info, Block block)
            : base(info, block)
        {
        }

        public MotorState NoEffectState
        {
            get
            {
                var state = new MotorState()
                {
                    Direction = MotorDirection.Positive,
                    Duty = 0,
                    ControlMode = MotorControlMode.DutySpecifiedMode,
                };
                return state;
            }
        }

        public MotorState LockedState
        {
            get
            {
                var state = new MotorState()
                {
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
                ControlMode = MotorControlMode.WaitingPulseMode,
                MemoryWhenEntered = MotorMemoryStateEnum.Controlling,
                DestinationID = beforemtr.DeviceID,
                DestinationMemory = MotorMemoryStateEnum.Locked,
                ThresholdCurrent = 0.01f,
            };

            return state;
        }

        private MotorDirection _before_dir = MotorDirection.Standby;
        private float _before_duty = 2.0f;
        public MotorState CreateMotorState(MotorDirection dir, float duty)
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

        public MotorState CreateMotorState(CommandInfo cmd)
        {
            MotorDirection dir = MotorDirection.Standby;
            float duty = 0f;
            var locked = cmd.Route.GetLockingControlingRoute(this.ParentBlock);

            if (locked == null)
            {
                return NoEffectState;
            }

            var seg = cmd.Route.Segments[this.ParentBlock];
            if (seg.Info.Equals(this.Info.RoutePositive))
            {
                dir = MotorDirection.Positive;
            }
            else if (seg.Info.Equals(this.Info.RouteNegative))
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
            var before = locked.Reverse().SkipWhile(b => b == this.ParentBlock).FirstOrDefault();

            return before;
        }

        public Block NextBlockHavingMotor(CommandInfo cmd)
        {
            var locked = cmd.Route.LockedBlocks.Where(s => s.HasMotor);
            var next = locked.SkipWhile(s => s == this.ParentBlock).FirstOrDefault();
            return next;
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

        private MotorMemoryStateEnum _before_state = MotorMemoryStateEnum.Unknown;
        public override void ApplyCommand(CommandFactory factory)
        {
            if (this.IsNeededExecution)
                return;

            var cmd = factory.CreateCommand(this.ParentBlock);
            var states = new Dictionary<MotorMemoryStateEnum, MotorState>();

            switch (cmd.MotorMode)
            {
                case MotorMemoryStateEnum.Controlling:
                    states.Add(MotorMemoryStateEnum.Controlling, CreateMotorState(cmd));
                    this.Device.CurrentMemory = MotorMemoryStateEnum.Controlling;
                    this.IsNeededExecution = true;
                    break;
                case MotorMemoryStateEnum.Waiting:
                    states.Add(MotorMemoryStateEnum.Controlling, CreateMotorState(cmd));
                    var waitingstate = BeforeBlockHavingMotor(cmd);
                    states.Add(MotorMemoryStateEnum.Waiting, CreateWaitingState(waitingstate.MotorEffector.Device));
                    this.Device.CurrentMemory = MotorMemoryStateEnum.Waiting;
                    this.IsNeededExecution = true;
                    break;
                case MotorMemoryStateEnum.NoEffect:
                    states.Add(MotorMemoryStateEnum.NoEffect, NoEffectState);
                    this.Device.CurrentMemory = MotorMemoryStateEnum.NoEffect;
                    break;
                case MotorMemoryStateEnum.Locked:
                case MotorMemoryStateEnum.Unknown:
                default:
                    throw new InvalidOperationException("invalid mode applied");
                    break;
            }

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
                        DeadTime = 100,
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
            var segment = cmd.Route.Segments[this.ParentBlock];

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
