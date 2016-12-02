using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Day2
    {
        private int x = 1;
        private int y = 1;
        int[,] keyPad = new int[3, 3] { { 1, 4, 7 }, { 2, 5, 8 }, { 3, 6, 9 } };
        string result = "";

        public Day2(AdventParser parser)
        {
            string[] data = parser.Parse("input_day2.txt", "\n");

            foreach (string instruction in data)
            {
                if (instruction.Equals(""))
                {
                    return;
                }
                for (int i = 0; i < instruction.Length; i++)
                {
                    switch (instruction[i])
                    {
                        case 'U':
                            if(y != 0) y -= 1;
                            break;
                        case 'R':
                            if (x != 2) x += 1;
                            break;
                        case 'D':
                            if (y != 2) y += 1;
                            break;
                        case 'L':
                            if (x != 0) x -= 1;
                            break;
                        default:
                            break;

                    }
                }
                result += keyPad[x,y];
               
            }
        
        }

        internal void PrintSolution()
        {
            Console.WriteLine("Solution " + result);
        }
    }
}
