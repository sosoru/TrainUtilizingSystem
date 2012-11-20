using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Threading;

using SensorLibrary;
using SensorLibrary.Packet;
using SensorLibrary.Packet.Control;
using SensorLibrary.Packet.Data;
using SensorLibrary.Devices;
using SensorLibrary.Devices.TusAvrDevices;
using SensorLibrary.Packet.IO;

using RouteLibrary;
using RouteLibrary.Base;
using RouteLibrary.Parser;

namespace DialogConsole
{
    class MainClass
    {
        static void Main(string[] args)
        {
            var ipbase = IPAddress.Parse("192.168.2.0");
            var ipmask = IPAddress.Parse("255.255.255.0");

            var dialog = new DialogCnosole();
            var io = new TusEthernetIO(ipbase, ipmask)
            {
                SourceID = new DeviceID(9, 0, 0),
                Port = 8000,
            };
            dialog.InitSheet(io);

            dialog.Loop();
        }
    }

    public class DialogCnosole
    {
        public PacketServer Server { get; set; }
        public BlockSheet Sheet { get; set; }

        public IList<Vehicle> Vehicles { get; set; }

        private IScheduler SchedulerPacketProcessing;
        private IScheduler SchedulerSendingProcessing;
        private SynchronizationContext SyncNetwork;
        private SynchronizationContext SyncPacketProcess;

        private IDisposable VehicleProcessing_;
        private IDisposable Sending_;
        private IDisposable Receiving_;
        private IDisposable ServingInfomation_;

        private object lock_writer = new object();
        private StreamWriter log_writer_;
        private StreamWriter LogWriter
        {
            get
            {
                lock (this.lock_writer)
                {
                    return this.log_writer_;
                }
            }
            set
            {
                lock (this.lock_writer)
                {
                    this.log_writer_ = value;
                }
            }
        }

        public void InitSheet(IDeviceIO io)
        {
            this.Server = CreateServer();
            this.Sheet = CreateSheet("test_looping.yaml", this.Server);
            this.Sheet.AssociatedScheduler = this.SchedulerPacketProcessing;

            this.Server.Controller = io;
            this.Vehicles = new List<Vehicle>();

            this.LogWriter = new StreamWriter("packet_log.txt");
        }

        public IEnumerable<string> routesnames
        {
            get
            {
                var loop = new[] { "AT1", "BAT1", "AT2", "BAT2", "AT3", "BAT3", "AT4", "BAT4" };

                return Enumerable.Range(1, 1)
                    .SelectMany(i => loop);
            }
        }

        public Route LoopingRoute
        {
            get { return new Route(this.Sheet, routesnames); }
        }

