using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Tus.AutoController
{
    [DataContract]
    public class Trigger
    {
        public virtual bool CheckTriggered()
        {
            return false;
        }

    }
}
