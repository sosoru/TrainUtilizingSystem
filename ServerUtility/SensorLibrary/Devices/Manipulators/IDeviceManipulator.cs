using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive;
using System.Reactive.Linq;

using SensorLibrary;

namespace SensorLibrary.Manipulators
{
    public interface IDeviceManipulator
    {
        bool IsExecutable { get; }
        Action ExecuteFunc { get;}
    }
}