        public void Loop()
        {
            this.SyncNetwork = new SynchronizationContext();
            this.SyncPacketProcess = new SynchronizationContext();

            this.SchedulerSendingProcessing = new SynchronizationContextScheduler(this.SyncNetwork);
            this.SchedulerPacketProcessing = new SynchronizationContextScheduler(this.SyncPacketProcess);

            this.Sheet.AssociatedScheduler = this.SchedulerPacketProcessing;

            this.Server.SendingObservable
                .Delay(TimeSpan.FromMilliseconds(15))
                .Repeat()
                .SelectMany(g => g.ExtractPackedPacket())
                //.Do(g => this.LogWriter.WriteLine(string.Format("({0}.{1}) : sending {2}",
                //                    DateTime.Now.ToLongTimeString(),
                //                    DateTime.Now.Millisecond,
                //                    g.ToString()
                //                    )))
                .ObserveOn(this.SchedulerSendingProcessing)
                .SubscribeOn(Scheduler.NewThread)
                .Subscribe();

            var timer = Observable.Interval(TimeSpan.FromMilliseconds(20), Scheduler.NewThread);
            this.Receiving_ = Observable.Defer(() => this.Server.ReceivingObservable)
                .ObserveOn(this.SchedulerSendingProcessing)
                .Repeat()
                .Zip(timer, (v, _) => v)
                .SelectMany(v => v.ExtractPackedPacket())
                //.Do(g => this.LogWriter.WriteLine(string.Format("({0}.{1}) : recving {2}",
                //                                                    DateTime.Now.ToLongTimeString(),
                //                                                    DateTime.Now.Millisecond,
                //                                                    g.ToString()
                //                                                    )))
                                                            .SubscribeOn(Scheduler.NewThread)
                                                            .Subscribe();
            this.ServingInfomation_ = Observable.Defer(() => this.GetHttpObservable())
                                                .ObserveOn(this.SchedulerPacketProcessing)
                                                .Repeat()
                                                .SubscribeOn(Scheduler.NewThread)
                                                .Subscribe();


            while (true)
            {
                Console.WriteLine("1 : show statuses");
                Console.WriteLine("3 : detect test");
                Console.WriteLine("4 : input command");
                Console.WriteLine("5 : monitoring vehicles");
                //Console.WriteLine("5 : apply command");
                Console.WriteLine();

                //Console.WriteLine(cmdinfo.Speed);
                //Console.WriteLine(cmdinfo.Route.Blocks
                //   .Where(b => b.HasMotor && b.IsBlocked)
                //    .Aggregate("", (ac, b) => ac += b.Name + ", "));

                var cmd = Console.ReadLine();

                try
                {
                    switch (cmd)
                    {
                        case "1":
                            ShowStatus(this.Sheet);
                            break;
                        case "3":
                            Detect(this.Sheet);
                            break;
                        //case "4":
                        //    InputCommand(this.Sheet, cmdinfo);
                        //    break;
                        case "5":
                            InputVehicleMonitoring();
                            break;
                        default:
                            Console.WriteLine("parse error");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

        public void InputCommand(BlockSheet sht, CommandInfo info)
        {
            info.Route = InputRoute(sht, info.Route);

            Console.WriteLine("Duty? [0-1]");
            var duty = Console.ReadLine();

            info.Speed = float.Parse(duty);
        }

        public string ShowStatus(BlockSheet sht)
        {
            var blocks = sht.InnerBlocks;

            sht.InquiryAllMotors();
            sht.InquiryDevices(sht.AllSwitches());


            foreach (var b in blocks)
                Console.WriteLine(b.ToString());
            return blocks.Select(b => b.ToString() + "\n")
                            .Aggregate("", (ag, s) => ag += s);
        }

        public void Detect(BlockSheet sht)
        {
            sht.ChangeDetectingMode();
            System.Threading.Thread.Sleep(1000);

            sht.InquiryAllMotors();
            System.Threading.Thread.Sleep(2000);


            Console.WriteLine(sht.InnerBlocks
               .Where(b => b.IsDetectingTrain || b.IsMotorDetectingTrain)
                .Aggregate("", (ac, b) => ac += b.Name + ", "));
        }

        public Route InputRoute(BlockSheet sht, Route before)
        {
            Console.WriteLine("route?");
            var content = Console.ReadLine();

            if (content == "")
                return before;

            try
            {
                var spil = content.Split(',');
                var rt = new Route(sht, spil.Select(s => sht.GetBlock(s.Trim()).Name));

                return rt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return before;
            }
        }

        public void InputVehicleMonitoring()
        {
            this.Sheet.Effect(new CommandFactory()
            {
                CreateCommand = b => new CommandInfo() { MotorMode = MotorMemoryStateEnum.NoEffect, }
            },
            this.Sheet.InnerBlocks);
            Console.WriteLine("Which block your vehicle halt on?");

            var bk = this.Sheet.GetBlock(Console.ReadLine());

            this.Vehicles.Clear();
            CreateVehicle(bk);

            this.VehicleProcessing_ = Observable.Defer(() => Observable.Start(VehicleProcess, this.SchedulerSendingProcessing))
                .Do(u => this.Sheet.InquiryAllMotors())
                .Delay(TimeSpan.FromMilliseconds(500))
                .Repeat()
                .SubscribeOn(Scheduler.NewThread)
                .Subscribe();

        }

        public PacketServer CreateServer()
        {
            var serv = new PacketServer(new AvrDeviceFactoryProvider());

            return serv;
        }

        public BlockSheet CreateSheet(string path, PacketServer serv)
        {
            var parser = new BlockYaml();
            var blocks = parser.Parse(path);

            return new BlockSheet(blocks, serv);
        }

        public void CreateVehicle(Block b)
        {
            if (this.VehicleProcessing_ != null)
                this.VehicleProcessing_.Dispose();

            foreach (var vh in this.Vehicles.ToArray())
            {
                vh.Route.InitLockingPosition();
            }
            this.Vehicles.Clear();

            var rt = this.LoopingRoute;
            rt.IsRepeatable = true;
            var v = new Vehicle(this.Sheet, rt);
            v.CurrentBlock = b;
            v.Speed = 0.5f;

            this.Vehicles.Add(v);
        }

        public void VehicleProcess()
        {
            foreach (var v in this.Vehicles)
            {
                v.Refresh();
            }
        }

        [DataContract]
        private class VehicleInfoReceived
        {
            [DataMember(IsRequired = true)]
            public string Name;

            [DataMember(IsRequired = false)]
            public string Speed;

            [DataMember(IsRequired = false)]
            public string RouteName;

            [DataMember(IsRequired = false)]
            public ICollection<string> Halts;

        }

        private void FillVehicleInfoResponse(HttpListenerContext r)
        {
            var res = r.Response;

            res.Headers.Add("Content-type: application/json");
            res.Headers.Add("Access-Control-Allow-Headers: origin, x-requested-with, accept");
            res.Headers.Add("Access-Control-Allow-Origin: null");
            using (var sw = new StreamWriter(res.OutputStream))
            using (var ms = new MemoryStream())
            {
                var cnt = new DataContractJsonSerializer(typeof(IEnumerable<Vehicle>));
                var vehis = this.Vehicles.ToArray();

                cnt.WriteObject(ms, vehis);

                sw.WriteLine(System.Text.UnicodeEncoding.UTF8.GetString(ms.ToArray()));
            }
        }

        private void FillConsoleInfoResponse(HttpListenerContext r)
        {
            var res = r.Response;
            var req = r.Request;

            if (req.HttpMethod == "PORT")
            {
                try
                {
                    var cnt = new DataContractJsonSerializer(typeof(VehicleInfoReceived));
                    var recvinfo = (VehicleInfoReceived)cnt.ReadObject(req.InputStream);
                    var vh = this.Vehicles.First(v => v.Name == recvinfo.Name);

                    if (recvinfo.Speed != null)
                    {
                        Console.WriteLine("{0} is changing speed from {1} to {2}", vh.Name, vh.Speed, recvinfo.Speed);

                        vh.Speed = float.Parse(recvinfo.Speed) / 100.0f;
                    }
                    if (recvinfo.RouteName != null)
                    {
                        Console.WriteLine("{0} is changing route from {1} to {2}", vh.Name, vh.Route.Name, recvinfo.RouteName);
                        var route = vh.AvailableRoutes.First(rt => rt.Name == recvinfo.Name);
                        if (route.Blocks.Contains(vh.CurrentBlock))
                        {
                            vh.ChangeRoute(route);
                        }
                    }
                    if (recvinfo.Halts != null)
                    {
                        Console.WriteLine("{0} is changing halts set to {1}", vh.Name, recvinfo.Halts.Aggregate("", (ag, s) => ag += s + ", "));
                        var halts = recvinfo.Halts.Select(h => new Halt(vh.Sheet.GetBlock(h)));
                        vh.Halt.Clear();
                        foreach (var h in halts)
                            vh.Halt.Add(h);
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if (req.HttpMethod == "GET")
            {
            }

        }

        private HttpListener http_listener = null;
        public IObservable<Unit> GetHttpObservable()
        {
            if (this.http_listener == null)
            {
                var listener = new HttpListener();
                var prefix = "http://+:8012/";

                listener.Prefixes.Add(prefix);

                this.http_listener = listener;
                this.http_listener.Start();
            }
            return Observable.FromAsyncPattern<HttpListenerContext>(this.http_listener.BeginGetContext, this.http_listener.EndGetContext)()
                .Do(r =>
                {
                    var res = r.Response;
                    var req = r.Request;

                    switch (r.Request.Url.PathAndQuery)
                    {
                        case "/vehicles":
                            FillVehicleInfoResponse(r);
                            break;
                        case "/console":
                            FillConsoleInfoResponse(r);
                            break;
                    }

                })
                .Select(r => Unit.Default);
        }

    }

    //public class DeviceDiagnostics
    //{
    //    private PacketServer serv;
    //    private PacketDispatcher dispat;
    //    private DeviceID devid;

    //    public Stream Writer { get; private set; }
    //    public Stream Reader { get; private set; }

    //    public DeviceDiagnostics(DeviceID id, IPAddress baseip)
    //    {
    //        this.devid = new DeviceID(baseip.GetAddressBytes()[3], 0, 0);
    //        var io = new SensorLibrary.Packet.IO.TusEthernetIO(baseip, new IPAddress(new byte[] { 255, 255, 255, 0 }))
    //        {
    //            SourceID = devid,
    //            Port = 8000,
    //        };

    //        this.dispat = new PacketDispatcher();
    //        this.serv.AddAction(this.dispat);

    //    }

    //    public void Start(Stream reader, Stream writer)
    //    {
    //        this.serv.LoopStart();

    //        if (!reader.CanRead)
    //            throw new InvalidOperationException("requires readable stream");

    //        if (!writer.CanWrite)
    //            throw new InvalidOperationException("requires writable stream");

    //        this.Reader = reader;
    //        this.Writer = writer;

    //    }

    //    public TDev CreateDevice<TDev>(DeviceID devid)
    //        where TDev : Device<IDeviceState<IPacketDeviceData>>, new()
    //    {
    //        var dev = new TDev() { DeviceID = devid };
    //        dev.ReceivingServer = this.serv;
    //        dev.Observe(this.dispat);

    //        return dev;
    //    }

    //}
}