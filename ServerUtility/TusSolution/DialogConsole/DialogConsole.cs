﻿using System;
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

        private StreamWriter LogWriterRecv;
        private object lock_writer = new object();
        private StreamWriter log_writer_;
        private StreamWriter LogWriterSend
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
            this.Sheet = CreateSheet(@"C:\Users\Administrator\Desktop\12_layout.yaml", this.Server);
            this.Sheet.AssociatedScheduler = this.SchedulerPacketProcessing;

            this.Server.Controller = io;
            this.Vehicles = new List<Vehicle>();

            this.LogWriterSend = new StreamWriter("packet_log.txt", true);
            this.LogWriterRecv = new StreamWriter("packet_log_recv.txt", true);
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
                .Do(g => this.LogWriterSend.WriteLine(string.Format("({0}.{1}) : sending {2}",
                                    DateTime.Now.ToLongTimeString(),
                                    DateTime.Now.Millisecond,
                                    g.ToString()
                                    )))
                .ObserveOn(this.SchedulerSendingProcessing)
                .SubscribeOn(Scheduler.NewThread)
                .Subscribe();

            var timer = Observable.Interval(TimeSpan.FromMilliseconds(20), Scheduler.NewThread);
            this.Receiving_ = Observable.Defer(() => this.Server.ReceivingObservable)
                .ObserveOn(this.SchedulerSendingProcessing)
                .Repeat()
                .Zip(timer, (v, _) => v)
                .SelectMany(v => v.ExtractPackedPacket())
                .Do(g => this.LogWriterRecv.WriteLine(string.Format("({0}.{1}) : recving {2}",
                                                                    DateTime.Now.ToLongTimeString(),
                                                                    DateTime.Now.Millisecond,
                                                                    g.ToString()
                                                                    )))
                                                            .SubscribeOn(Scheduler.NewThread)
                                                            .Subscribe();
            this.StartHttpObservable();

            this.Sheet.Effect(new CommandFactory()
            {
                CreateCommand = b => new CommandInfo() { MotorMode = MotorMemoryStateEnum.NoEffect, }
            },
this.Sheet.InnerBlocks);

            Thread.Sleep(1000);

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

            Console.WriteLine("Vehicle Name ?");
            var vhname = Console.ReadLine();

            Console.WriteLine("Which block your vehicle halt on?");
            var bk = this.Sheet.GetBlock(Console.ReadLine());

            //this.Vehicles.Clear();
            var v = CreateVehicle(vhname, bk);

            this.VehicleProcessing_ = Observable.Defer(() => Observable.Start(VehicleProcess, this.SchedulerSendingProcessing))
                .Do(u => this.Sheet.InquiryAllMotors())
                .Delay(TimeSpan.FromMilliseconds(2000))

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

        public Route InputRoute(out bool ignoreblockage)
        {
            Console.WriteLine("select route [A-D] [rev] [sub] [ign]");
            var ans = Console.ReadLine().ToLower();

            var rev = ans.Contains("rev");
            var sub = ans.Contains("sub");
            ignoreblockage = ans.Contains("ign");

            if (ans.Length < 1)
                throw new ArgumentException("insufficient parameters");

            IEnumerable<string> rt = null;
            var firstch = ans.First();
            //rev = !rev;
            switch (firstch)
            {
                case 'a':
                    rt = RouteGeneratorForTwelve.GetLoopA(rev, sub);
                    break;
                case 'b':
                    rt = RouteGeneratorForTwelve.GetLoopB(rev, sub);
                    break;
                case 'c':
                    rt = RouteGeneratorForTwelve.GetLoopC(rev, sub);
                    break;
                case 'd':
                    rt = RouteGeneratorForTwelve.GetLoopD(rev, sub);
                    break;
                default:
                    throw new InvalidDataException("invalid route selection");
            }

            return new Route(rt.Select(s => this.Sheet.GetBlock(s)).ToList());
        }

        public Vehicle CreateVehicle(string vhname, Block b)
        {
            if (this.VehicleProcessing_ != null)
                this.VehicleProcessing_.Dispose();

            foreach (var vh in this.Vehicles.ToArray())
            {
                vh.Route.InitLockingPosition();
            }

            bool ign = false;
            Route rt = InputRoute(out ign);

            rt.IsRepeatable = true;
            var v = new Vehicle(this.Sheet, rt);
            v.CurrentBlock = b;
            v.Speed = 1.0f;
            v.Name = vhname;
            v.IgnoreBlockage = (ign);

            this.Vehicles.Add(v);
            return v;
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
            var req = r.Request;

            if (req.HttpMethod == "POST")
            {
                try
                {
                    var cnt = new DataContractJsonSerializer(typeof(VehicleInfoReceived));
                    var recvinfo = (VehicleInfoReceived)cnt.ReadObject(req.InputStream);
                    var vh = this.Vehicles.First(v => v.Name == recvinfo.Name);

                    if (recvinfo.Speed != null)
                    {
                        var changeto = float.Parse(recvinfo.Speed) / 100.0f;
                        Console.WriteLine("{0} is changing speed from {1} to {2}", vh.Name, vh.Speed, changeto);

                        vh.Speed = changeto;
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
                finally
                {
                    res.Close();
                }
            }
            else
            {
                res.Headers.Add("Content-type: application/json");
                res.Headers.Add("Access-Control-Allow-Headers: origin, x-requested-with, accept");
                res.Headers.Add("Access-Control-Allow-Origin: http://192.168.2.9");
                using (var sw = new StreamWriter(res.OutputStream))
                using (var ms = new MemoryStream())
                {
                    var cnt = new DataContractJsonSerializer(typeof(IEnumerable<Vehicle>));
                    var vehis = this.Vehicles.ToArray();

                    cnt.WriteObject(ms, vehis);

                    sw.WriteLine(System.Text.UnicodeEncoding.UTF8.GetString(ms.ToArray()));
                }
            }
        }

        private HttpListener http_listener = null;
        public void StartHttpObservable()
        {
            if (this.http_listener == null)
            {
                var listener = new HttpListener();
                var prefix = "http://+:8012/";

                listener.Prefixes.Add(prefix);
                this.http_listener = listener;
                this.http_listener.Start();
            }
            var obsvfunc = Observable.FromAsyncPattern<HttpListenerContext>(this.http_listener.BeginGetContext, this.http_listener.EndGetContext);

            this.ServingInfomation_ = Observable.Defer(obsvfunc)
                .Repeat()
                .ObserveOn(this.SchedulerPacketProcessing)
                .SubscribeOn(Scheduler.NewThread)
                .Subscribe(r =>
                {
                    var res = r.Response;
                    var req = r.Request;

                    switch (r.Request.Url.PathAndQuery)
                    {
                        case "/vehicles":
                            FillVehicleInfoResponse(r);
                            break;
                    }
                });

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