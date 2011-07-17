using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SensorLibrary
{
    public class TrainControllerState
        : DeviceState<TrainControllerData>
    {
        public TrainControllerState(DevicePacket packet, TrainControllerData data, PacketServer server)
            : base(packet, data, server)
        {
            if (packet.ModuleType != ModuleTypeEnum.TrainController)
                throw new ArgumentException("incorrect module type");
        }

        public TrainControllerState(DevicePacket packet)
            : this(packet, null, null)
        {
        }

        private ushort DutyMask
        {
            get
            {
                var enable = (int)this.Data.dutyEnabledBits;
                return (ushort)((1 << enable) - 1);
            }
        }

        public int Duty
        {
            get { return this.Data.duty; }
            set { this.Data.duty = (ushort)((int)value & (int)DutyMask); }
        }

        public int DutyResolution
        {
            get { return this.Data.dutyEnabledBits; }
        }

        public double EssentialDutyResolution
        {
            get { return Math.Log10(this.DeviceFrequency / this.PWMFreqency) / Math.Log10(2);}
        }

        public bool IsSatisfiedDutyResolution
        {
            get { return this.EssentialDutyResolution >= this.DutyResolution;}
        }

        public byte DeviceRegisteredPeriod
        {
            get { return this.Data.period; }
            set
            {
                this.Data.period = value;
            }
        }

        public int PreScale
        {
            get
            {
                var res = (TrainControllerPrescale)this.Data.prescale;
                switch (res)
                {
                    case TrainControllerPrescale.PS_1_1:
                        return 1;
                    case TrainControllerPrescale.PS_1_4:
                        return 4;
                    case TrainControllerPrescale.PS_1_16:
                        return 16;
                }
                return -1; //not reaches
            }
            set
            {
                if (!(value == 1 || value == 4 || value == 16))
                    throw new InvalidOperationException("invalid prescale");

                TrainControllerPrescale scale = TrainControllerPrescale.PS_1_1 ;
                switch (value)
                {
                    case 1:
                        scale = TrainControllerPrescale.PS_1_1;
                        break;
                    case 4:
                        scale = TrainControllerPrescale.PS_1_4;
                        break;
                    case 16:
                        scale = TrainControllerPrescale.PS_1_16;
                        break;
                }

                this.Data.prescale = (byte)scale;
            }
        }

        public double DeviceFrequency
        {
            get
            {
                return (double)this.Data.frequency * 1000000.0; //multiply million
            }
        }

        public double DevicePeriod
        {
            get { return 1.0 / this.DeviceFrequency; }
        }

        public double PWMPeriod
        {
            //[(PR2) + 1] • 4 • TOSC • (TMR2 Prescale Value)
            get { return ((double)this.DeviceRegisteredPeriod + 1.0) * 4.0 * this.DevicePeriod * (double)this.PreScale; }
        }

        public double PWMFreqency
        {
            get { return 1.0 / this.PWMPeriod; }
        }
    }
}
