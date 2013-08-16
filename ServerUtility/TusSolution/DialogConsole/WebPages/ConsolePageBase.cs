using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using DialogConsole.Features.Base;

namespace DialogConsole.WebPages
{
    public abstract class ConsolePageBase<T> : IConsolePage
    {
        [Import]
        public IFeatureParameters Param { get; set; }
        public string Query { get; set; }

        public void SetResponseParameter(string query)
        {
            Query = query;
        }

        protected virtual DataContractJsonSerializer JsonSerializer
        {
            get { return new DataContractJsonSerializer(typeof (T)); }
        }

        public abstract T GetContent();

        public string GetJsonContent()
        {
            var ser = this.JsonSerializer;
            var obj = GetContent();
            using (var ms = new MemoryStream())
            using (var sr = new StreamReader(ms, Encoding.UTF8))
            {
                ser.WriteObject(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                return sr.ReadToEnd();
            }
        }

        public abstract void ApplyJsonRequest(T obj);

        public void ApplyJsonContent(string content)
        {
            var ser = this.JsonSerializer;
            using (var st = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                var deserialized = ser.ReadObject(st);
                this.ApplyJsonRequest((T) deserialized);
            }   
        }
    }
}