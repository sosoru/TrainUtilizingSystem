using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using Tus.Communication;

namespace Tus.Communication.Device.AvrComposed
{
    [DataContract]
    public class MotorState
        : DeviceState<MotorData>
    {
        public MotorState()
            : base()
        {
            this.Data = new MotorData();
        }

        [IgnoreDataMember]
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

        [DataMember(Name = "ControlMode")]
        public string ControlModeString
        {
            get { return Enum.GetName(typeof(MotorControlMode), this.ControlMode); }
            set
            {
                this.ControlMode = (MotorControlMode)Enum.Parse(typeof(MotorControlMode), value);
            }
        }

        [IgnoreDataMember]
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

        [DataMember(Name = "Direction")]
        public string DirectionString
        {
            get { return Enum.GetName(typeof(MotorDirection), this.Direction); }
            set
            {
                this.Direction = (MotorDirection)Enum.Parse(typeof(MotorDirection), value);
            }
        }

        [DataMember]
        public float Duty
        {
            get
            {
                return (float)this.Data.Duty / 255.0f;
            }
            set
            {
                if (value < 0.0f || value > 1.0f)
                    throw new ArgumentOutOfRangeException("Duty value must be in [0, 1]");

                this.Data.Duty = (byte)Math.Round(value * 255.0f);
            }
        }

        [DataMember]
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

        [DataMember]
        [Obsolete("ThresholdCurrentの設定は反映されません")]
        public float ThresholdCurrent
        {
            get
            {
                return 0;// (float)this.Data.ThresholdValue / 255.0f * 5.0f;
            }
            set
            {
                //if (value < 0.0f || value > 5.0f)
                //    throw new ArgumentOutOfRangeException("Current value must be in [0, 5]");

                //this.Data.ThresholdValue = (byte)Math.Round(value / 5.0f * 255.0f);
            }
        }

        [IgnoreDataMember]
        public DeviceID DestinationID
        {
            get { return this.Data.DestinationID; }
            set { this.Data.DestinationID = value; }
        }

        [DataMember(Name = "DestinationID")]
        public string DestinationIDString
        {
            get { return this.DestinationID.ToString(); }
            set { this.DestinationID = DeviceIdParser.FromString(value).First(); }
        }

        [IgnoreDataMember]
        [DataMember(Name = "MemoryWhenEntered")]
        public string MemoryWhenEnteredString
        {
            get { return Enum.GetName(typeof(MotorMemoryStateEnum), this.MemoryWhenEntered); }
            set
            {
                this.MemoryWhenEntered = (MotorMemoryStateEnum)Enum.Parse(typeof(MotorMemoryStateEnum), value);
            }
        }

        [IgnoreDataMember]
        public MotorMemoryStateEnum MemoryWhenEntered
        {
            get { return (MotorMemoryStateEnum)(this.Data.TransitMemory & 0x0F); }
            set
            {
                this.Data.TransitMemory &= 0xF0;
                this.Data.TransitMemory |= Convert.ToByte((byte)value & 0x0F);
            }
        }

        [DataMember(Name = "DestinationMemory")]
        public string DestinationMemoryString
        {
            get { return Enum.GetName(typeof(MotorMemoryStateEnum), this.DestinationMemory); }
            set
            {
                this.DestinationMemory = (MotorMemoryStateEnum)Enum.Parse(typeof(MotorMemoryStateEnum), value);
            }
        }

        public MotorMemoryStateEnum DestinationMemory
        {
            get { return (MotorMemoryStateEnum)(this.Data.TransitMemory >> 4); }
            set
            {
                var entered = (int)this.Data.TransitMemory;
                entered &= 0x0F;
                entered |= ((int)value) << 4;
                this.Data.TransitMemory = (byte)entered;
            }
        }

        [IgnoreDataMember]
        public MotorMemoryStateEnum TargetMemory
        {
            get
            {
                return (MotorMemoryStateEnum)this.Data.CurrentMemory;
            }
            set { this.Data.CurrentMemory = (byte)value; }
        }

        public override string ToString()
        {
            return base.ToString() + string.Format("|mt : dir={0}, duty={1}, curr={2}, mode={3}, mem={4}",
                                                   this.DirectionString,
                                                   this.Duty,
                                                   this.Current,
                                                   this.ControlModeString,
                                                   this.TargetMemory);
        }

    }

    public enum MotorControlMode
        : byte
    {
        Unknown = 0x00,
        DutySpecifiedMode = 0x01,
        CurrentFeedBackMode = 0x02,
        WaitingPulseMode = 0x03,
    }

    //todo:進行方向と極性を一致させるためのカプセル化
    //テスト線に併せて極性逆になってた
    public enum MotorDirection
        : byte
    {
        Standby = 0x00,
        Positive = 0x01,
        Negative = 0x02,
    }

}
