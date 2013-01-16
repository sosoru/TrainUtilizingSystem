using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace DialogConsole.Features.Base
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class FeatureMetadata
        : Attribute, IFeatureMetadata
    {
        public string FeatureExpression { get; set; }
        public string FeatureName { get; set; }
        public bool IsShown { get; set; }
        
        public FeatureMetadata(string expr, string name, bool isShown = true)
        {
            this.FeatureExpression = expr;
            this.FeatureName = name;
            this.IsShown = isShown;
        }
    }
}
