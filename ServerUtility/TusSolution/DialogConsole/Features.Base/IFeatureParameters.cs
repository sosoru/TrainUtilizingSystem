using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;
using DialogConsole.WebPages;
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

        IObservable<Unit> SendingPacketPipeline { get; set; }

        IEnumerable<Lazy<IConsolePage, ITusPageMetadata>> Pages { get; set; }

        IObservable<Unit> VehiclePipeline { get; set; }
        IObservable<Unit> SyncDevicePipeline { get; set; }

        IDisposable VehicleProcessing { get; set; }
        IDisposable SyncDeviceProcessing { get; set; }

        IObservable<DevicePacket> ReceivingPacketPipeline { get; set; }
    }
}
