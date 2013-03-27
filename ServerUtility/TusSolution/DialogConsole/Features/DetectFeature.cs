﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using DialogConsole.Features.Base;

namespace DialogConsole.Features
{
    [FeatureMetadata("3", "detect test")]
    [Export(typeof(IFeature))]
    class DetectFeature
        : BaseFeature, IFeature
    {
        public void Execute()
        {
            this.Param.Sheet.ChangeDetectingMode();
            System.Threading.Thread.Sleep(1000);

            this.Param.Sheet.InquiryAllMotors();
            System.Threading.Thread.Sleep(2000);

            Console.WriteLine(this.Param.Sheet.InnerBlocks
               .Where(b => b.IsDetectingTrain || b.IsMotorDetectingTrain)
                .Aggregate("", (ac, b) => ac += b.Name + ", "));
        }

        public void Init()
        {
        }
    }
}