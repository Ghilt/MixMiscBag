using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandpiles3D
{
    class SandPilesIterationData
    {
        public int iteration { get; private set; }
        public Color[,] dim2Projection { get; private set; }


        public SandPilesIterationData(int iteration, Color[,] dim2Projection)
        {
            this.iteration = iteration;
            this.dim2Projection = dim2Projection;
        }
    }
}
