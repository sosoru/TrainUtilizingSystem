using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouteLibrary.Base
{

    public class Vehicle
    {
        private static int LastVehicleID;
        public int VehicleID { get; private set; }

        public Route Route { get; set; }
        public CommandInfo Command { get; set;}

        public static Vehicle()
        {
            LastVehicleID = 0;
        }

        public Vehicle()
        {
            this.VehicleID = LastVehicleID++;
        }

        public bool Refresh()
        {
            bool is_refreshed = false;

            if(this.Route.IsSectionFinished)
            {
                this.Route.LockNextUnit();                
                is_refreshed = true;
            }

            if(this.Route.IsLeftSectionFirst)
            {
                this.Route.ReleaseBeforeUnit();
                is_refreshed = true;
            }

            return is_refreshed;
        }

        public Block Neighbor
        {
            get{ 
                return this.Route.Blocks.SkipWhile(b => this.CurrentBlock).FirstOrDefault();
            }}

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // TODO: write your implementation of Equals() here
            return this.VehicleID.Equals(((Vehicle)obj).VehicleID);
        }

        public bool operator =(Vehicle A, Vehicle B)
        {
            return A.Equals(B);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return this.VehicleID.GetHashCode();
        }

    }
}
