using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Tus.AutoController
{
    [DataContract]
    public class Unten
    {
        [DataMember]
        public string VehicleName { get; set; }
        public VehiclesProvider VehiclesProvider { get; set; }
        [DataMember]
        public PhaseBatch PhaseBatch { get; set; }
        public bool IsWatching { get; private set; }
        private TriggerFactory _trigerFactory;

        public DataContractJsonSerializer Serializer
        {
            get
            {
                return new DataContractJsonSerializer(typeof(Unten),
                new[] {typeof (BlockReachedTrigger), typeof (SpeedReachedTrigger), typeof (WaitByTimeTrigger)
        });

            }
        }

        public Unten()
        {

        }

        public void SetParameterDefault()
        {
            this.VehiclesProvider = new VehiclesProvider()
            {
                VehiclesStatus = () => new[] { new Deserialized.DeserializedVehicle() },
            };
            this.VehicleName = "";
            this.PhaseBatch = new PhaseBatch();

        }

        public Deserialized.DeserializedVehicle GetMyVehicle()
        {
            return this.VehiclesProvider.FindByName(this.VehicleName);
        }

        public void InitializeWatching()
        {
            InitializeTriggerFactory();

            foreach (var p in this.PhaseBatch.Phases) p.TriggerFactory = this._trigerFactory;

            this.PhaseBatch.SetInitialPhase();

            this.IsWatching = true;
        }

        public void SkipPhases(int count)
        {
            for (int i = 0; i < count; i++)
                this.PhaseBatch.StepNextPhase();
        }

        private void InitializeTriggerFactory()
        {
            this._trigerFactory = new TriggerFactory()
            {
                VehicleDefaultName = this.VehicleName,
                VehiclesProvider = this.VehiclesProvider,
            };
        }


        public bool ContinueWatching()
        {
            var vehicle = VehiclesProvider.FindByName(this.VehicleName);

            if (vehicle == null) return false;

            var currentPhase = this.PhaseBatch.CurrentPhase;
            if (currentPhase == null) return false;

            // 先行列車が居るときはPhaseを進めない
            if (vehicle.Distance <= 1 && currentPhase.StayGoSignal) return false;

            if (!currentPhase.Evaluate()) return false;
            this.PhaseBatch.StepNextPhase();
            return true;
        }

        public void AbortWatching()
        {
            this.IsWatching = false;
        }

        public void RecreatePhaseTriggers()
        {
            this.InitializeTriggerFactory();
            foreach (var phase in this.PhaseBatch.Phases)
            {
                phase.TriggerFactory = this._trigerFactory;
                phase.InitializeTrigger();
            }
        }

        public Phase CreateNewPhase()
        {
            return new Phase()
            {
                TriggerFactory = this._trigerFactory,
            };
        }
    }

}