using System;
using System.Runtime.Serialization;

namespace Tus.AutoController
{
    [DataContract]
    public class SpeedReachedTrigger : Trigger
    {
        [DataMember]
        public string VehicleName { get; set; }

        [DataMember]
        public float TargetSpeed { get; set; }
        [DataMember]
        public float Epsilon { get; set; }

        public VehiclesProvider VehiclesProvider { get; set; }

        public override bool CheckTriggered()
        {
            var vehi = VehiclesProvider.FindByName(this.VehicleName);

            if (vehi == null) return false; // target vehicle is not contained

            return Math.Abs(this.TargetSpeed - vehi.CurrentSpeed) < this.Epsilon;
        }

    }
}