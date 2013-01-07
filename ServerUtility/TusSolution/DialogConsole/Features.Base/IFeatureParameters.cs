using System;
namespace DialogConsole.Features.Base
{
    public interface IFeatureParameters
    {
        System.Reactive.Concurrency.IScheduler SchedulerSendingProcessing { get; set; }
        Tus.Route.BlockSheet Sheet { get;  }
        System.Collections.Generic.IList<Tus.Route.Vehicle> Vehicles { get; }
    }
}
