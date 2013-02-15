using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using DialogConsole.Features.Base;
using Tus.Route;

namespace DialogConsole.Features
{
    [Export(typeof(IFeature))]
    [FeatureMetadata("w", "web server", IsShown = false)]
    internal class StateWebServerFeature
        : BaseFeature, IFeature
    {
        public void Execute()
        {
        }

        public void Init()
        {
            this.StartHttpObservable();
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
                        Console.WriteLine("{0} is changing route from {1} to {2}", vh.Name, vh.Route.Name,
                                          recvinfo.RouteName);
                        var route = vh.AvailableRoutes.First(rt => rt.Name == recvinfo.Name);
                        if (route.Blocks.Contains(vh.CurrentBlock))
                        {
                            vh.ChangeRoute(route);
                        }
                    }
                    if (recvinfo.Halts != null)
                    {
                        Console.WriteLine("{0} is changing halts set to {1}", vh.Name,
                                          recvinfo.Halts.Aggregate("", (ag, s) => ag += s + ", "));
                        var halts = recvinfo.Halts.Select(h => new Halt(vh.Sheet.GetBlock(h)));
                        vh.Halt.Clear();
                        foreach (var h in halts)
                            vh.Halt.Add(h);
                    }

                    this.Param.VehiclePipeline.Subscribe();
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

        private void StartHttpObservable()
        {
            if (this.http_listener == null)
            {
                var listener = new HttpListener();
                var prefix = "http://+:8012/";

                listener.Prefixes.Add(prefix);
                this.http_listener = listener;
                this.http_listener.Start();
            }
            var obsvfunc = Observable.FromAsyncPattern<HttpListenerContext>(this.http_listener.BeginGetContext,
                                                                            this.http_listener.EndGetContext);

            this.Param.ServingInfomation = Observable.Defer(obsvfunc)
                                                .Repeat()
                                                .ObserveOn(this.Param.SchedulerPacketProcessing)
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