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
        BlockSheet Sheet { get; set; }
        System.Collections.Generic.IList<Vehicle> Vehicles { get; set; }
        IDisposable ServingInfomation { get; set; }
        IScheduler SchedulerPacketProcessing { get; set; }

        IObservable<DevicePacket> SendingPacketPipeline { get; set; }

        IObservable<Unit> VehiclePipeline { get; set; }
        IDisposable VehicleProcessing { get; set; }
        IObservable<DevicePacket> ReceivingPacketPipeline { get; set; }
        RouteListFactory AvailableRoutesFactory { get; set; }
    }
}
