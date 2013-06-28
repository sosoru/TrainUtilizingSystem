using DialogConsole.Features.Base;

namespace DialogConsole.WebPages
{
    public interface IConsolePage
    {
        string Query { get; set; }
        IFeatureParameters Param { get; set; }
        void SetResponseParameter(string query);
        string GetJsonContent();
    }
}