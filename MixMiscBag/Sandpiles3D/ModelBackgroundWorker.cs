using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandpiles3D
{
    public class ModelBackgroundWorker : BackgroundWorker //Probably an atrocious use of this class, I have no education or know no best practises for this
    {
        private bool cancelled;
        public SandpilesCalculator model { get; private set; }
        
        public ModelBackgroundWorker(SandpilesCalculator model)
        {
            this.model = model;
        }

        public void CancelAsyncModel()
        {
            CancelAsync();
            cancelled = true;
        }

        public bool IsCancelled()
        {
            return CancellationPending || cancelled;
        }

    }
}
