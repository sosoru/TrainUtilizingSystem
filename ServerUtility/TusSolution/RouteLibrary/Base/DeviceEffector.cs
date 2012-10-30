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
        void ApplyCommand(CommandInfo cmd);
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

            this.Device.Observe(block.Sheet.Dispatcher);
        }

        private DateTime before_send;
        public void ExecuteCommand()
        {
            this.Device.CurrentState.ReceivingServer = ParentBlock.Sheet.Server;
            this.Device.SendState();
            this.before_send = DateTime.Now;

            this.IsNeededExecution = false;
        }

        public virtual bool IsNeededExecution { get; set; }
        public abstract void ApplyCommand(CommandInfo cmd);
    }

    public class MotorEffector
        : DeviceEffector<Motor, MotorInfo, MotorState>
    {
        public MotorEffector(MotorInfo info, Block block)
            : base(info, block)
        {
            this.IsNeededExecution = true;
        }

        public MotorState NoEffectState
        {
            get
            {
                var state = new MotorState()
                {
                    ReceivingServer = this.ParentBlock.Sheet.Server,
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

        public MotorState CreateMotorState(CommandInfo cmd)
        {
            MotorDirection dir = MotorDirection.Standby;
            float duty = 0f;
            var locked = cmd.Route.LockedSegments;

            if (!locked.ContainsKey(this.ParentBlock))
            {
                //if (cmd.AnyToDefault)
                //{
                //    this.Device.CurrentState.Data = this.DefaultState.Data;
                //}
                return NoEffectState;
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
            var before = locked.Reverse().SkipWhile(b => b == this.ParentBlock).FirstOrDefault();

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
            var locked = cmd.Route.LockedBlocks.Where(s => s.HasMotor).ToArray();

            if (!locked.Contains(this.ParentBlock))
                return MotorMemoryStateEnum.NoEffect;

            var last = locked.Length - 1;
            var thispos = Array.IndexOf(locked, this.ParentBlock);

            if (thispos == last)
                return MotorMemoryStateEnum.Waiting;
            else if (thispos == last - 1)
                return MotorMemoryStateEnum.Controlling;
            else
                return MotorMemoryStateEnum.Locked;
        }

        private MotorMemoryStateEnum _before_state = MotorMemoryStateEnum.Unknown;
        public override void ApplyCommand(CommandInfo cmd)
        {
            this.IsNeededExecution = true;

            var mode = SelectCurrentMemory(cmd);
            var states = new Dictionary<MotorMemoryStateEnum, Motor>{
                { MotorMemoryStateEnum.Controlling, CreateMotorState(cmd) },
                {MotorMemoryStateEnum.Waiting , CreateWaitingState(BeforeBlockHavingMotor(cmd).MotorEffector.Device)},
                {MotorMemoryStateEnum.NoEffect, NoEffectState},
                {MotorMemoryStateEnum.Locked, LockedState}
            };

            this.Device.States = states;
            this.Device.CurrentMemory = mode;

            // when execution is NOT needed :
                // NoEffect -> NoEfect
            if(!(this._before_state == MotorMemoryStateEnum.NoEffect
                 && mode == MotorMemoryStateEnum.NoEffect))
            {
                this.IsNeededExecution = true;
            }

            _before_state = mode;
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
                        ReceivingServer = this.ParentBlock.Sheet.Server,
                        Position = PointStateEnum.Straight,
                        DeadTime = 100,
                        ChangingTime = 250,
                    };
                return state;
            }
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
    }

}
