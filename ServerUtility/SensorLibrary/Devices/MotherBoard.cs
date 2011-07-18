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
            : base(id, ModuleTypeEnum.MotherBoard, obsv)
        {
            this.StateEqualityComparer = new GenericComparer<MotherBoardState>(mbstateCompare);

        }

        public MotherBoard(DeviceID id)
            : this(id, null)
        {
        }

        private bool mbstateCompare(MotherBoardState x, MotherBoardState y)
        {
            if (x.ParentID != y.ParentID)
                return false;

            for (int i = 0; i < x.ModuleTypeLength; i++)
            {
                if (x[i] != y[i])
                    return false;
            }
            return true;
        }
    }
}