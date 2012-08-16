using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Reactive;
using System.Reactive.Linq;

using SensorLibrary;
using SensorLibrary.Packet;
using SensorLibrary.Packet.Control;
using SensorLibrary.Devices;
using SensorLibrary.Devices.TusAvrDevices;

using RouteLibrary;
using RouteLibrary.Base;
using RouteLibrary.Parser;

namespace RouteServerConsole
{
    static class Extenstions
    {
        static public IEnumerable<Route> ToRoute(this IEnumerable<IEnumerable<string>> src, BlockSheet sheet)
        {
            return src.Select(names => names.Select(n => sheet.InnerBlocks.First(b => b.Name == n)))
                .Select(bs => new Route(bs.ToList()));
        }
    }

    class Program
    {
        private static BlockSheet sheet { get; set; }

        static PacketServer CreateServer(IPAddress ipbase, IPAddress ipmask)
        {
            var serv = new PacketServer(new AvrDeviceFactoryProvider());
            var io = new SensorLibrary.Packet.IO.TusEthernetIO(ipbase, ipmask)
                         {
                             SourceID = new DeviceID(100, 0, 0),
                             Port = 8000,
                         };

            serv.Controller = io;

            serv.LoopStart();
            return serv;
        }

        static BlockSheet CreateSheet(string path, PacketServer serv)
        {
            var parser = new BlockYaml();
            var blocks = parser.Parse(path);

            return new BlockSheet(blocks, serv);
        }

        static IEnumerable<Route> Route_cw()
        {
            var routes = new[]{
                new[] {"AT1", "PT2", "BT1", "PT3", "CT1", "PT4", "DT1", "PT1", "AT1"},
            };
            return routes.ToRoute(sheet)
                .Repeat();
        }

        static IEnumerable<Route> Route_cw2()
        {
            var routes = new[]
                             {
                                 new[] {"AT2", "PT1", "DT1", "PT4", "CT2", "PT3", "BT1", "PT2", "AT2"},
                             };
            return routes.ToRoute(sheet)
                .Repeat();
        }

        static void ClearState()
        {
            sheet.Effectors.ForEach(e => e.ExecuteDefaultCommand());
        }

        static void ChangeAllSwithches(SensorLibrary.Packet.Data.PointStateEnum pt)
        {
            sheet.Effectors.Where(e => e is SwitchEffector)
                .Cast<SwitchEffector>()
                .Do(s =>
                        {
                            s.Device.CurrentState.Position = pt;
                            s.ExecuteCommand();
                        })
                .ToArray();
        }

        static IObservable<string> ShowStatuses()
        {
            return sheet.InnerBlocks.Where(b => b.HasSensor)
                    .Select(b => string.Format("Sensor : {0} is {1} {2} {3} {4} {5}",
                                               b.Name,
                                               b.IsDetectingTrain,
                                               b.Detectors.First().Devices.First().CurrentState.Data.VoltageOn,
                                               b.Detectors.First().Devices.First().CurrentState.Data.VoltageOff,
                                               b.Detectors.First().Devices.Last().CurrentState.Data.VoltageOn,
                                               b.Detectors.First().Devices.Last().CurrentState.Data.VoltageOff
                                     ))
                                     .ToObservable();
        }

        static IObservable<string> ShowSwitchStatuses()
        {
            return sheet.InnerBlocks.Where(b => b.HasSwitch)
                .SelectMany(b => b.Effectors)
                .Where(e => e is SwitchEffector)
                .Cast<SwitchEffector>()
                .Select(e => string.Format("Switch : {0} is {1}", e.ParentBlock.Name, e.Device.CurrentState.Position))
                .ToObservable();
        }

        static IObservable<string> ShowMotorStatuses()
        {
            return sheet.InnerBlocks.Where(b => b.HasMotor)
                .SelectMany(b => b.Effectors)
                .Where(e => e is MotorEffector)
                .Cast<MotorEffector>()
                .Select(
                    e =>
                    string.Format("Motor : {0} is {1}, {2}", e.ParentBlock.Name, e.Device.CurrentState.Duty,
                                  e.Device.CurrentState.Direction))
                .ToObservable();
        }

        static IObservable<string> ShowRouteState(IEnumerable<Route> routes)
        {
            return routes.Select(r =>
                                     {
                                         var names = r.LockedBlocks
                                             .ToArray()
                                             .Select(b => b.Name + ", ")
                                             .Aggregate((ag, s) => ag + s);
                                         return names;
                                     }
                )
                .ToObservable();
        }

        static IObservable<Route> positioning()
        {
            // positioning train to AT1(cw), AT2(ccw)

            var cw = Route_cw().First();
            var ccw = Route_cw2().First();

            return new[] { cw }
                .ToObservable()
                .Do(r =>
                        {
                            ClearState();
                            EffectRoute(cw, 0.6f, true);
                            ClearState();
                            EffectRoute(ccw, 0.6f, true);
                        });
        }

        //static IObservable<Route> FollowingRoute(IObservable<Route> rt)
        //{
        //    return
        //        ClearState()
        //            .SelectMany(s => EffectRoute(rt.First(), 0.5f).TakeWhile(r => r.IsRouteFinished));
        //}

