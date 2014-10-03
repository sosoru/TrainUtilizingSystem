using System.Collections.Specialized;
using DialogConsole.Features.Base;

namespace DialogConsole.WebPages
{
    public interface IConsolePage
    {
        NameValueCollection Query { get; set; }
        IFeatureParameters Param { get; set; }
        void SetParameter(NameValueCollection query);
        string GetJsonContent();
        void RefreshSendingJsonContent();
        void CacheReceivedJsonContent(string query);
        void ApplyReceivedJsonRequest();
    }
}