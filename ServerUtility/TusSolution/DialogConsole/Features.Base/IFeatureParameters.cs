using System;
using System.Reactive;
using System.Reactive.Concurrency;
using Tus.Communication;
using Tus.Factory;
using Tus.TransControl.Base;

namespace DialogConsole.Features.Base
{
    public interface IFeatureParameters
    {
        Layout UsingLayout { get; set; }
        IDisposable ServingInfomation { get; set; }
        IScheduler SchedulerPacketProcessing { get; set; }

        IObservable<DevicePacket> SendingPacketPipeline { get; set; }

        IObservable<Unit> VehiclePipeline { get; set; }
        IDisposable VehicleProcessing { get; set; }
        IObservable<DevicePacket> ReceivingPacketPipeline { get; set; }
    }
}
