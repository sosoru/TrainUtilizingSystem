using System;
using System.Runtime.Serialization;

namespace Tus.AutoController
{
    [DataContract]
    public class WaitByTimeTrigger : Trigger
    {
        [DataMember]
        public DateTime ScheduledDateTime { get; set; }
        [DataMember]
        public TimeSpan ScheduledTimeSpan { get; set; }

        public override bool CheckTriggered()
        {
            return this.ScheduledDateTime <= DateTime.Now;
        }
    }
}