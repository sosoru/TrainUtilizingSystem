using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using DialogConsole.Features.Base;
using Tus.TransControl.Base;

namespace DialogConsole.Features
{
    [FeatureMetadata("5", "monitoring vehicles")]
    [Export(typeof(IFeature))]
    internal class InputVehicleFeature
        : BaseFeature, IFeature
    {
        public void Execute()
        {
            try
            {
                Console.WriteLine("Vehicle Name ?");
                string vhname = Console.ReadLine();

                Console.WriteLine("Which block your vehicle halt on?");
                Block bk = Param.UsingLayout.Sheet.GetBlock(Console.ReadLine());

                //if block is not found
                if (bk == null)
                {
                    Console.WriteLine("Block is not found");
                    return;
                }

                //this.Vehicles.Clear();
                Vehicle v = CreateVehicle(vhname, bk);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Param.VehiclePipeline = Observable.Defer(
                () => Observable.Start(VehicleProcess, Param.SchedulerPacketProcessing));
            //.Do(u => Param.Sheet.InquiryStatusOfAllMotors());
            //.Do(u => this.Param.Sheet.InquiryDevices());

            Param.VehicleProcessing = Param.VehiclePipeline
                .Do(u => this.Param.UsingLayout.Sheet.InquiryStatusOfAllMotors())
                .Delay(TimeSpan.FromMilliseconds(500)).Repeat()
                                           .SubscribeOn(Scheduler.NewThread)
                                           .Subscribe();

        }

        public void Init()
        {
        }

        private Route InputRouteManually(out bool ignoreblockage)
        {
            ignoreblockage = true;
            Console.WriteLine("write route manually");

            string[] rt = Console.ReadLine().Split(',');
            return new Route(rt.Select(s => Param.UsingLayout.Sheet.GetBlock(s.Trim())).ToList());
        }

        private Route InputRoute(out bool ignoreblockage, IEnumerable<Route> routes)
        {
            Console.WriteLine("select route [name] [sub] [ign]");
            string ans = Console.ReadLine().ToLower().Trim();

            //bool rev = ans.Contains("rev");
            bool sub = ans.Contains("sub");
            ignoreblockage = ans.Contains("ign");

            if (ans.Length < 1)
                throw new ArgumentException("insufficient parameters");

            var routename = ans.Split(' ')[0];
            var rt = routes.FirstOrDefault(r => r.Name.ToLower() == routename);
            if (null == rt)
                throw new KeyNotFoundException("no route whose name is equal to the specified name is found");
            else
            {
                rt.IsRepeatable = true;
                return rt;
            }
        }

        private Vehicle CreateVehicle(string vhname, Block b)
        {
            if (Param.VehicleProcessing != null)
                Param.VehicleProcessing.Dispose();

            foreach (Vehicle vh in Param.UsingLayout.Vehicles.ToArray())
            {
                vh.Route.InitLockingPosition();
            }

            bool ign = false;
            bool rev = false;
            var routes = this.Param.UsingLayout.AvailableRoutesFactory.Create();
            Route rt = InputRoute(out ign, routes);

            rt.IsRepeatable = true;
            var v = new Vehicle(Param.UsingLayout.Sheet, rt);
            v.AvailableRoutes = routes;
            v.CurrentBlock = b;
            v.Speed = 0.0f;
            v.Name = vhname;
            v.IgnoreBlockage = (ign);

            Param.UsingLayout.Vehicles.Add(v);
            return v;
        }

        private object lock_vehicleprocess = new object();
        private void VehicleProcess()
        {
            lock (lock_vehicleprocess)
            {
                foreach (Vehicle v in Param.UsingLayout.Vehicles.ToArray())
                {
                    v.Refresh();
                }

            }
        }
    }
}