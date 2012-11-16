using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.PlatformServices;
using System.Threading.Tasks;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;

using SensorLibrary.Packet.IO;
using SensorLibrary.Devices;
using SensorLibrary.Packet.Data;
using SensorLibrary.Devices.TusAvrDevices;

namespace SensorLibrary.Packet.Control
{
    public class PacketServer
         : IDisposable
    {
        //protected IDisposable packetObservableDisposable { get; private set; }

        public bool IsLooping { get { return recv_disp != null || send_disp != null; } }
        public IDeviceIO Controller { get; set; }
        public DeviceFactoryProvider FactoryProvider { get; set; }

        private IDisposable recv_disp = null;
        private IDisposable send_disp = null;

        private volatile object lockStream = new object();
        private List<PacketServerAction> actionList = new List<PacketServerAction>();
        private PacketDispatcher thisobsv_;

        private Queue<DevicePacket> sending_queue = new Queue<DevicePacket>();

        public PacketServer(DeviceFactoryProvider factory)
            : this()
        {
            this.FactoryProvider = factory;
            this.thisobsv_ = new PacketDispatcher();

            this.AddAction(this.thisobsv_);
        }

        public PacketServer()
        {
        }

        public IObservable<IDeviceState<IPacketDeviceData>> GetDispatcher()
        {
            return thisobsv_;
        }

        public PacketServerAction AddAction(PacketDispatcher dispatcher)
        {
            return this.AddAction((state) => dispatcher.Notify(state));
        }

        public PacketServerAction AddAction(Action<IDeviceState<IPacketDeviceData>> act)
        {
            var inst = new PacketServerAction(act);
            this.AddAction(inst);
            return inst;
        }

        public PacketServerAction AddAction(PacketServerAction act)
        {
            if (!actionList.Contains(act) && act != null)
                this.actionList.Add(act);

            return act;
        }

        public void RemoveAction(PacketServerAction act)
        {
            this.actionList.Remove(act);
        }

        public virtual void SendPacket(DevicePacket packet)
        {
            this.sending_queue.Enqueue(packet);
        }

        public virtual void SendState(IDevice<IDeviceState<IPacketDeviceData>> dev)
        {
            //todo : thread control (reading and writing on the same thread)
            //lock (lockStream)

            try
            {
                foreach (var p in DevicePacket.CreatePackedPacket(dev))
                    this.SendPacket(p);
            }
            catch (ArgumentException)
            {
            }
        }

        public  void DispatchState(IDeviceState<IPacketDeviceData> state)
        {
            var f =
                this.FactoryProvider.AvailableDeviceTypes.FirstOrDefault(
                    a => a.ModuleType == state.ModuleType);
            if (f != null)
            {
                //var state = f.DeviceStateCreate();
                //var data = f.DeviceDataCreate();

                //state.ReceivingServer = this;

                this.actionList.ForEach((item) => item.Act(state));
            }
            else
            {
                Console.WriteLine("pero");
            }

        }

        private IObservable<DevicePacket> SendState()
        {
            //var sub = new ReplaySubject<DevicePacket>();
            //while (this.sending_queue.Count > 0)
            //{
            //    sub.OnNext(this.sending_queue.Dequeue());
            //}
            //sub.OnCompleted();

            if (this.sending_queue.Count > 0)
                return new[] { this.sending_queue.Dequeue() }.ToObservable();
            else
                return Observable.Empty<DevicePacket>();

        }

        public IObservable<DevicePacket> ReceivingObservable
        {
            get
            {
                return Observable.Defer(this.Controller.GetReadingPacket)
                        .Do(pack =>
                        {
                            //var f =
                            //    this.FactoryProvider.AvailableDeviceTypes.FirstOrDefault(
                            //        a => a.ModuleType == pack.ModuleType);
                            //if (f != null)
                            //{
                                //var state = f.DeviceStateCreate();
                                //var data = f.DeviceDataCreate();

                                //state.ReceivingServer = this;

                                this.actionList.ForEach((item) => pack.ExtractPackedPacket().ToList().ForEach(s => item.Act(s)));
                            //}
                            //else
                            //{
                            //    Console.WriteLine("pero");
                            //}
                        });
            }
        }

        public IObservable<DevicePacket> SendingObservable
        {
            get
            {
                return Observable.Defer(SendState)
                        .SelectMany(this.Controller.GetWritingPacket);
            }
        }

        private bool blockLoopStarting = false;
        public void LoopStart(IScheduler scheduler)
        {
            if (!blockLoopStarting)
            {
                blockLoopStarting = true;
                if (!IsLooping)
                {
                    this.recv_disp = this.ReceivingObservable.SubscribeOn(scheduler).Subscribe();
                    this.send_disp = this.SendingObservable.SubscribeOn(scheduler).Subscribe();
                }
                blockLoopStarting = false;
            }
        }

        public void LoopStop()
        {
            if (this.recv_disp != null)
                this.recv_disp.Dispose();

            if (this.send_disp != null)
                this.send_disp.Dispose();
        }


        #region Dispose-Finalize Pattern
        private bool __disposed = false;
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (__disposed) return;
            if (disposing)
            {

            }

            //if (this.Controller != null)
            //{
            //    try
            //    {
            //        this.Controller
            //    }
            //    catch (IOException)
            //    { }
            //}

            //base.Dispose();
            this.LoopStop();
            __disposed = true;
        }

        ~PacketServer()
        {
            this.Dispose(false);
        }
        #endregion

    }

    public class PacketServerAction
    {
        private Action<IDeviceState<IPacketDeviceData>> act;
        public PacketServerAction(Action<IDeviceState<IPacketDeviceData>> act)
        {
            this.act = act;
        }
        public void Act(IDeviceState<IPacketDeviceData> state)
        {
            this.act(state);
        }
    }
}
