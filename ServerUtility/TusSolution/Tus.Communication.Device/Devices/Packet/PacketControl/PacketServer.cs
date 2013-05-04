using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Tus.Communication
{
    public class PacketServer
        : IDisposable
    {
        private readonly List<PacketServerAction> actionList = new List<PacketServerAction>();

        private readonly List<DevicePacket> sending_queue = new List<DevicePacket>(128);
        private readonly PacketDispatcher thisobsv_;
        private bool blockLoopStarting;
        private volatile object lockStream = new object();
        private IDisposable recv_disp;
        private IDisposable send_disp;

        public PacketServer()
        {
            thisobsv_ = new PacketDispatcher();

            AddAction(thisobsv_);
        }

        public bool IsLooping
        {
            get { return recv_disp != null || send_disp != null; }
        }

        public IDeviceIO Controller { get; set; }

        public IObservable<DevicePacket> ReceivingObservable
        {
            get
            {
                return Observable.Defer(Controller.GetReadingPacket)
                                 .Do(
                                     pack =>
                                     {
                                         actionList.ForEach(
                                             (item) => pack.ExtractPackedPacket().ToList().ForEach(s => item.Act(s)));
                                     });
            }
        }

        public IObservable<DevicePacket> SendingObservable
        {
            get
            {
                return Observable.Defer(SendState)
                                 .SelectMany(Controller.GetWritingPacket);
            }
        }

        public IObservable<IDeviceState<IPacketDeviceData>> GetDispatcher()
        {
            return thisobsv_;
        }

        public PacketServerAction AddAction(PacketDispatcher dispatcher)
        {
            return AddAction((state) => dispatcher.Notify(state));
        }

        public PacketServerAction AddAction(Action<IDeviceState<IPacketDeviceData>> act)
        {
            var inst = new PacketServerAction(act);
            AddAction(inst);
            return inst;
        }

        public PacketServerAction AddAction(PacketServerAction act)
        {
            if (!actionList.Contains(act) && act != null)
                actionList.Add(act);

            return act;
        }

        public void RemoveAction(PacketServerAction act)
        {
            actionList.Remove(act);
        }

        private object enqueuelock = new object();
        public virtual void EnqueuePacket(DevicePacket packet)
        {
            lock (enqueuelock)
            {
                var duplicated =
                    sending_queue.FirstOrDefault(
                        pack => pack != null && pack.ID.ModuleAddr == packet.ID.ModuleAddr && pack.ID.ParentPart == packet.ID.ParentPart);

                if (duplicated != null)
                {
                    this.sending_queue.Remove(duplicated);
                }
                this.sending_queue.Add(packet);

            }
        }

        public virtual void EnqueueState(IDevice<IDeviceState<IPacketDeviceData>> dev)
        {
            //todo : thread control (reading and writing on the same thread)
            //lock (lockStream)

            try
            {
                foreach (DevicePacket p in PacketExtension.CreatePackedPacket(dev))
                    EnqueuePacket(p);
            }
            catch (ArgumentException)
            {
            }
        }

        public void DispatchState(IDeviceState<IPacketDeviceData> state)
        {
            actionList.ForEach((item) => item.Act(state));
        }

        private IObservable<DevicePacket> SendState()
        {
            lock (enqueuelock)
            {
                if (sending_queue.Count > 0)
                {
                    var packets = sending_queue.ToArray();
                    this.sending_queue.Clear();
                    return packets.ToObservable();

                }
                else
                    return Observable.Empty<DevicePacket>();

            }
        }

        public void LoopStart(IScheduler scheduler)
        {
            if (!blockLoopStarting)
            {
                blockLoopStarting = true;
                if (!IsLooping)
                {
                    recv_disp = ReceivingObservable.SubscribeOn(scheduler).Subscribe();
                    send_disp = SendingObservable.SubscribeOn(scheduler).Subscribe();
                }
                blockLoopStarting = false;
            }
        }

        public void LoopStop()
        {
            if (recv_disp != null)
                recv_disp.Dispose();

            if (send_disp != null)
                send_disp.Dispose();
        }

        #region Dispose-Finalize Pattern

        private bool __disposed;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (__disposed) return;
            if (disposing)
            {
            }

            LoopStop();
            __disposed = true;
        }

        ~PacketServer()
        {
            Dispose(false);
        }

        #endregion
    }

    public class PacketServerAction
    {
        private readonly Action<IDeviceState<IPacketDeviceData>> act;

        public PacketServerAction(Action<IDeviceState<IPacketDeviceData>> act)
        {
            this.act = act;
        }

        public void Act(IDeviceState<IPacketDeviceData> state)
        {
            act(state);
        }
    }
}