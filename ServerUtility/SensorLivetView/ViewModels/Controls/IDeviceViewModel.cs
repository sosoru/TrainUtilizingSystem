using System;
namespace SensorLivetView.ViewModels.Controls
{
    interface IDeviceViewModel<out TModel>
     where TModel : SensorLivetView.Models.Devices.IDeviceModel<SensorLibrary.IDevice<SensorLibrary.IDeviceState<SensorLibrary.IPacketDeviceData>>>
    {
        SensorLibrary.DeviceID DevID { get; }
        TModel Model { get;  }
        Livet.Command.DelegateCommand SendPacketCommand { get; }
    }
}
