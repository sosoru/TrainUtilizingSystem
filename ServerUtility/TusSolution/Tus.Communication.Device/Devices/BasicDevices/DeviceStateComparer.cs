using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tus.Communication
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
                var ara = x.ToByteArray();
                var arb = y.ToByteArray();
                return ara.SequenceEqual(arb);                
            }
        }

        public int GetHashCode(IDeviceState<IPacketDeviceData> obj)
        {
            return obj.ToByteArray().GetHashCode();
        }
    }
}
