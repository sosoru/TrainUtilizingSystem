using System;
namespace RouteLibrary.Base
{
    public interface IDeviceEffector
    {
        void EffectByRoute(CommandInfo cmd);
    }
}
