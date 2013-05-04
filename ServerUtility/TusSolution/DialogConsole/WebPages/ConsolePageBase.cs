using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using DialogConsole.Features.Base;

namespace DialogConsole.WebPages
{
    public abstract class ConsolePageBase : IConsolePage
    {
        [Import]
        public IFeatureParameters Param { get; set; }

        public string Query { get; set; }

        public void SetResponseParameter(string query)
        {
            Query = query;
        }

        public abstract string GetJsonContent();

        protected string GetJsonContent<T>(T obj, DataContractJsonSerializer ser)
        {
            using (var ms = new MemoryStream())
            using (var sr = new StreamReader(ms, Encoding.UTF8))
            {
                ser.WriteObject(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                return sr.ReadToEnd();
            }

        }

        protected string GetJsonContent<T>(T obj)
        {
            // 実行時型はDataContractSerializerは処理できない
            var ser = new DataContractJsonSerializer(typeof(T));
            return GetJsonContent<T>(obj, ser);
        }

        public abstract void ApplyJsonRequest();
    }
}