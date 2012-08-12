using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary;
using SensorLibrary.Packet.Data;

namespace SensorLibrary.Devices.TusAvrDevices
{
    public class SwitchState
        : DeviceState<SwitchData>
    {
        public SwitchState()
            : base()
        {
            this.BasePacket = new DevicePacket();
            this.BasePacket.ModuleType = ModuleTypeEnum.AvrSwitch;
            this.Data = new SwitchData();
        }

        //unit : msec
        public int DeadTime
        {
            get
            {
                return this.Data.DeadTime + 100;
            }
            set
            {
                var raw = value - 100;
                if (raw < 0 || raw > 255)
                    throw new ArgumentOutOfRangeException("DeadTime must be in [100, 355]");

                this.Data.DeadTime = (byte)raw;
            }
        }

        public int ChangingTime
        {
            get
            {
                return this.Data.ChangingTime * 10;
            }
            set
            {
                var raw = Math.Round(((float)value) / 10.0f);
                if (raw < 0 || raw > 100)
                    throw new ArgumentOutOfRangeException("ChangingTime must be in [0, 1005]");

                this.Data.ChangingTime = (byte)raw;
            }
        }

        public PointStateEnum Position
        {
            get
            {
                return (PointStateEnum)this.Data.Position;
            }
            set
            {
                this.Data.Position = (byte)value;
            }
        }

    }
}
