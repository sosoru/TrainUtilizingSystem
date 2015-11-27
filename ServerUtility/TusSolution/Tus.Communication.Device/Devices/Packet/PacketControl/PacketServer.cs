using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

namespace Tus.Communication
{
    public class PacketServer
        : IDisposable
    {
        private readonly List<PacketServerAction> actionList = new List<PacketServerAction>();

        public ConcurrentQueue<DevicePacket> sending_queue = new ConcurrentQueue<DevicePacket>();
        public ConcurrentQueue<DevicePacket> receving_queue = new ConcurrentQueue<DevicePacket>();
        public readonly PacketDispatcher thisobsv_;
        private bool blockLoopStarting;
        private volatile object lockqueue = new object();
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

        public int QueuingCount
        {
            get
            {
                lock (this.lockqueue)
                {
                    return this.sending_queue == null ? 0 : this.sending_queue.Count;
                }
            }
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
                                         this.receving_queue.Enqueue(pack);
                                     });
            }
        }

        //public IObservable<DevicePacket> SendingObservable
        //{
        //    get
        //    {
        //        return Observable.Defer(sendState)
        //            .SelectMany(Controller.GetWritingPacket);
        //    }
        //}
        public void SendAll()
        {
            var states = sendState();
            foreach (var state in states)
            {
                this.Controller.WritePacket(state);
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

        public virtual void EnqueuePacket(DevicePacket packet)
        {
                sending_queue.Enqueue(packet);
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

        private IEnumerable<DevicePacket> yieldFunc(IEnumerable<DevicePacket> list)
        {
            foreach (var p in list)
            {
                yield return p;
                Thread.Sleep(10);
            }
        }

        private IEnumerable<DevicePacket> sendState()
        {
            if (this.sending_queue.Count == 0)
                return Enumerable.Empty<DevicePacket>();

            var list = new List<DevicePacket>();
            DevicePacket pack;
            while (this.sending_queue.TryDequeue(out pack))
            {
                list.Add(pack);
            }
            return list;
        }

        [Obsolete]
        public void LoopStart(IScheduler scheduler)
        {
            if (!blockLoopStarting)
            {
                blockLoopStarting = true;
                if (!IsLooping)
                {
                    recv_disp = ReceivingObservable.SubscribeOn(scheduler).Subscribe();
       //             send_disp = SendingObservable.SubscribeOn(scheduler).Subscribe();
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