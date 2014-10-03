using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using DialogConsole.Features.Base;

namespace DialogConsole.WebPages
{
    public abstract class ConsolePageBase<TSend, TRecv> : IConsolePage
    {
        [Import]
        public IFeatureParameters Param { get; set; }
        public NameValueCollection Query { get; set; }

        public ConsolePageBase()
        {
            Query = new NameValueCollection();
        }

        public void SetParameter(NameValueCollection query)
        {
            Query = query;
        }

        protected virtual IEnumerable<Type> KnownTypesWhenSerialization
        {
            get { return new Type[] { }; }
        }
        protected DataContractJsonSerializer JsonSendingTypeSerializer
        {
            get { return new DataContractJsonSerializer(typeof(TSend), KnownTypesWhenSerialization); }
        }
        protected DataContractJsonSerializer JsonReceivedTypeSerializer
        {
            get { return new DataContractJsonSerializer(typeof(TRecv), this.KnownTypesWhenSerialization); }
        }

        private string _json_content;
        private object _lock_json_content = new object();
        public void RefreshSendingJsonContent()
        {
            var content = this.CreateSendingJsonContent();
            lock (_lock_json_content)
            {
                this._json_content = content;
            }
        }

        public abstract TSend CreateSendingContent();

        public string CreateSendingJsonContent()
        {
            var ser = this.JsonSendingTypeSerializer;
            using (var ms = new MemoryStream())
            using (var sr = new StreamReader(ms, Encoding.UTF8))
            {
                var content = this.CreateSendingContent();
                if (content == null)
                    return "";

                ser.WriteObject(ms, this.CreateSendingContent());
                ms.Seek(0, SeekOrigin.Begin);
                return sr.ReadToEnd();
            }
        }

        public string GetJsonContent()
        {
            lock (this._lock_json_content)
                return this._json_content;
        }

        public abstract void ApplyReceivedJsonRequest();

        protected ConcurrentQueue<TRecv> ReceivedContents = new ConcurrentQueue<TRecv>();
        public void CacheReceivedJsonContent(string content)
        {
            var ser = this.JsonReceivedTypeSerializer;
            using (var st = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                var deserialized = ser.ReadObject(st);
                ReceivedContents.Enqueue((TRecv)deserialized);
            }
        }
    }

}