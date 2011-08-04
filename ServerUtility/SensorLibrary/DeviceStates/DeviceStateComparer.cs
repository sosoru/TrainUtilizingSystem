using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensorLibrary
{
    public class DeviceStateComparer
        : IEqualityComparer<IDeviceState<IPacketDeviceData>>
    {

        public bool Equals(IDeviceState<IPacketDeviceData> x, IDeviceState<IPacketDeviceData> y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;
            else
            {
                var ara = x.BasePacket.Data;
                var arb = y.BasePacket.Data;
                return ara.SequenceEqual(arb);                
            }
        }

        public int GetHashCode(IDeviceState<IPacketDeviceData> obj)
        {
            return obj.BasePacket.Data.GetHashCode();
        }
    }
}
