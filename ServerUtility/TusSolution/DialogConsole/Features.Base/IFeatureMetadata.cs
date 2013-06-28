using System;
namespace DialogConsole.Features.Base
{
    public interface IFeatureMetadata
    {
        string FeatureExpression { get; }
        string FeatureName { get; }
        bool IsShown { get; }   

    }
}
