﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

using Tus.Communication;

namespace Tus.Communication.Device.AvrComposed
{
    public class Motor
        : Device<MotorState>, ISensorDevice
    {
        public Motor()
            : base(ModuleTypeEnum.AvrMotor, new MotorState())
        {
            this.States = new Dictionary<MotorMemoryStateEnum, MotorState>();
        }

        public Motor(PacketServer server)
            : this()
        {
            this.ReceivingServer = server;
        }

        protected Motor(Motor mtr, MotorState state)
            : this()
        {
            this.CurrentState = state;
            this.DeviceID = mtr.DeviceID;
        }

        public bool IsDetected
        {
            get
            {
                return this.CurrentState.Current > this.CurrentState.ThresholdCurrent;
            }
        }

        public IDictionary<MotorMemoryStateEnum, MotorState> States
        {
            get;
            set;
        }

        private Kernel deviceKernel_ = null;
        public Kernel DeviceKernel
        {
            get
            {
                if (this.deviceKernel_ == null)
                    this.deviceKernel_ = new Kernel()
                    {
                        DeviceID = this.DeviceID,
                        ReceivingServer = this.ReceivingServer
                    };

                return this.deviceKernel_;
            }
        }

        public IEnumerable<DevicePacket> ChangeMemoryTo(MotorMemoryStateEnum mem)
        {
            var kernel = Kernel.MemoryState(this.DeviceID, new MemoryState((int)mem));
            var devp = PacketExtension.CreatePackedPacket(kernel);

            return devp;
        }

        private IEnumerable<DevicePacket> CreateApplyingStates()
        {
            var statelist = new List<IDevice<IDeviceState<IPacketDeviceData>>>();

            foreach (var state in States.OrderBy(k => k.Key == CurrentMemory))
            {
                statelist.Add(Kernel.MemoryState(this.DeviceID, new MemoryState((int)state.Key)));
                statelist.Add(new Motor(this, state.Value));
            }

            return PacketExtension.CreatePackedPacket(statelist);

        }

        public override void Observe(IObservable<IDeviceState<IPacketDeviceData>> observable)
        {
            base.Observe(observable);

            this.DeviceKernel.Observe(observable);
        }

        public override void SendState()
        {
            if (this.CurrentMemory == MotorMemoryStateEnum.Unknown)
                this.CurrentMemory = MotorMemoryStateEnum.NoEffect;

            var app = this.CreateApplyingStates();
            foreach (var p in app)
                this.ReceivingServer.EnqueuePacket(p);

            //var pack = this.ChangeMemoryTo(this.CurrentMemory);
            //foreach (var p in pack)
            //    this.ReceivingServer.SendPacket(p);
        }

        public MotorMemoryStateEnum CurrentMemory
        {
            get
            {
                if (!(this.DeviceKernel.CurrentState.Command == KernelCommand.MemoryState ))
                    return MotorMemoryStateEnum.Unknown;

                return (MotorMemoryStateEnum)this.DeviceKernel.CurrentState.Data.Content1;
            }
            set
            {
                var memstate = new MemoryState((int)value);

                this.DeviceKernel.CurrentState = memstate;
            }
        }

    }

    public enum MotorMemoryStateEnum
        : byte
    {
        NoEffect = 0x01,
        Controlling = 0x02,
        Waiting = 0x03,
        Locked = 0x04,
        Unknown = 0x00,
    }
}