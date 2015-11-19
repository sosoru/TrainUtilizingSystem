using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using DialogConsole.Features.Base;
using DialogConsole.WebPages;
using Tus.Communication;
using Tus.Diagnostics;
using Tus.TransControl.Base;

namespace DialogConsole.Features
{
    [FeatureMetadata("r", "resume last vehicles")]
    [Export(typeof(IFeature))]
    internal class ResumeVehicleFeature
        : BaseFeature, IFeature
    {
        [ImportMany]
        public IEnumerable<Lazy<IConsolePage, ITusPageMetadata>> Pages { get; set; }

        public void Execute()
        {
            var fname = "last_vehicle.txt";
            var vpage = (Pages.First(page => page.Value is VehiclePage)).Value as VehiclePage;
            VehicleInfoReceived[] vehicles;

            using (var ms = new MemoryStream(File.ReadAllBytes(fname)))
            {
                vehicles = vpage.JsonReceivedTypeSerializer.ReadObject(ms) as VehicleInfoReceived[];
            }

            foreach (var v in vehicles)
            {
                Vehicle newvehicle;
                TryCreateVehicle(v, Param.UsingLayout.Sheet.GetBlock(v.CurrentBlockName), v.RouteName, out newvehicle);
                Logger.WriteLineAsTransInfo("resume {0}({1}) on {2}({3})", v.Name, v.ShownName, v.CurrentBlockName, v.RouteName);
            }
        }

        public void Init()
        {
        }

        private RouteOrder InputRouteOrder(out bool ignoreblockage, out bool release, IEnumerable<RouteOrder> routes)
        {
            Console.WriteLine("select route [name] [sub] [ign] [rl]");
            string ans = Console.ReadLine().ToLower().Trim();

            //bool rev = ans.Contains("rev");
            bool sub = ans.Contains("sub");
            ignoreblockage = ans.Contains("ign");
            release = ans.Contains("rl");

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

        private IList<RouteOrder> routeorders = null;
        private bool TryCreateVehicle(VehicleInfoReceived obj, Block b, string rtordername, out Vehicle vh)
        {
            bool ign = false;
            bool rev = false;
            bool release = false;
            if (this.routeorders == null)
                this.routeorders = this.Param.UsingLayout.AvailableRoutesOrderFactory.Create().ToList();

            var routes = this.routeorders;
            RouteOrder rt = routes.First(r => r.Name == rtordername);

            rt.IsRepeatable = true;
            var v = new Vehicle(Param.UsingLayout.Sheet, rt)
            {
                Name = obj.Name,
                ShownName = obj.ShownName,
                IgnoreBlockage = ign,
                ReleaseBlockage = release,
                AvailableRoutes = routes,
                StopThreshold = float.Parse(obj.StopThreshold),
            };
            // VehicleをRouteに抑えられるかチェックすること
            if (v.CanLockRoute(b))
            {
                v.Run(0.0f, b);
                if (bool.Parse(obj.IsHalt))
                    v.HaltHere();

                Param.UsingLayout.Vehicles.Add(v);
                vh = v;
                return true;
            }
            else
            {
                Console.WriteLine("full block");
                vh = null;
                return false;
            }
        }

    }
}