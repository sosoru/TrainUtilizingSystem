using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Tus.AutoController
{
    [DataContract]
    public class PhaseBatch
    {
        [DataMember]
        public List<Phase> Phases { get; set; }

        public PhaseBatch()
        {
            this.Phases = new List<Phase>();

        }
        public void SetInitialPhase()
        {
            if (_enumerator != null)
                _enumerator.Dispose();

            _enumerator = this.Phases.GetEnumerator();
            if (_enumerator.MoveNext())
            {
                this.CurrentPhase = _enumerator.Current;
                if (this.CurrentPhase.TriggerInitializer != null) this.CurrentPhase.InitializeTrigger();
            }
            else
            {
                throw new ApplicationException("Phases長さが0なので初期化できない");
            }
        }

        public void StepNextPhase()
        {
            if (_enumerator == null)
                throw new ApplicationException("enumerator == null, SetInitialPhaseが呼び出されていない？");
            if (_enumerator.MoveNext())
            {
                this.CurrentPhase = _enumerator.Current;
                if (this.CurrentPhase.TriggerInitializer != null) this.CurrentPhase.InitializeTrigger();
            }
            else
            {
                this.SetInitialPhase();
            }
        }

        private IEnumerator<Phase> _enumerator;
        public Phase CurrentPhase { get; set; }
    }
}
