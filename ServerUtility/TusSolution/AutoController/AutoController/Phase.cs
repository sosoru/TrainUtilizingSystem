using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Tus.AutoController
{
    [DataContract]
    public class Phase
    {
        public Phase()
        {
        }

        public Phase(string name)
        {
            // TODO: Complete member initialization
            this.Name = name;
            this.Speed = 0.1f;
            this.StayDistance = 1;
        }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public Trigger Trigger { get; protected set; }
        public TriggerFactory TriggerFactory { get; set; }
        public bool Evaluate()
        {
            return this.Trigger.CheckTriggered();
        }

        [DataMember]
        public float Speed { get; set; }

        [DataMember]
        public double Accelation { get; set; }

        [DataMember]
        public bool StayGoSignal { get; set; }

        public Func<TriggerFactory, Trigger> TriggerInitializer { get; set; }

        [DataMember(IsRequired =false, EmitDefaultValue=true)]
        public int StayDistance { get; set; }

        public void InitializeTrigger()
        {
            if (this.TriggerFactory != null)
                this.Trigger = this.TriggerInitializer(this.TriggerFactory);
        }

        [OnDeserializing]
        public void OnDeserializing(StreamingContext context)
        {
            this.StayDistance = 1;
        }
    }
}
