using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandpiles3D
{
    class SandpilesCalculator
    {
        private const int MAX_AMOUNT = 6;

        private int[, ,] space;
        private int[, ,] delta;
        private int width;
        private int height;
        private int depth;
        public int iterationCounter { get; private set; }

        public SandpilesCalculator(int width, int height, int depth)
        {
            iterationCounter = 0;
            this.width = width;
            this.height = height;
            this.depth = depth;
            this.space = new int[width, height, depth];
            this.delta = new int[width, height, depth];
        }

        public void Fill(int value)
        {
            space.Fill(value);
        }

        public void FillMax()
        {
            space.Fill(MAX_AMOUNT);
        }

        internal void SetPosition(int x, int y, int z, int value)
        {
            space[x, y, z] = value;
        }


        public void Iterate()
        {
            delta = new int[width, height, depth];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        if (space[x, y, z] > MAX_AMOUNT)
                        {
                            Collapse(x, y, z);
                        }
                    }
                }
            }
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        space[x, y, z] += delta[x, y, z];
                    }
                }
            }
            iterationCounter++;
        }

        private void Collapse(int x, int y, int z)
        {
            delta[x, y, z] = -MAX_AMOUNT;
            AddDelta(x - 1, y, z);
            AddDelta(x + 1, y, z);
            AddDelta(x, y - 1, z);
            AddDelta(x, y + 1, z);
            AddDelta(x, y, z - 1);
            AddDelta(x, y, z + 1);
        }

        private void AddDelta(int x, int y, int z)
        {
            if (x < 0 || y < 0 || z < 0)
            {
                return;
            }
            else if (x >= width || y >= height || z >= depth)
            {
                return;
            }
            delta[x, y, z]++;
        }

        public int[,] GetCrossSection(int position, bool xDim, bool yDim, bool zDim)
        {
            if ((xDim ? 1 : 0) + (yDim ? 1 : 0) + (zDim ? 1 : 0) > 1)
            {
                throw new Exception("Only 2 dimensional cross sections allowed");
            }
            else if (xDim)
            {
                int[,] crossSection = new int[height, depth];
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        crossSection[y, z] = space[position, y, z];
                    }
                }
                return crossSection;
            }
            else if (yDim)
            {
                throw new NotImplementedException();
            }
            else if (zDim)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        internal Color[,] Get2DProjection()
        {
            int dims = 3;

            Color[,] projection = new Color[height, width];
            float[, ,] flatten = new float[height, width, dims];

            float[] biggestValue = new float[dims];

            float[,] multipliers = new float[depth, dims];
            for (int z = 0; z < depth; z++)
            {
                multipliers[z, 0] = z / (float)depth;
                multipliers[z, 1] = (depth - z) / (float)depth;
                multipliers[z, 2] = 1 - ((depth / 2 - z) / (float)depth / 2);
            }
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int lastValue = space[x, y, 0];
                    for (int z = 1; z < depth; z++)
                    {
                        int difference = Math.Abs(lastValue - space[x, y, z]);
                        for (int d = 0; d < dims; d++)
                        {
                            flatten[x, y, d] += difference * multipliers[z, d];
                        }
                    }

                    for (int d = 0; d < dims; d++)
                    {
                        if (flatten[x, y, d] > biggestValue[d])
                        {
                            biggestValue[d] = flatten[x, y, d];
                        }
                    }
                }
            }
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int d = 0; d < dims; d++)
                    {
                        flatten[x, y, d] = (flatten[x, y, d] / biggestValue[d]) * 250;
                    }
                    projection[x, y] = Color.FromArgb((int)flatten[x, y, 0], (int)flatten[x, y, 1], (int)flatten[x, y, 2]);
                }
            }
            return projection;
        }
    }

    public static class ArrayExtensions
    {
        public static void Fill<T>(this T[, ,] originalArray, T with) // http://stackoverflow.com/questions/5943850/fastest-way-to-fill-an-array-with-a-single-value
        {
            for (int x = 0; x < originalArray.GetLength(0); x++)
            {
                for (int y = 0; y < originalArray.GetLength(1); y++)
                {
                    for (int z = 0; z < originalArray.GetLength(2); z++)
                    {
                        originalArray[x, y, z] = with;
                    }
                }
            }
        }
    }
}
