﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SensorLibrary.Devices.TusAvrDevices;

namespace RouteLibrary.Base
{
    public enum SensingMethod
    {
        IRComming,
    }

    public class Halt
    {
        public Block HaltBlock { get; set; }
        public SensingMethod Method { get; set; }

        public Halt(Block blk) {
            this.HaltBlock = blk;
            this.Method = SensingMethod.IRComming;
        }

        public bool IRSensingHalt()
        {
            if (!this.HaltBlock.HasSensor)
                return true;

            var sens = this.HaltBlock.Detector;
            return sens.Devices.Any(s => s.IsDetected);
        }

        public bool HaltState
        {
            get
            {
                return IRSensingHalt();
            }
        }
    }
}