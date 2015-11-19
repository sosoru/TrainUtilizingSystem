using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Tus.TransControl.Base
{
    public enum SensingMethod
    {
        IRComming,
        None,
    }

    [DataContract]
    public class Halt
    {
        [DataMember]
        public virtual Block HaltBlock { get; set; }
        [DataMember]
        public SensingMethod Method { get; set; }

        public Halt(Block blk)
        {
            this.HaltBlock = blk;
        }
        public Halt() { }

        public bool IRSensingHalt()
        {
            if (!this.HaltBlock.HasSensor)
                return true;

            var sens = this.HaltBlock.Detector;
            return sens.Devices.Any(s => s.IsDetected);
        }

        private bool _haltstate = false;
        public virtual bool HaltState
        {
            get
            {
                if (this.Method == SensingMethod.IRComming)
                    return IRSensingHalt();
                else
                    return _haltstate;
            }
            set
            {
                if (this.Method == SensingMethod.IRComming)
                    throw new InvalidOperationException("cannot set haltstate if method is IRcomming");
                else
                    _haltstate = value;
            }
        }
    }
}
