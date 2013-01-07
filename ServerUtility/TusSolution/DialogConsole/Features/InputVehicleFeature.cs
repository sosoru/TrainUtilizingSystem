using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

using Tus.Route;

using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Threading;
using System.IO;
using DialogConsole.Features.Base;

namespace DialogConsole.Features
{
    [FeatureMetadata("5", "monitoring vehicles")]
    [Export(typeof(IFeature))]
    class InputVehicleFeature
        : BaseFeature, IFeature
    {        private IDisposable VehicleProcessing_;
        private Route InputRoute(out bool ignoreblockage)
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

            return new Route(rt.Select(s => this.Param.Sheet.GetBlock(s)).ToList());
        }

        private Vehicle CreateVehicle(string vhname, Block b)
        {
            if (this.VehicleProcessing_ != null)
                this.VehicleProcessing_.Dispose();

            foreach (var vh in this.Param.Vehicles.ToArray())
            {
                vh.Route.InitLockingPosition();
            }

            bool ign = false;
            Route rt = InputRoute(out ign);

            rt.IsRepeatable = true;
            var v = new Vehicle(this.Param.Sheet, rt);
            v.CurrentBlock = b;
            v.Speed = 0.0f;
            v.Name = vhname;
            v.IgnoreBlockage = (ign);

            this.Param.Vehicles.Add(v);
            return v;
        }

        private void VehicleProcess()
        {
            foreach (var v in this.Param.Vehicles)
            {
                v.Refresh();
            }
        }

        public void Execute()
        {
            Console.WriteLine("Vehicle Name ?");
            var vhname = Console.ReadLine();

            Console.WriteLine("Which block your vehicle halt on?");
            var bk = this.Param.Sheet.GetBlock(Console.ReadLine());

            //this.Vehicles.Clear();
            var v = CreateVehicle(vhname, bk);

            this.VehicleProcessing_ = Observable.Defer(() => Observable.Start(VehicleProcess, this.Param.SchedulerSendingProcessing))
                .Do(u => this.Param.Sheet.InquiryAllMotors())
                .Delay(TimeSpan.FromMilliseconds(1000))

                .Repeat()
                .SubscribeOn(Scheduler.NewThread)
                .Subscribe();

        }
    }
}
