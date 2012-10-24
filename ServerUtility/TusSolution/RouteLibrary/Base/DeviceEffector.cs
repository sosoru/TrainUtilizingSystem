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
            if ((DateTime.Now - before_send).Seconds > 5 ||
                (this.BeforeState == null
                || !CheckAll(this.BeforeState, this.Device.CurrentState as TState)))
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

        public abstract void ApplyCommand(CommandInfo cmd);

        public TState BeforeState { get; private set; }
        public abstract TState DefaultState { get; }

        protected bool CheckBefore(Func<TState, object> f)
        {
            return this.BeforeState == null
                || f(this.BeforeState).Equals(f((TState)this.Device.CurrentState));
        }

        protected virtual bool CheckAll(TState a, TState b)
        {
            return false;
        }
    }

    public class MotorEffector
        : DeviceEffector<Motor, MotorInfo, MotorState>
    {
        public MotorEffector(MotorInfo info, Block block)
            : base(info, block)
        {
        }

        private MotorState default_state;
        public override MotorState DefaultState
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

        public override void ApplyCommand(CommandInfo cmd)
        {
            //if (this.CheckBefore(s => s.Direction))
            //{
            var locked = cmd.Route.LockedSegments;

            if (!locked.ContainsKey(this.ParentBlock))
            {
                if (cmd.AnyToDefault)
                {
                    this.Device.CurrentState.Data = this.DefaultState.Data;
                    //this.Device.CurrentState.FlushDataState();

                }
                return;
            }

            var seg = locked[this.ParentBlock];

            if ((seg.IsFromAny || seg.From.Name == this.Info.RoutePositive.From.Name)
                    && (seg.IsToAny || seg.To.Name == this.Info.RoutePositive.To.Name))
            {
                this.Device.CurrentState.Direction = MotorDirection.Positive;
            }
            else if ((seg.IsFromAny || seg.From.Name == this.Info.RouteNegative.From.Name)
                        && (seg.IsToAny || seg.To.Name == this.Info.RouteNegative.To.Name))
            {
                this.Device.CurrentState.Direction = MotorDirection.Negative;
            }
            else
            {
                this.Device.CurrentState.Direction = MotorDirection.Standby;
            }
            //}

            //if (this.CheckBefore(s => s.Duty))
            //{
            this.Device.CurrentState.Duty = cmd.Speed;
            //}
        }

        protected override bool CheckAll(MotorState a, MotorState b)
        {
            return a.Data.Direction == b.Data.Direction && a.Data.Duty == b.Data.Duty;
        }
    }

    public class SwitchEffector
        : DeviceEffector<Switch, SwitchInfo, SwitchState>
    {
        public SwitchEffector(SwitchInfo info, Block block)
            : base(info, block) { }

        private SwitchState default_state;
        public override SwitchState DefaultState
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
        }
        protected override bool CheckAll(SwitchState a, SwitchState b)
        {
            return a.Data.Position == b.Data.Position;
        }
    }

}
