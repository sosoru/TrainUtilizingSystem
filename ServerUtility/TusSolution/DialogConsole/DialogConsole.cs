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
using Tus.TransControl.Base;


namespace DialogConsole
{
    using Tus.Factory;
    using DialogConsole.Features.Base;

    internal class MainClass
    {
        private static void Main(string[] args)
        {
            var container = new DialogConsoleContainer();
            var dialog = container.GetExport<DialogConsole.DialogConsoleClass>();

            dialog.Value.Loop();
        }
    }

    [Export(typeof(CompositionContainer))]
    class DialogConsoleContainer
        : CompositionContainer
    {
        private static AggregateCatalog catalogs
        {
            get
            {
                var catalog = new AggregateCatalog();
                catalog.Catalogs.Add(new AssemblyCatalog(".\\Tus.Factory.dll"));
                catalog.Catalogs.Add((new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly())));

                return catalog;
            }
        }

        public DialogConsoleContainer()
            : base(catalogs)
        {
        }

    }

    [Export(typeof(IFeatureParameters))]
    class DialogConsoleParameters
        : IFeatureParameters
    {
        public RouteListFactory AvailableRoutesFactory { get; set; }
        public BlockSheet Sheet { get; set; }
        public IList<Vehicle> Vehicles { get; set; }
        public IDisposable ServingInfomation { get; set; }
        public IObservable<Unit> VehiclePipeline { get; set; }
        public IDisposable VehicleProcessing { get; set; }
        public IObservable<DevicePacket> SendingPacketPipeline { get; set; }
        public IObservable<DevicePacket> ReceivingPacketPipeline { get; set; }

        [ImportingConstructor]
        public DialogConsoleParameters(SheetFactory fact, RouteListFactory rtfact)
        {
            AvailableRoutesFactory = rtfact;
            this.Sheet = fact.Create();
            rtfact.Sheet = this.Sheet;

            this.Vehicles = new List<Vehicle>();
        }

        public IScheduler SchedulerPacketProcessing { get; set; }
    }

    [Export]
    public class DialogConsoleClass
        : IPartImportsSatisfiedNotification
    {
        [Import]
        public IFeatureParameters Param { get; set; }

        [ImportMany]
        public IEnumerable<Lazy<IFeature, IFeatureMetadata>> Features { get; set; }

        private SynchronizationContext _syncPacketProcess;

        private IDisposable _receiving;

        private StreamWriter _logWriterRecv;
        private object lock_writer = new object();
        private StreamWriter _logWriter;

        private StreamWriter LogWriterSend
        {
            get
            {
                lock (this.lock_writer)
                {
                    return this._logWriter;
                }
            }
            set
            {
                lock (this.lock_writer)
                {
                    this._logWriter = value;
                }
            }
        }

        public void Loop()
        {
            this.LogWriterSend = new StreamWriter("packet_log_.txt", true);
            this._logWriterRecv = new StreamWriter("packet_log_recv_.txt", true);

            this._syncPacketProcess = new SynchronizationContext();
            this.Param.SchedulerPacketProcessing = new SynchronizationContextScheduler(this._syncPacketProcess);
            this.Param.Sheet.AssociatedScheduler = this.Param.SchedulerPacketProcessing;

            this.Param.SendingPacketPipeline = this.Param.Sheet.Server.SendingObservable
                                                   .ObserveOn(this.Param.SchedulerPacketProcessing);

            //this.Param.Sheet.Server.SendingObservable
            this.Param.SendingPacketPipeline
                .Delay(TimeSpan.FromMilliseconds(15))
                .Repeat()
                .SelectMany(g => g.ExtractPackedPacket())
                .Do(g => this.LogWriterSend.WriteLine(string.Format("({0}.{1}) : sending {2}",
                                                                    DateTime.Now.ToLongDateString() +
                                                                    DateTime.Now.ToLongTimeString(),
                                                                    DateTime.Now.Millisecond,
                                                                    g.ToString()
                                                          )))
                .ObserveOn(this.Param.SchedulerPacketProcessing)
                .SubscribeOn(Scheduler.Default)
                .Subscribe();

            var timer = Observable.Interval(TimeSpan.FromMilliseconds(20), Scheduler.Default);

            this.Param.ReceivingPacketPipeline = Observable.Defer(() => this.Param.Sheet.Server.ReceivingObservable)
                                                           .ObserveOn(this.Param.SchedulerPacketProcessing)
                                                           .Repeat()
            .SubscribeOn(Scheduler.Default)
                                                           .Zip(timer, (v, _) => v);
            this.Param.ReceivingPacketPipeline.SelectMany(v => v.ExtractPackedPacket())
            .Do(g => this._logWriterRecv.WriteLine(string.Format("({0}.{1}) : recving {2}",
                                                                 DateTime.Now
                                                                         .ToLongDateString() +
                                                                 DateTime.Now
                                                                         .ToLongTimeString(),
                                                                 DateTime.Now.Millisecond,
                                                                 g.ToString()
                                                       )))
            .Subscribe();

            this.Param.Sheet.Effect(new CommandFactory()
                                        {
                                            CreateCommand =
                                                b => new CommandInfo() { MotorMode = MotorMemoryStateEnum.NoEffect, },
                                        },
                                    this.Param.Sheet.InnerBlocks);

            foreach (var f in this.Features)
                f.Value.Init();

            while (true)
            {
                foreach (var f in this.Features.Where(f => f.Metadata.IsShown))
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

        public void OnImportsSatisfied()
        {
            this.Features = this.Features.OrderBy(f => f.Metadata.FeatureExpression).ToList();
        }
    }
}