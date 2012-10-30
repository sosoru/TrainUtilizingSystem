using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary.Packet.Data;

namespace SensorLibrary.Devices.TusAvrDevices
{
    public class MotorState
        : DeviceState<MotorData>
    {
        public MotorState()
            : base()
        {
            this.Data = new MotorData();        
        }

        public MotorControlMode ControlMode
        {
            get
            {
                return (MotorControlMode)this.Data.ControlMode;
            }
            set
            {
                this.Data.ControlMode = (byte)value;
            }
        }

        public MotorDirection Direction
        {
            get
            {
                return (MotorDirection)this.Data.Direction;
            }
            set
            {
                this.Data.Direction = (byte)value;
            }
        }

        public float Duty
        {
            get
            {
                return (float)this.Data.Duty / 255.0f;
            }
            set
            {
                if(value < 0.0f || value > 1.0f)
                    throw new ArgumentOutOfRangeException("Duty value must be in [0, 1]");

                this.Data.Duty = (byte)Math.Round(value * 255.0f);
            }
        }

        public float Current
        {
            get
            {
                return (float)this.Data.Current / 255.0f * 5.0f;
            }
            set
            {
                if (value < 0.0f || value > 5.0f)
                    throw new ArgumentOutOfRangeException("Current value must be in [0, 5]");

                this.Data.Current = (byte)Math.Round(value / 5.0f * 255.0f);
            }
        }

        public float ThresholdCurrent
        {
            get
            {
                return (float)this.Data.ThresholdValue / 255.0f * 5.0f;
            }
            set
            {
                if (value < 0.0f || value > 5.0f)
                    throw new ArgumentOutOfRangeException("Current value must be in [0, 5]");

                this.Data.ThresholdValue = (byte)Math.Round(value / 5.0f * 255.0f);
            }
        }

        public DeviceID DestinationID
        {
            get { return this.Data.DestinationID; }
            set { this.Data.DestinationID = value; }
        }

        public MotorMemoryStateEnum MemoryWhenEntered
        {
            get { return (MotorMemoryStateEnum)this.Data.MemoryAfterEntered; }
            set { this.Data.MemoryAfterEntered = (byte)value; }
        }

        public MotorMemoryStateEnum DestinationMemory
        {
            get { return (MotorMemoryStateEnum)this.Data.DestinationMemory; }
            set { this.Data.DestinationMemory = (byte)value; }
        }
 
    }

    public enum MotorControlMode
        : byte
    {
        DutySpecifiedMode = 0x01,
        CurrentFeedBackMode = 0x02,
        WaitingPulseMode = 0x03,
    }

    public enum MotorDirection
        : byte
    {
        Standby = 0x00,
        Positive = 0x01,
        Negative = 0x02,
    }

}
