using System;
using System.Collections.Generic;
using System.Linq;
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
        IEnumerable<TDev> Devices { get; }
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
        private DateTime before_send;

        public DeviceEffector(TInfo info, Block block)
        {
            Info = info;
            ParentBlock = block;

            Devices = info.Addresses.Select(id => new TDev
                                                      {
                                                          DeviceID = id,
                                                          ReceivingServer = ParentBlock.Sheet.Server,
                                                      }).ToArray();


            Devices.ForEach(dev => dev.Observe(block.Sheet.Dispatcher));
            IsNeededExecution = false;
        }

        public Block ParentBlock { get; private set; }
        public TInfo Info { get; private set; }
        public IEnumerable<TDev> Devices { get; private set; }

        public void ExecuteCommand()
        {
            Devices.ForEach(dev =>
                                {
                                    dev.ReceivingServer = ParentBlock.Sheet.Server;
                                    dev.SendState();
                                });
            before_send = DateTime.Now;

            IsNeededExecution = false;
        }

        public virtual bool IsNeededExecution { get; set; }
        public abstract void ApplyCommand(CommandFactory factory);
    }

    internal static class BlockExtension
    {
        public static Block BeforeBlockHavingMotor(this Block blk, Route route)
        {
            IEnumerable<Block> locked = route.LockedBlocks.Where(s => s.HasMotor);
            Block before = locked.Reverse().SkipWhile(b => b == blk).FirstOrDefault();

            return before;
        }

        public static Block NextBlockHavingMotor(this Block blk, Route route)
        {
            IEnumerable<Block> locked = route.LockedBlocks.Where(s => s.HasMotor);
            Block next = locked.SkipWhile(s => s == blk).FirstOrDefault();
            return next;
        }
    }

    public class MotorEffector
        : DeviceEffector<Motor, MotorInfo, MotorState>
    {
        private MotorState _before_mtr_state;
        private MotorMemoryStateEnum _before_state = MotorMemoryStateEnum.Unknown;

        public MotorEffector(MotorInfo info, Block block)
            : base(info, block)
        {
        }

        #region "Create State Methods"

        private MotorDirection _before_dir = MotorDirection.Standby;
        private float _before_duty = 2.0f;

        private MotorState CreateMotorState(MotorDirection dir, float duty)
        {
            var state = new MotorState
                            {
                                ControlMode = MotorControlMode.DutySpecifiedMode,
                                Duty = duty,
                                Direction = dir,
                            };

            if (_before_dir != dir || _before_duty != duty)
                IsNeededExecution = true;

            _before_dir = dir;
            _before_duty = duty;

            return state;
        }

        private MotorState CreateMotorState(CommandInfo cmd)
        {
            var dir = MotorDirection.Standby;
            float duty = 0f;
            ControlUnit locked = cmd.Route.RouteOrder.GetControlingUnit(ParentBlock);

            if (locked == null)
            {
                return CreateNoEffectState();
            }

            RouteSegment seg = cmd.Route.RouteOrder.GetSegment(ParentBlock);
            dir = Info.SelectDirection(seg.Info);
            duty = cmd.Speed;

            return CreateMotorState(dir, duty);
        }

        // TODO : check whether CreateState() and SetState() should be static method or not
        static private MotorState CreateNoEffectState()
        {
            var state = new MotorState
                            {
                                Direction = MotorDirection.Positive,
                                Duty = 0,
                                ControlMode = MotorControlMode.DutySpecifiedMode,
                            };
            return state;
        }

        private MotorState CreateLockedState()
        {
            var state = new MotorState
                            {
                                Direction = MotorDirection.Standby,
                                Duty = 0,
                                ControlMode = MotorControlMode.DutySpecifiedMode,
                            };
            return state;
        }

        private MotorState CreateWaitingState(BlockPolar polar, Motor beforemtr)
        {
            float threshold = 0.05f;
            MotorState state;
            if (polar == BlockPolar.Positive)
            {
                state = new MotorState
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
                state = new MotorState
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

        public override bool IsNeededExecution { get; set; }

        public void SetDetectingMode(float duty)
        {
            var mode = MotorMemoryStateEnum.Controlling;
            var states = new Dictionary<MotorMemoryStateEnum, MotorState>
                             {
                                 {mode, CreateMotorState(MotorDirection.Positive, duty)}
                             };
            Devices.ForEach(dev =>
                                {
                                    dev.States = states;
                                    dev.CurrentMemory = mode;
                                });

            PacketExtension.CreatePackedPacket(Devices).Send(ParentBlock.Sheet.Server);
        }

        static internal void SetNoEffectMode(IEnumerable<Motor> mtrs)
        {
            var mode = MotorMemoryStateEnum.NoEffect;
            var states = new Dictionary<MotorMemoryStateEnum, MotorState>
                             {
                                 {mode, CreateNoEffectState()}
                             };
            mtrs.ForEach(dev =>
                    {
                        dev.States = states;
                        dev.CurrentMemory = mode;
                    });

        }

        public void SetNoEffectMode()
        {
            SetNoEffectMode(this.Devices);
        }

        public override void ApplyCommand(CommandFactory factory)
        {
            if (IsNeededExecution)
                return;

            if (_before_mtr_state == null)
                _before_mtr_state = new MotorState();

            CommandInfo cmd = factory.CreateCommand(ParentBlock);
            var states = new Dictionary<MotorMemoryStateEnum, MotorState>();

            switch (cmd.MotorMode)
            {
                case MotorMemoryStateEnum.Controlling:
                    //if (_before_state != MotorMemoryStateEnum.Controlling)
                    //{
                    states.Add(MotorMemoryStateEnum.Locked, CreateLockedState());
                    //}
                    states.Add(MotorMemoryStateEnum.Controlling, CreateMotorState(cmd));
                    IsNeededExecution = true;
                    break;
                case MotorMemoryStateEnum.Waiting:
                    BlockPolar polar = cmd.Route.RouteOrder.Polar;
                    MotorState cntstate = CreateMotorState(cmd);
                    Block waitingblock = ParentBlock.BeforeBlockHavingMotor(cmd.Route);

                    states.Add(MotorMemoryStateEnum.Waiting,
                               CreateWaitingState(polar, waitingblock.MotorEffector.Devices.First()));
                    if (polar == BlockPolar.Positive)
                    {
                        states.Add(MotorMemoryStateEnum.Locked, CreateLockedState());
                        this.Devices.ForEach(m => m.ModeAfterWaiting = MotorMemoryStateEnum.Locked);
                    }
                    else
                    {
                        states.Add(MotorMemoryStateEnum.Controlling, CreateMotorState(cmd));
                        this.Devices.ForEach(m => m.ModeAfterWaiting = MotorMemoryStateEnum.Controlling);
                    }

                    //if (this.Device.States.ContainsKey(MotorMemoryStateEnum.Controlling) &&
                    //    this.Device.States[MotorMemoryStateEnum.Controlling].Duty == cntstate.Duty)
                    //{
                    //    this.IsNeededExecution = false;
                    //}
                    //else
                    //{
                    //    this.IsNeededExecution = true;
                    //}
                    IsNeededExecution = true;

                    break;
                case MotorMemoryStateEnum.Locked:
                    states.Add(MotorMemoryStateEnum.Locked, CreateLockedState());
                    states.Add(MotorMemoryStateEnum.Controlling, CreateMotorState(cmd));
                    IsNeededExecution = true;
                    break;
                case MotorMemoryStateEnum.NoEffect:
                    states.Add(MotorMemoryStateEnum.NoEffect, CreateNoEffectState());
                    IsNeededExecution = true;
                    break;

                case MotorMemoryStateEnum.Unknown:
                default:
                    throw new InvalidOperationException("invalid mode applied");
            }

            Devices.ForEach(dev =>
                                {
                                    dev.States = states;
                                    dev.CurrentMemory = cmd.MotorMode;
                                });

            if (_before_state != cmd.MotorMode)
                IsNeededExecution = true;

            _before_state = cmd.MotorMode;
        }
    }

    public class SwitchEffector
        : DeviceEffector<Switch, SwitchInfo, SwitchState>
    {
        private SwitchState default_state;

        public SwitchEffector(SwitchInfo info, Block block)
            : base(info, block)
        {
            if (info.PositionReversed != null)
            {
                foreach (var sw in this.Devices)
                    if (info.PositionReversed.Contains(sw.DeviceID))
                        sw.PositionReversed = true;
            }
        }

        public SwitchState DefaultState
        {
            get
            {
                var state = new SwitchState
                                {
                                    Position = PointStateEnum.Straight,
                                    DeadTime = 300,
                                    ChangingTime = 250,
                                };
                return state;
            }
        }

        //private PointStateEnum _before_position = PointStateEnum.Any;
        public override void ApplyCommand(CommandFactory factory)
        {
            if (IsNeededExecution)
                return;

            CommandInfo cmd = factory.CreateCommand(ParentBlock);
            if (cmd.Route == null)
                return;

            RouteSegment segment = cmd.Route.RouteOrder.GetSegment(ParentBlock);

            Devices.ForEach(dev =>
                                {
                                    if ((segment.IsFromAny ||
                                         Info.DirStraight.Any(i => i.From.Name == segment.From.Name))
                                        &&
                                        (segment.IsToAny ||
                                         Info.DirStraight.Any(i => i.To.Name == segment.To.Name)))
                                    {
                                        dev.CurrentState.Position = PointStateEnum.Straight;
                                    }
                                    else if ((segment.IsFromAny ||
                                              Info.DirCurved.Any(i => i.From.Name == segment.From.Name))
                                             &&
                                             (segment.IsToAny ||
                                              Info.DirCurved.Any(i => i.To.Name == segment.To.Name)))
                                    {
                                        dev.CurrentState.Position = PointStateEnum.Curve;
                                    }
                                    else
                                    {
                                        dev.CurrentState.Position = PointStateEnum.Any;
                                    }
                                });
            //if (_before_position != this.Device.CurrentState.Position)
            //    this.IsNeededExecution = true;

            //_before_position = this.Device.CurrentState.Position;
        }
    }
}