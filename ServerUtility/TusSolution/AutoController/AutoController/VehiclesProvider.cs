using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Tus.AutoController.Deserialized;
namespace Tus.AutoController
{
    public class VehiclesProvider
    {
        public Func<IEnumerable<DeserializedVehicle>> VehiclesStatus { get; set; }

        public DeserializedVehicle FindByName(string vehicleName)
        {
            return this.VehiclesStatus().FirstOrDefault(v => v.Name.Contains(vehicleName));
        }

        public static VehiclesProvider ByEnumerable(params DeserializedVehicle[] e)
        {
            return new VehiclesProvider() { VehiclesStatus = () => e };
        }
    }
}

