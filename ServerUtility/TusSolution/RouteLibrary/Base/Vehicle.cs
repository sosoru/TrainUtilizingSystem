﻿using System;
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
        public BlockSheet Sheet { get; set; }

        static Vehicle()
        {
            LastVehicleID = 0;
        }

        public Vehicle(BlockSheet sht, Route rt)
        {
            this.VehicleID = LastVehicleID++;

            this.Sheet = sht;
            this.Route = rt;
        }

        public bool Refresh()
        {
            bool is_refreshed = false;

            if (this.Route.IsSectionFinished)
            {
                this.Route.LockNextUnit();
                is_refreshed = true;
            }

            if (this.Route.IsLeftSectionFirst)
            {
                this.Route.ReleaseBeforeUnit();
                is_refreshed = true;
            }

            return is_refreshed;
        }

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

        public static bool operator ==(Vehicle A, Vehicle B)
        {
            return A.Equals(B);
        }

        public static bool operator !=(Vehicle A, Vehicle B)
        {
            return !(A.Equals(B));
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return this.VehicleID.GetHashCode();
        }

        public void Run()
        {
            var cmd = new CommandInfo()
            {
                Route = this.Route,
                Speed = 0.5f,
            };

            this.Sheet.Effect( cmd , this.Route.LockedBlocks);

        }
    }
}
