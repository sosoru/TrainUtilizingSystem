using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Tus.Communication.Device.AvrComposed;

namespace DialogConsole.WebPages
{
    [Export(typeof(IConsolePage))]
    [TusPageMetadata("switch device control", "switch")]
    public class SwitchPage : ConsolePageBase<IEnumerable<Switch>, IEnumerable<Switch>>
    {
        private IEnumerable<Switch> Switches
        {
            get
            {
                var switches =
                    this.Param.UsingLayout.Sheet.AllDevices.Where(
                        d => d.ModuleType == Tus.Communication.ModuleTypeEnum.AvrSwitch)
                        .Cast<Switch>();
                return switches;
            }
        }

        public override IEnumerable<Switch> CreateSendingContent()
        {
            return Switches;
        }

        public override void ApplyReceivedJsonRequest()
        {
            IEnumerable<Switch> obj;
            if (!this.ReceivedContents.TryDequeue(out obj)) return;
            foreach (var sentsw in obj)
            {
                var sw = this.Switches.FirstOrDefault(dev => dev.DeviceID == sentsw.DeviceID);
                if (sw != null)
                {
                    sw.CurrentState.DeadTime = 100;
                    sw.CurrentState.ChangingTime = 200;

                    if (sw.PositionReversed)
                    {
                        if (sentsw.CurrentState.Position == Tus.Communication.PointStateEnum.Straight)
                            sentsw.CurrentState.Position = Tus.Communication.PointStateEnum.Curve;
                        else if (sentsw.CurrentState.Position == Tus.Communication.PointStateEnum.Curve)
                            sentsw.CurrentState.Position = Tus.Communication.PointStateEnum.Straight;
                    }
                    sw.CurrentState.Position = sentsw.CurrentState.Position;

                    Console.WriteLine("{0}(switch): Position is changed to {1}", sw.DeviceIDString, sw.CurrentState.PositionString);
                    sw.SendState();
                }
            }
        }
    }
}