using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Tus.Communication;
using Tus.Communication.Device.AvrComposed;

namespace SensorLibrary.Devices.TusAvrDevices
{
    [DataContract]
    public class LedState
        :DeviceState<LedData>
    {
        public LedState()
            : base()
        {
            this.Data = new LedData();
        }

        [DataMember]
        public float Duty
        {
            get
            {
                return this.Data.DutyValue/128.0f;
            }
            set
            {
                if(value < 0.0f || value > 1.0f)
                    throw new ArgumentOutOfRangeException("Duty must be in [0, 1]");

                this.Data.DutyValue = (byte)Math.Round(value * 128.0f);
            }
        }
    }
}
