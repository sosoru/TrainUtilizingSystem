using System;
using System.Reactive.Concurrency;

namespace DialogConsole.Features.Base
{
    public interface IFeatureParameters
    {
        Tus.Route.BlockSheet Sheet { get; set; }
        System.Collections.Generic.IList<Tus.Route.Vehicle> Vehicles { get; set; }
        IDisposable ServingInfomation { get; set; }
        IScheduler SchedulerPacketProcessing{  get; set;} 

    }
}
