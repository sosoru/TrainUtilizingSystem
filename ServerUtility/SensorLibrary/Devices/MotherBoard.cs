using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.ComponentModel;
using System.Reactive.Linq;

namespace SensorLibrary
{
    public class MotherBoard
        : Device<MotherBoardState>
    {
        public MotherBoard(DeviceID id, IObservable<IDeviceState<IPacketDeviceData>> obsv)
            : base(id, ModuleTypeEnum.MotherBoard, obsv) { }

        public MotherBoard(DeviceID id)
            : this(id, null)
        {
        }
    }
}