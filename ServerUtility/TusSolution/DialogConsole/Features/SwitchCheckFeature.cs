using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using DialogConsole.Features.Base;
using Tus.Communication;
using Tus.Communication.Device.AvrComposed;

namespace DialogConsole.Features
{
    [FeatureMetadata("7", "switch check")]
    [Export(typeof(IFeature))]
    class SwitchCheckFeature
        : BaseFeature, IFeature
    {
        private PointStateEnum _beforePos;
        public void Execute()
        {
            PointStateEnum pos = _beforePos == PointStateEnum.Straight ? PointStateEnum.Curve : PointStateEnum.Straight;

            Console.WriteLine(pos);
            if (pos != PointStateEnum.Any)
            {
                ChangeSwitch(pos);
            }
            _beforePos = pos;
        }

        private void ChangeSwitch(PointStateEnum dir)
        {
            var devs = this.Param.Sheet.AllDevices.Where(d => d is Switch).Cast<Switch>();
            foreach (var d in devs)
            {
                d.CurrentState.Position = dir;
                d.CurrentState.DeadTime = 100;
                d.CurrentState.ChangingTime = 200;
                d.SendState();
                System.Threading.Thread.Sleep(100);
            }
        }

        public void Init()
        {
        }
    }
}
