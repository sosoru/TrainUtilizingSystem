using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.IO.Ports;
using System.Reactive.Linq;
using System.Reactive;

namespace Tus.Communication
{
    public enum EthCommandEnum
        : byte
    {

    }

    public enum EthErrorEnum
        : byte
    {

    }

    public enum ModuleTypeEnum
        : byte
    {
        MotherBoard = 0x00,
        TrainSensor = 0x01,
        PointModule = 0x02,
        TrainController = 0x03,
        RemoteModule = 0x04,

        AvrKernel = 0x11,
        AvrMotor = 0x12,
        AvrSwitch = 0x13,
        AvrSensor = 0x14,
        AvrUartSetting = 0x15,

        Unknown = 0x0F,
    }


}