using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Day1
    {
        private const char TURN_LEFT = 'L';
        private AdventParser parser;
        private int currentDirection = 0;
        private int x = 0;
        private int y = 0;

        public Day1(AdventParser parser)
        {
            this.parser = parser;
            string[] data = parser.Parse("input_day1.txt", ", ");

            foreach (string instruction in data)
            {
                char turn = instruction[0];
                int distance = Int32.Parse(instruction.Substring(1));
                currentDirection += turn == TURN_LEFT ? -1 : 1;
                currentDirection = (4+currentDirection) % 4; // this is madness c#, madness
                switch(currentDirection){
                    case 0:
                        y += distance; 
                        break;
                    case 1:
                        x += distance;
                        break;
                    case 2:
                        y -= distance;
                        break;
                    case 3:
                        x -= distance;
                        break;
                    default:
                        throw new Exception();
                }
            }
        
        }

        internal void PrintSolution()
        {
            Console.WriteLine("Solution " + Math.Abs(x + y));
        }
    }
}
