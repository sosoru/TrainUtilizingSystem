using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Net;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using DialogConsole.Features.Base;
using DialogConsole.WebPages;
using Tus.Communication.Device.AvrComposed;
using Tus.TransControl.Base;

namespace DialogConsole.Features
{
    [Export(typeof(IFeature))]
    [FeatureMetadata("w", "web server", IsShown = false)]
    internal class StateWebServerFeature
        : BaseFeature, IFeature
    {
        [Import]
        public CompositionContainer Container { get; set; }

        [ImportMany]
        public IEnumerable<Lazy<IConsolePage, ITusPageMetadata>> Pages { get; set; }

        public void Execute()
        {
        }

        public void Init()
        {
            this.StartHttpObservable();
        }

        public void ProcessResponse(HttpListenerRequest req, HttpListenerResponse res)
        {
            var container = this.Container;
            var exports = this.Pages;

            var query = req.Url.PathAndQuery;
            var page = exports.First(p => query.Contains(p.Metadata.Query));

            page.Value.SetResponseParameter(query);
            var content = page.Value.GetJsonContent();
            FillResponse(res, content);
        }

        private DateTime _updatebefore = DateTime.MinValue;
        private object _update_lock = new object();
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
                    var vh = this.Param.UsingLayout.Vehicles.First(v => v.Name == recvinfo.Name);

                    if (recvinfo.Speed != null)
                    {
                        var changeto = float.Parse(recvinfo.Speed) / 100.0f;
                        Console.WriteLine("{0} is changing speed from {1} to {2}", vh.Name, vh.Speed, changeto);

                        vh.Speed = changeto;
                    }
                    if (recvinfo.RouteName != null)
                    {
                        Console.WriteLine("{0} is changing route from {1} to {2}", vh.Name, vh.AssociatedRoute.RouteOrder.Name,
                                          recvinfo.RouteName);
                        var route = vh.AvailableRoutes.First(rt => rt.Name == recvinfo.RouteName);
                        if (route.Blocks.Contains(vh.CurrentBlock))
                        {
                            vh.ChangeRoute(route);
                        }
                    }
                    if (recvinfo.Halts != null)
                    {
                        Console.WriteLine("{0} is changing halts set to {1}", vh.Name,
                                          recvinfo.Halts.Aggregate("", (ag, s) => ag + (s + ", ")));
                        var halts = recvinfo.Halts.Select(h => new Halt(vh.Sheet.GetBlock(h)));
                        vh.Halt.Clear();
                        foreach (var h in halts)
                            vh.Halt.Add(h);
                    }

                    //todo:ここ汚いから速くなおせ
                    //lock (this._update_lock)
                    //{
                    //    if ((DateTime.Now - this._updatebefore).Milliseconds > 200)
                    //    {
                    this.Param.VehiclePipeline.Subscribe();
                    this.Param.SendingPacketPipeline.Subscribe();
                    //        this._updatebefore = DateTime.Now;
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine("request ignored");
                    //    }

                    //}
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    res.Close();
                }
            }
            else
            {
                try
                {
                    using (var ms = new MemoryStream())
                    {
                        var cnt = new DataContractJsonSerializer(typeof(IEnumerable<Vehicle>), new[] {typeof(ControlUnit), typeof(Switch), typeof(Motor), typeof(UsartSensor), typeof(MemoryState) });
                        var vehis = this.Param.UsingLayout.Vehicles.ToArray();

                        cnt.WriteObject(ms, vehis);
                        var s = Encoding.UTF8.GetString(ms.ToArray());
                        FillResponse(res, s);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private static void FillResponse(HttpListenerResponse res, string s)
        {
            res.Headers.Add("Content-type: application/json");
            res.Headers.Add("Access-Control-Allow-Headers: x-requested-with, accept");
            res.Headers.Add("Access-Control-Allow-Origin: *");
            using (var sw = new StreamWriter(res.OutputStream))
            {
                sw.WriteLine(s);
            }
        }

        private HttpListener _httpListener;

        private void StartHttpObservable()
        {
            if (this._httpListener == null)
            {
                var listener = new HttpListener();
                const string prefix = "http://+:8012/";

                listener.Prefixes.Add(prefix);
                this._httpListener = listener;
                this._httpListener.Start();
            }
            var obsvfunc = Observable.FromAsyncPattern<HttpListenerContext>(this._httpListener.BeginGetContext,
                                                                            this._httpListener.EndGetContext);

            this.Param.ServingInfomation = Observable.Defer(obsvfunc)
                                                .Repeat()
                                                .ObserveOn(this.Param.SchedulerPacketProcessing)
                                                .SubscribeOn(Scheduler.NewThread)
                                                .Subscribe(r =>
                                                               {
                                                                   var req = r.Request;
                                                                   var res = r.Response;

                                                                   switch (r.Request.Url.PathAndQuery)
                                                                   {
                                                                       case "/vehicles":
                                                                           FillVehicleInfoResponse(r);
                                                                           break;
                                                                       default:
                                                                           //this.ProcessResponse(req, res);
                                                                           break;
                                                                   }
                                                               });
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
}