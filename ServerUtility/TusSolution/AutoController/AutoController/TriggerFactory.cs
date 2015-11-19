using System;

namespace Tus.AutoController
{
    public class TriggerFactory
    {
        public VehiclesProvider VehiclesProvider { get; set; }
        public string VehicleDefaultName { get; set; }

        public WaitByTimeTrigger WaitByTime(TimeSpan span)
        {
            return new WaitByTimeTrigger()
                   {
                       ScheduledDateTime = DateTime.Now + span,
                       ScheduledTimeSpan = span,
                   };
        }

        public BlockReachedTrigger BlockReached(string vehiclename, string blockname)
        {
            return new BlockReachedTrigger()
                   {
                       VehiclesProvider = this.VehiclesProvider,
                       VehicleName = vehiclename,
                       BlockName = blockname,
                   };
        }

        public BlockReachedTrigger BlockReached(string blockname)
        {
            return this.BlockReached(this.VehicleDefaultName, blockname);
        }

        public SpeedReachedTrigger SpeedReached(string vehiclename,float targetspeed, float epsilon = 0.01f)
        {
            return new SpeedReachedTrigger()
                   {
                       VehicleName = vehiclename,
                       VehiclesProvider = this.VehiclesProvider,
                       Epsilon = epsilon,
                       TargetSpeed = targetspeed
                   };
        }

        public SpeedReachedTrigger SpeedReached(float targetspeed)
        {
            return SpeedReached(this.VehicleDefaultName, targetspeed);
        }
    }
}