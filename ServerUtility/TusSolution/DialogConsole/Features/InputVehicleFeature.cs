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
using Tus.TransControl.Base;

namespace DialogConsole.Features
{
    [FeatureMetadata("5", "monitoring vehicles")]
    [Export(typeof(IFeature))]
    internal class InputVehicleFeature
        : BaseFeature, IFeature
    {
        [ImportMany]
        public IEnumerable<Lazy<IConsolePage, ITusPageMetadata>> Pages { get; set; }

        public void Execute()
        {
            Console.WriteLine("Vehicle Name ?");
            string vhname = Console.ReadLine();

            if (this.Param.UsingLayout.Vehicles.Any(vh => vh.Name == vhname))
            {
                Console.WriteLine("Duplicated vehicle name");
                return;
            }

            Console.WriteLine("Which block your vehicle halt on?");
            Block bk = Param.UsingLayout.Sheet.GetBlock(Console.ReadLine());

            //if block is not found
            if (bk == null)
            {
                Console.WriteLine("Block is not found");
                return;
            }

            //this.Vehicles.Clear();
            Vehicle v = null;
            if (TryCreateVehicle(vhname, bk, out v))
            {
                //if (this.Param.SyncDevicePipeline == null)
                //{
                //    Param.SyncDevicePipeline = Observable.Defer(() =>
                //            Observable.Start(this.Param.UsingLayout.Sheet.InquiryDevices))
                //        .Delay(TimeSpan.FromMilliseconds(300))
                //        .Repeat()
                //        .SubscribeOn(Scheduler.NewThread);

                //    Param.SyncDeviceProcessing = this.Param.SyncDevicePipeline.Subscribe();

                //}


            }
        }

        public void Init()
        {
            Param.VehiclePipeline = Observable.Defer(
                () => Observable.Start(VehicleProcess));
            //.Do(u => Param.Sheet.InquiryStatusOfAllMotors());
            //.Do(u => this.Param.Sheet.InquiryDevices());

            Param.VehicleProcessing = Param.VehiclePipeline
                //.Do(u => this.Param.UsingLayout.Sheet.InquiryStatusOfAllMotors())
                .Delay(TimeSpan.FromMilliseconds(500)).Repeat()
                                           .SubscribeOn(Scheduler.NewThread)
                                           .Subscribe();

        }

        private RouteOrder InputRouteManually(out bool ignoreblockage)
        {
            ignoreblockage = true;
            Console.WriteLine("write route manually");

            string[] rt = Console.ReadLine().Split(',');
            return new RouteOrder(rt.Select(s => Param.UsingLayout.Sheet.GetBlock(s.Trim())).ToArray());
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
        private bool TryCreateVehicle(string vhname, Block b, out Vehicle vh)
        {

            //foreach (Vehicle vh in Param.UsingLayout.Vehicles.ToArray())
            //{
            //    vh.AssociatedRoute.InitLockingPosition();
            //}

            bool ign = false;
            bool rev = false;
            bool release = false;
            if (this.routeorders == null)
                this.routeorders = this.Param.UsingLayout.AvailableRoutesOrderFactory.Create().ToList();

            var routes = this.routeorders;
            RouteOrder rt = InputRouteOrder(out ign, out release, routes);

            rt.IsRepeatable = true;
            var v = new Vehicle(Param.UsingLayout.Sheet, rt)
            {
                Name = vhname,
                IgnoreBlockage = (ign),
                ReleaseBlockage = release,
                AvailableRoutes = routes
            };

            lock (lock_vehicleprocess)
            {
                //if (Param.VehicleProcessing != null)
                //    Param.VehicleProcessing.Dispose();

                // VehicleをRouteに抑えられるかチェックすること
                if (v.CanLockRoute(b))
                {
                    v.Run(0.0f, b);
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

        private object lock_vehicleprocess = new object();
        private void VehicleProcess()
        {
            lock (lock_vehicleprocess)
            {
                try
                {
                    DevicePacket packet;
                    while (this.Param.UsingLayout.Sheet.Server.receving_queue.TryDequeue(out packet))
                    {
                        var states = packet.ExtractPackedPacket();
                        foreach (var state in states)
                            this.Param.UsingLayout.Sheet.Server.thisobsv_.Notify(state);
                    }

                    //Webから更新を反映
                    foreach (var page in this.Pages)
                    {
                        page.Value.ApplyReceivedJsonRequest();
                    }

                    foreach (Vehicle v in Param.UsingLayout.Vehicles.ToArray())
                    {
                        v.Refresh();
                    }

                    this.Param.UsingLayout.Sheet.SetUnlockedBlocksToDefault();
                    //inquiry devices
                    this.Param.UsingLayout.Sheet.InquiryDevices();

                    //send packets to device
                    //this.Param.SendingPacketPipeline.Subscribe();

                    foreach (var page in this.Pages)
                    {
                        page.Value.RefreshSendingJsonContent();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }
        }
    }
}