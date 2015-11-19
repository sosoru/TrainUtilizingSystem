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

        private DataContractJsonSerializer _jsonSendingSerializer = null;
        public DataContractJsonSerializer JsonSendingTypeSerializer
        {
            get
            {
                if (_jsonSendingSerializer == null)
                {
                    this._jsonSendingSerializer = new DataContractJsonSerializer(typeof(TSend),
                        KnownTypesWhenSerialization);
                }
                return this._jsonSendingSerializer;
            }
        }
        private DataContractJsonSerializer _jsonReceivedSerializer = null;
        public DataContractJsonSerializer JsonReceivedTypeSerializer
        {
            get
            {
                if (_jsonReceivedSerializer == null)
                    this._jsonReceivedSerializer = new DataContractJsonSerializer(typeof(TRecv),
                                                                                  this.KnownTypesWhenSerialization);
                return this._jsonReceivedSerializer;
            }
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
            return CreateSendingJsonContent(this.CreateSendingContent());
        }

        public string CreateSendingJsonContent(TSend content)
        {
            if (content == null)
                return "";

            var ser = this.JsonSendingTypeSerializer;
            using (var ms = new MemoryStream())
            using (var sr = new StreamReader(ms, Encoding.UTF8))
            {
                ser.WriteObject(ms, content);
                ms.Seek(0, SeekOrigin.Begin);
                return sr.ReadToEnd();
            }
        }

        public string CreateReceivingJsonContent(TRecv content)
        {
            if (content == null)
                return "";

            var ser = this.JsonReceivedTypeSerializer;
            using (var ms = new MemoryStream())
            using (var sr = new StreamReader(ms, Encoding.UTF8))
            {
                ser.WriteObject(ms, content);
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