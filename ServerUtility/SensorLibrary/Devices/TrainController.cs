using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensorLibrary
{
    public class TrainController
        : Device<TrainControllerState>
    {
        public TrainController()
            : base()
        {
            this.ModuleType = ModuleTypeEnum.TrainController;
        }

        public override void OnNext(IDeviceState<IPacketDeviceData> value)
        {
            var state = value as TrainControllerState;

            if (state != null)
            {
                if (state.LowerFreq == 0)
                    state = this.CurrentState;

                base.OnNext(state);
            }
            else
                base.OnNext(value);
        }
    }
}
