using System;
namespace DialogConsole.Factory
{
    interface IConsoleApplicationSettings
    {
        string IpMask { get; set; }
        int IpPort { get; set; }
        string IpSegment { get; set; }
        string ParentDeviceID { get; set; }
        string SheetPath { get; set; }
    }
}
