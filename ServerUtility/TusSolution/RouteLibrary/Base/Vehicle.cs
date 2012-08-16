using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouteLibrary.Base
{
    public class VehicleState
    {
        public Block Position { get; set; }
    }

    public class Vehicle
        : IObserver<VehicleState>
    {
        public Route Route;

        public Vehicle() { }


        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(VehicleState value)
        {
            throw new NotImplementedException();
        }
    }
}
