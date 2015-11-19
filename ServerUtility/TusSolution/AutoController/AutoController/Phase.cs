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

        public void InitializeTrigger()
        {
            this.Trigger = this.TriggerInitializer(this.TriggerFactory);
        }
    }
}
