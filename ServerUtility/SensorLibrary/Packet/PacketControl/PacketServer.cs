using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.PlatformServices;
using System.Threading.Tasks;

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

        public bool IsLooping { get; private set; }
        public IDeviceIO Controller { get; set; }
        public DeviceFactoryProvider FactoryProvider { get; set; }
        public TaskScheduler Scheduler { get; set; }

        private volatile object lockStream = new object();
        private List<PacketServerAction> actionList = new List<PacketServerAction>();
        private bool cancellation = false;
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
            this.IsLooping = false;
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

        private void DispatchState(IDeviceState<IPacketDeviceData> state)
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

        private bool blockLoopStarting = false;
        public void LoopStart()
        {

            if (!blockLoopStarting)
            {
                blockLoopStarting = true;
                if (!IsLooping)
                {
                    this.IsLooping = true;
                    //todo : stopping
                    Observable.Defer(this.Controller.GetReadingPacket)
                        .SelectMany(packs => packs.ExtractPackedPacket())
                        .Do(pack =>
                                {
                                })
                        .ObserveOn(System.Reactive.Concurrency.NewThreadScheduler.Default)
                        .Repeat()
                        .Subscribe(pack => { }, (Exception ex) => Console.WriteLine(ex.ToString()));

                    Observable
                        .Defer(() => Observable
                                        .Return(((this.sending_queue.Count > 0) ? this.sending_queue.Dequeue() : null))
                                        .Delay(TimeSpan.FromMilliseconds(1))
                                        .SkipWhile(d => d == null)
                                                )
                        .SelectMany(this.Controller.GetWritingPacket)
                        .ObserveOn(System.Reactive.Concurrency.Scheduler.NewThread)
                        .Repeat()
                        .Subscribe(pack => { }, (Exception ex) => Console.WriteLine(ex.ToString()));

                }
                //     Task.Factory.StartNew(() => ListeningLoop());

                blockLoopStarting = false;
            }
        }

        public void LoopStop()
        {
            this.cancellation = true;
        }

        private void ListeningLoop()
        {
            this.IsLooping = true;
            while (!cancellation)
            {
                try
                {

                    DevicePacket pack = new DevicePacket();
                    try
                    {
                        lock (lockStream)
                            pack = this.Controller.ReadPacket();

                        if (pack == null)
                        {
                            System.Threading.Thread.Sleep(1);
                            continue;
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        if (pack != null)
                            Console.WriteLine(pack.ToString());
                    }
                }
                catch (EndOfStreamException)
                {
                    Console.WriteLine("stream closed");
                }
            }
            this.IsLooping = false;
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
