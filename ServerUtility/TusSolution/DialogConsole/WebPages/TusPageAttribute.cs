using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DialogConsole.Features.Base;

namespace DialogConsole.WebPages
{
    public interface ITusPageMetadata
        : IFeatureMetadata
    {
        string Query { get; }
        string Name { get; }
    }

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class TusPageMetadataAttribute
        : Attribute
    {
        public string Query { get; set; }
        public string Name { get; set; }

        public TusPageMetadataAttribute(string name, string query)
        {
            this.Name = name;
            this.Query = query;
        }
    }
}
