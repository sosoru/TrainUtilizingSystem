using System.Runtime.Serialization;

namespace Tus.AutoController
{
    [DataContract]
    public class BlockReachedTrigger : Trigger
    {
        public VehiclesProvider VehiclesProvider { get; set; }

        [DataMember]
        public string VehicleName { get; set; }
        [DataMember]
        public string BlockName { get; set; }
        public override bool CheckTriggered()
        {
            var vehi = VehiclesProvider.FindByName(this.VehicleName);

            if (vehi == null || vehi.CurrentBlock == null) return false; // triggering vehicle is found but is not prepared.

            return vehi.CurrentBlock.Name == this.BlockName; // triggering vehicle is reached to specified block.
        }
    }
}