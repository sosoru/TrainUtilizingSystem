using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using SensorLibrary.Packet.IO;
using SensorLibrary.Devices;

namespace SensorLibrary.Packet.Control
{
    public class PacketServer
         : IDisposable
    {
        //protected IDisposable packetObservableDisposable { get; private set; }

        public bool IsLooping { get; private set; }
        public IDeviceIO Controller { get; set; }
        public DeviceFactoryProvider FactoryProvider { get; set; }

        private volatile object lockStream = new object();
        private List<PacketServerAction> actionList = new List<PacketServerAction>();
        private bool cancellation = false;

        public PacketServer(DeviceFactoryProvider factory)
        {
            this.FactoryProvider = factory;

            IsLooping = false;
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

        public void SendPacket(DevicePacket pack)
        {
            lock (lockStream)
                this.Controller.WritePacket(pack);
        }

        private bool blockLoopStarting = false;
        public void LoopStart()
        {
            if (!blockLoopStarting)
            {
                blockLoopStarting = true;
                if (!IsLooping)
                    Task.Factory.StartNew(() => ListeningLoop());

                blockLoopStarting = false;
            }
        }

        public void LoopStop()
        {
            this.cancellation = true;
        }

        private void avr_sensor_spliter(DevicePacket packet)
        {
            for(int i=0; i<8; i++)
            {
                var data = new SensorLibrary.Packet.Data.SensorData()
                               {
                                   VoltageOn = packet.Data[i*2],
                                   VoltageOff = packet.Data[i*2 + 1],
                               };
                var state = new SensorLibrary.Devices.TusAvrDevices.SensorState()
                                {
                                    Data = data,
                                };
                state.FlushDataState();

                state.BasePacket.ID = packet.ID;
                state.BasePacket.ID.InternalAddr += (byte)i;
                state.ReceivingServer = this;
                this.actionList.ForEach(a => a.Act(state));
            }
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

                        var f = this.FactoryProvider.AvailableDeviceTypes.FirstOrDefault(a => a.ModuleType == pack.ModuleType);
                        if (f != null)
                        {
                            if (pack.ModuleType == ModuleTypeEnum.AvrSensor)
                            {
                                this.avr_sensor_spliter(pack);
                            }
                            else
                            {
                                var state = f.DeviceStateCreate();
                                var data = f.DeviceDataCreate();

                                state.BasePacket = pack;
                                state.ReceivingServer = this;

                                //Console.WriteLine(state.ToString());

                                this.actionList.ForEach((item) => item.Act(state));
                            }
                        }
                        else
                        {
                            Console.WriteLine("pero");
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
