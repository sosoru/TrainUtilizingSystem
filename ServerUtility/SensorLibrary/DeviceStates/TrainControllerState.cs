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

        private ushort getmask(byte cnt)
        {
            return (ushort)((1 << cnt) -1);
        }

        private ushort dutyMask
        {
            get
            {
                return getmask(this.Data.dutyEnabledBits);
            }
        }

        private ushort voltageMask
        {
            get
            {
                return getmask(this.Data.voltageEnabledBits);
            }
        }

        public int Duty
        {
            get { return this.Data.duty; }
            set { this.Data.duty = (ushort)((int)value & (int)dutyMask); }
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

                this.Data.prescale = scale;
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

        public TrainControllerDirection Direction
        {
            get
            {
                return this.Data.direction;
            }
            set
            {
                this.Data.direction = value;
            }
        }

        public TrainControllerMode ControllerMode
        {
            get
            {
                return this.Data.mode;
            }
            set
            {
                this.Data.mode = value;
            }
        }

        public int Voltage
        {
            get
            {
                return (int)this.Data.voltage;
            }
            set
            {
                this.Data.voltage = (ushort)((ushort)value & this.voltageMask);
            }
        }

        public int MeisuredVoltage
        {
            get
            {
                return (int)this.Data.meisuredVoltage;
            }
        }

        public int MeisuredVoltage2
        {
            get
            {
                return (int)this.Data.meisuredVoltage2;
            }
        }

        public PidParams PidParams
        {
            get
            {
                return new PidParams()
                {
                    paramp = this.Data.paramp,
                    parami = this.Data.parami,
                    //paramd = this.Data.paramd,
                };
            }
            set
            {
                this.Data.paramp = value.paramp;
                this.Data.parami = value.parami;
                //this.Data.paramd = value.paramd;
            }
        
        }
        
    }
}
