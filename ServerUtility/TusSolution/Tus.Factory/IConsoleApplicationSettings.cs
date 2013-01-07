using System;
using System.ComponentModel.Composition;
namespace Tus.Factory
{
    public interface IConsoleApplicationSettings
    {
        string IpMask { get; }
        int IpPort { get; }
        string IpSegment { get; }
        string ParentDeviceID { get; }
        string SheetPath { get; }
    }
}
