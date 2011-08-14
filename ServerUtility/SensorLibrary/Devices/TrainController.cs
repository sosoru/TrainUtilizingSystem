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
            :base()
        {
            this.ModuleType = ModuleTypeEnum.TrainController;
        }


    }
}
