using System;
namespace DengoController
{
    public interface IDengoController
    {
        double AccelLevel { get; }
        double BrakeLevel { get; }
        bool Position { get; }
    }
}
