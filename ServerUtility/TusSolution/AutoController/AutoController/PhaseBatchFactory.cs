using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Tus.AutoController
{
    public class PhaseBatchFactory
    {
        public virtual PhaseBatch Create()
        {
            return new PhaseBatch();
        }

    }
}
