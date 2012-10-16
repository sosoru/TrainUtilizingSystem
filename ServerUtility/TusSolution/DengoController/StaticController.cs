using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;
using System.Reactive.Linq;

namespace DengoController
{
    public class StaticController : IDengoController
    {
        public IObservable<double> accellist;
        public IEnumerable<double> brakelist;

        public StaticController()
        {

        }

        public double AccelLevel
        {
            get;
            set;
            
        }

        public double BrakeLevel
        {
            get;
            set;
        }


        public bool Position
        {
            get { throw new NotImplementedException(); }
        }
    }
}
