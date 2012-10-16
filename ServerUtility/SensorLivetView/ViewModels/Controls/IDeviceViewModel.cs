using System;
using SensorLibrary;
using SensorLibrary.Devices;

namespace SensorLivetView.ViewModels.Controls
{
    public interface IDeviceViewModel<out TModel>
     where TModel : SensorLivetView.Models.Devices.IDeviceModel<IDevice<IDeviceState<IPacketDeviceData>>>
    {
        SensorLibrary.DeviceID DevID { get; }
        TModel Model { get;  }
        Livet.Command.DelegateCommand SendPacketCommand { get; }
    }
}
