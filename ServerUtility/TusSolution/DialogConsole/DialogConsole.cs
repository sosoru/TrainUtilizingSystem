using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

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

        private IScheduler SchedulerPacketProcessing;
        private IScheduler SchedulerSendingProcessing;

        private IDisposable Sending_;
        private IDisposable Receiving_;

        public void InitSheet(IDeviceIO io)
        {
            this.Server = CreateServer();
            this.Sheet = CreateSheet("test_looping.yaml", this.Server);

            this.Server.Controller = io;
        }

        public void Loop()
        {
            this.SchedulerSendingProcessing = Scheduler.TaskPool;
            this.SchedulerPacketProcessing = Scheduler.TaskPool;

            //this.Sending_ = Observable.Timer(DateTimeOffset.MinValue, TimeSpan.FromMilliseconds(20), this.SchedulerSendingProcessing)
            //    .Zip(this.Server.SendingObservable, (l, u) => new { l, u })
            //    .SubscribeOn(Scheduler.NewThread)
            //    .Subscribe();
            this.Server.SendingObservable.Delay(TimeSpan.FromMilliseconds(20))
                .Repeat()
                .Do(g => Console.WriteLine(string.Format("({0}.{1}) : sending {2}",
                                    DateTime.Now.ToLongTimeString(),
                                    DateTime.Now.Millisecond,
                                    g.ToString()
                                    )))
                .SubscribeOn(Scheduler.NewThread)
                .Subscribe();

            this.Receiving_ = this.Server.ReceivingObservable.ObserveOn(this.SchedulerPacketProcessing)
                .Repeat()
                .Do(state => Console.WriteLine(string.Format("({0}.{1}) : recving {2}",
                                        DateTime.Now.ToLongTimeString(),
                                        DateTime.Now.Millisecond,
                                        state.ToString()
                                        )))
                .SubscribeOn(Scheduler.NewThread)
                .Subscribe();

            while (true)
            {
                Console.WriteLine("1 : show statuses");
                Console.WriteLine("3 : detect test");
                Console.WriteLine("4 : input command");
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
                        //case "5":
                        //    this.Sheet.Effect(cmdinfo);
                        //    break;
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
            System.Threading.Thread.Sleep(3000);

            sht.InquiryAllMotors();
            System.Threading.Thread.Sleep(2000);

            Console.WriteLine(sht.InnerBlocks
               .Where(b => b.HasMotor && b.IsBlocked)
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