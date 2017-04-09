﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sandpiles3DWPF.Model
{
    class SandpilesIterationData
    {
        public int iteration { get; private set; }
        public Color[,] dim2Projection { get; private set; }


        public SandpilesIterationData(int iteration, Color[,] dim2Projection)
        {
            this.iteration = iteration;
            this.dim2Projection = dim2Projection;
        }
    }
}
