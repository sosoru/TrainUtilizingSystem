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
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Threading;

using Tus.Communication;
using Tus.Communication.Device;
using Tus.Communication.Device.AvrComposed;

using Tus;
using Tus.Route;
using Tus.Route.Parser;


namespace DialogConsole
{
    using Tus.Factory;
    using DialogConsole.Features.Base;
    class MainClass
    {
        static void Main(string[] args)
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(".\\Tus.Factory.dll"));
            catalog.Catalogs.Add((new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly())));
            var container = new CompositionContainer(catalog);
            var shtfactory = container.GetExport<SheetFactory>();

            var dialog = container.GetExport<DialogConsole.DialogCnosole>();

            dialog.Value.Loop();
        }
    }

    [Export(typeof(IFeatureParameters))]
    public class DialogConsoleParameters
        : IFeatureParameters
    {
        public BlockSheet Sheet { get; set; }
        public IList<Vehicle> Vehicles { get; set; }
        public IScheduler SchedulerSendingProcessing { get; set; }

        [ImportingConstructor]
        public DialogConsoleParameters(SheetFactory fact)
        {
            this.Sheet = fact.Create();
            this.Vehicles = new List<Vehicle>();
        }
    }

    [Export]
    class DialogCnosole
        : IPartImportsSatisfiedNotification
    {
        [Import]
        public IFeatureParameters Param { get; set; }

        [ImportMany]
        public IEnumerable<Lazy<IFeature, IFeatureMetadata>> Features { get; set; }

        private IScheduler SchedulerPacketProcessing;
        private SynchronizationContext SyncNetwork;
        private SynchronizationContext SyncPacketProcess;

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

        public void Loop()
        {
            this.LogWriterSend = new StreamWriter("packet_log.txt", true);
            this.LogWriterRecv = new StreamWriter("packet_log_recv.txt", true);


            this.SyncNetwork = new SynchronizationContext();
            this.SyncPacketProcess = new SynchronizationContext();

            this.Param.SchedulerSendingProcessing = new SynchronizationContextScheduler(this.SyncNetwork);
            this.SchedulerPacketProcessing = new SynchronizationContextScheduler(this.SyncPacketProcess);

            this.Param.Sheet.AssociatedScheduler = this.SchedulerPacketProcessing;

            this.Param.Sheet.Server.SendingObservable
                .Delay(TimeSpan.FromMilliseconds(15))
                .Repeat()
                .SelectMany(g => g.ExtractPackedPacket())
                .Do(g => this.LogWriterSend.WriteLine(string.Format("({0}.{1}) : sending {2}",
                                    DateTime.Now.ToLongTimeString(),
                                    DateTime.Now.Millisecond,
                                    g.ToString()
                                    )))
                .ObserveOn(this.Param.SchedulerSendingProcessing)
                .SubscribeOn(Scheduler.Default)
                .Subscribe();

            var timer = Observable.Interval(TimeSpan.FromMilliseconds(20), Scheduler.Default);
            this.Receiving_ = Observable.Defer(() => this.Param.Sheet.Server.ReceivingObservable)
                .ObserveOn(this.Param.SchedulerSendingProcessing)
                .Repeat()
                .Zip(timer, (v, _) => v)
                .SelectMany(v => v.ExtractPackedPacket())
                .Do(g => this.LogWriterRecv.WriteLine(string.Format("({0}.{1}) : recving {2}",
                                                                    DateTime.Now.ToLongTimeString(),
                                                                    DateTime.Now.Millisecond,
                                                                    g.ToString()
                                                                    )))
                                                            .SubscribeOn(Scheduler.Default)
                                                            .Subscribe();
            this.StartHttpObservable();

            this.Param.Sheet.Effect(new CommandFactory()
            {
                CreateCommand = b => new CommandInfo() { MotorMode = MotorMemoryStateEnum.NoEffect, }
            },
            this.Param.Sheet.InnerBlocks);

            while (true)
            {
                foreach (var f in this.Features)
                    Console.WriteLine("{0} - {1}", f.Metadata.FeatureExpression, f.Metadata.FeatureName);

                Console.WriteLine();
                var cmd = Console.ReadLine();

                try
                {
                    var feature = this.Features.FirstOrDefault(f => f.Metadata.FeatureExpression == cmd);

                    if (feature == default(IFeature))
                        Console.WriteLine("parse error");
                    else
                    {
                        feature.Value.Execute();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

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

        [DataContract]
        public class VehicleInfoReceived
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
                    var vh = this.Param.Vehicles.First(v => v.Name == recvinfo.Name);

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
                res.Headers.Add("Access-Control-Allow-Headers: x-requested-with, accept");
                res.Headers.Add("Access-Control-Allow-Origin: *");
                using (var sw = new StreamWriter(res.OutputStream))
                using (var ms = new MemoryStream())
                {
                    var cnt = new DataContractJsonSerializer(typeof(IEnumerable<Vehicle>));
                    var vehis = this.Param.Vehicles.ToArray();

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

        public void OnImportsSatisfied()
        {
            this.Features = this.Features.OrderBy(f => f.Metadata.FeatureExpression).ToList();
        }
    }

}