        //static IObservable<Route> FollowingDualRoute(IEnumerable<Route> rs)
        //{
        //    return
        //        EffectRoutes(rs, 0.6f)
        //            .TakeWhile(a => a.IsRouteFinished);

        //}

        static void EffectRoutes(IEnumerable<Route> routes, float speed)
        {
            var rs = routes.ToArray();
            var mvr = rs
                .Aggregate(new List<Route>(),
                           (l, r) =>
                           {
                               if (!l.Any(bfr => bfr.LockedBlocks.Any(b1 => r.LockedBlocks.Contains(b1))))
                               {
                                   l.Add(r);
                               }

                               return l;
                           });
            //rs.Except(mvr).ForEach(r => EffectRouteOnce(r, 0, true));
            mvr.ForEach(r => EffectRouteOnce(r, speed, false));
        }

        static void SetToDefault(Route r)
        {
            while (r.Blocks.Count == r.LockedBlocks.Count())
            {
                r.LockNextUnit();
            }

            EffectRoute(r, 0.0f, true);
        }

        static void EffectRouteOnce(Route r, float speed, bool todefault)
        {
            if (r.IsSectionFinished)
            {
                r.LockNextUnit();
                r.ReleaseBeforeUnit();
            }

            var cmd = new CommandInfo()
                          {
                              Route = r,
                              Speed = speed,
                              AnyToDefault = todefault,
                          };
            sheet.Effect(cmd);

        }

        static void EffectRoute(Route r, float speed, bool todefault)
        {

            while (true)
            {
                if (r.IsRouteFinished)
                    return;

                EffectRouteOnce(r, speed, todefault);
            }
        }

        //static IObservable<Route> SelectCommand(string cmd)
        //{
        //    IObservable<Route> ob;

        //    switch (cmd)
        //    {
        //        case "1":
        //            ob = positioning();
        //            break;
        //        case "2":
        //            ob = FollowingDualRoute(new[] { Route_cw().First(), Route_cw2().First() });
        //            break;
        //        case "3":
        //            ob = FollowingDualRoute(new[] { Route_cw().First() });
        //            break;
        //        case "4":
        //            ob = FollowingDualRoute(new[] { Route_cw2().First() });
        //            break;
        //        default:
        //            ob = Observable.Empty<Route>();
        //            break;
        //    }

        //    return ob;

        //}

        static void Main(string[] args)
        {
            var serv = CreateServer(new IPAddress(new byte[] { 192, 168, 2, 24 }),
                                    new IPAddress(new byte[] { 255, 255, 255, 0 }));
            sheet = CreateSheet(@"C:\Users\Administrator\Desktop\新しいフォルダー (2)\815.yaml", serv);
            var message =
                "press 1:positioning trains, 2:following double routes, 3:following cw route, 4:following ccw route";

            var sc = System.Reactive.Concurrency.Scheduler.NewThread;

            var statuses =
                Observable
                .Start(() => DateTime.Now.ToString())
                .Concat(new[] { message }.ToObservable())
                //.Concat(Observable.Defer(() => ShowRouteState(new[] { })))
                .Concat(Observable.Defer(() => ShowStatuses()))
                .Concat(Observable.Defer(() => ShowSwitchStatuses()))
                .Concat(Observable.Defer(() => ShowMotorStatuses()))
                .Concat(new[] { "" }.ToObservable())
                .Do(Console.WriteLine)
                .Delay(TimeSpan.FromSeconds(1))
                .Repeat()
                .SubscribeOn(sc)
                .Subscribe(s => { }, ex => Console.WriteLine(ex.ToString()));

            IDisposable monitoring = null;
            while (true)
            {

                var cmd = Console.ReadLine();

                if (monitoring != null)
                    monitoring.Dispose();

                while (true)
                {
                    new[] {
                        new{route= Route_cw().First(), sp = 0.5f, ps = SensorLibrary.Packet.Data.PointStateEnum.Straight},
                        new{route=Route_cw2().First(), sp = 0.3f, ps = SensorLibrary.Packet.Data.PointStateEnum.Curve},
                        }
                        .ForEach(r =>
                                     {

                                         ChangeAllSwithches(r.ps);
                                         System.Threading.Thread.Sleep(1000);

                                         EffectRouteOnce(r.route, 0, true);
                                         System.Threading.Thread.Sleep(1000);
                                         EffectRoute(r.route, r.sp, true);
                                     });
                }


                ClearState();
                EffectRouteOnce(Route_cw().First(), 0f, true);
                System.Threading.Thread.Sleep(1000);
                EffectRoute(Route_cw2().First(), 0.6f, true);
                ClearState();

                System.Threading.Thread.Sleep(1000);

                //var rs = new[] { Route_cw().First(), Route_cw2().First() };
                //rs.ForEach(r =>
                //               {
                //                   //r.LockNextUnit();
                //                   EffectRoute(r, 0.5f, false);
                //                   ClearState();
                //               });


                //monitoring = SelectCommand(cmd)
                //    .SubscribeOn(sc)
                //    .Subscribe();
            }

        }
    }
}
