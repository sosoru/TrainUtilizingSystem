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
using DialogConsole.Properties;
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

        public void Execute()
        {
        }

        public void Init()
        {
            this.StartHttpObservable();
        }

        public void ProcessResponseOrReqest(HttpListenerRequest req, HttpListenerResponse res)
        {
            try
            {
                var container = this.Container;
                var exports = this.Param.Pages;

                var query = req.QueryString;
                var page = exports.FirstOrDefault(p => req.Url.LocalPath.Contains(p.Metadata.Query));

                if (page == null)
                    return;
                
            page.Value.SetParameter(query);
                if (req.HttpMethod == "GET")
                {
                    var content = page.Value.GetJsonContent();
                    res.Headers.Add("Content-type: application/json");
                    res.Headers.Add("Access-Control-Allow-Headers: x-requested-with, accept");
                    res.Headers.Add("Access-Control-Allow-Origin: *");
                    using (var sw = new StreamWriter(res.OutputStream))
                    {
                        sw.Write(content);
                    }
                }
                else if (req.HttpMethod == "POST")
                {
                    using (var sr = new StreamReader(req.InputStream))
                        page.Value.CacheReceivedJsonContent(sr.ReadToEnd());

                         res.Headers.Add("Content-type: application/json");
                        res.Headers.Add("Access-Control-Allow-Headers: x-requested-with, accept");
                        res.Headers.Add("Access-Control-Allow-Origin: *");
                   using (var sw = new StreamWriter(res.OutputStream))
                   {
                       //page.Value.RefreshSendingJsonContent();
                       // sw.WriteLine(page.Value.GetJsonContent());
                       sw.WriteLine(@"[{""status""=""200""}]");
                   }
                }
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

        private HttpListener _httpListener;
        private void StartHttpObservable()
        {
            if (this._httpListener == null)
            {
                var listener = new HttpListener();
                string prefix = Settings.Default.http;

                listener.Prefixes.Add(prefix);
                this._httpListener = listener;
                this._httpListener.Start();
            }
            var obsvfunc = Observable.FromAsyncPattern<HttpListenerContext>(this._httpListener.BeginGetContext,
                                                                            this._httpListener.EndGetContext);

            this.Param.ServingInfomation = Observable.Defer(obsvfunc)
                                                .Repeat()
                                                .SubscribeOn(Scheduler.NewThread)
                                                .Subscribe(r =>
                                                               {
                                                                   var req = r.Request;
                                                                   var res = r.Response;

                                                                   this.ProcessResponseOrReqest(req, res);
                                                               });
        }
    }

    [DataContract]
    public struct VehicleInfoReceived
    {
        [DataMember(IsRequired = true)]
        public string Name;

        [DataMember(IsRequired=false)]
        public string ShownName;

        [DataMember(IsRequired = false)]
        public string Speed;

        [DataMember(IsRequired = false)]
        public string Accelation;

        [DataMember(IsRequired = false)]
        public string Timeout;

        [DataMember(IsRequired = false)]
        public string RouteName;

        [DataMember(IsRequired = false)]
        public string CurrentBlockName;

        [DataMember(IsRequired = false)]
        public ICollection<string> Halts;

        [DataMember(IsRequired = false)]
        public string IsHalt;

        [DataMember(IsRequired=false)]
        public string StopThreshold;
    }

}