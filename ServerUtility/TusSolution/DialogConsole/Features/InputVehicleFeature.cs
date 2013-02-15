using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using DialogConsole.Features.Base;
using Tus.Route;

namespace DialogConsole.Features
{
    [FeatureMetadata("5", "monitoring vehicles")]
    [Export(typeof (IFeature))]
    internal class InputVehicleFeature
        : BaseFeature, IFeature
    {
        public void Execute()
        {
            Console.WriteLine("Vehicle Name ?");
            string vhname = Console.ReadLine();

            Console.WriteLine("Which block your vehicle halt on?");
            Block bk = Param.Sheet.GetBlock(Console.ReadLine());

            //this.Vehicles.Clear();
            Vehicle v = CreateVehicle(vhname, bk);


            Param.VehiclePipeline = Observable.Defer(
                () => Observable.Start(VehicleProcess, Param.SchedulerPacketProcessing))
                                              .Do(u => Param.Sheet.InquiryAllMotors());

            Param.VehicleProcessing = Param.VehiclePipeline.Delay(TimeSpan.FromMilliseconds(1000)).Repeat()
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
            return new Route(rt.Select(s => Param.Sheet.GetBlock(s.Trim())).ToList());
        }

        private Route InputRoute(out bool ignoreblockage)
        {
            Console.WriteLine("select route [A-D] [rev] [sub] [ign]");
            string ans = Console.ReadLine().ToLower();

            bool rev = ans.Contains("rev");
            bool sub = ans.Contains("sub");
            ignoreblockage = ans.Contains("ign");

            if (ans.Length < 1)
                throw new ArgumentException("insufficient parameters");

            IEnumerable<string> rt = null;
            char firstch = ans.First();
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

            return new Route(rt.Select(s => Param.Sheet.GetBlock(s)).ToList());
        }

        private Vehicle CreateVehicle(string vhname, Block b)
        {
            if (Param.VehicleProcessing != null)
                Param.VehicleProcessing.Dispose();

            foreach (Vehicle vh in Param.Vehicles.ToArray())
            {
                vh.Route.InitLockingPosition();
            }

            bool ign = false;
            Route rt = InputRouteManually(out ign);

            rt.IsRepeatable = true;
            var v = new Vehicle(Param.Sheet, rt);
            v.CurrentBlock = b;
            v.Speed = 0.0f;
            v.Name = vhname;
            v.IgnoreBlockage = (ign);

            Param.Vehicles.Add(v);
            return v;
        }

        private void VehicleProcess()
        {
            foreach (Vehicle v in Param.Vehicles)
            {
                v.Refresh();
            }
        }
    }
}