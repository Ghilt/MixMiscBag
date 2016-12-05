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
        private HashSet<KeyValuePair<int, int>> visited = new HashSet<KeyValuePair<int, int>>();
        private int x = 0;
        private int y = 0;

        public Day1(AdventParser parser)
        {
            this.parser = parser;
            string[] data = parser.Parse("input_day1.txt", ", ");
            visited.Add(new KeyValuePair<int, int>(0, 0));

            foreach (string instruction in data)
            {
                char turn = instruction[0];
                int distance = Int32.Parse(instruction.Substring(1));
                currentDirection += turn == TURN_LEFT ? -1 : 1;
                currentDirection = (4 + currentDirection) % 4; // this is madness c#, madness
                switch (currentDirection)
                {
                    case 0:
                        CheckIfVisited(0, distance);
                        break;
                    case 1:
                        CheckIfVisited(distance, 0);
                        break;
                    case 2:
                        CheckIfVisited(0, -distance);
                        break;
                    case 3:
                        CheckIfVisited(-distance, 0);
                        break;
                    default:
                        throw new Exception();
                }
            }
        }

        private void CheckIfVisited(int x, int y)
        {
            int sign = (x < 0 || y < 0) ? -1 : 1;
            Func<int, int, bool> cond = (v1, v2) => sign < 0 ? v1 >= v2 : v1 <= v2;
            Func<int, int, int, int> filterOnDimension = (startValue, distance, dimensionRelevant) => dimensionRelevant != 0 ? startValue + distance : startValue;

            int target = (x != 0) ? x : y;

            for (int d = sign; cond(d, target); d += sign)
            {
                int xCoord = filterOnDimension(this.x, d, x);
                int yCoord = filterOnDimension(this.y, d, y);
                KeyValuePair<int, int> current = new KeyValuePair<int, int>(xCoord, yCoord);
                if (visited.Contains(current))
                {
                    Console.WriteLine("Crossing at  (" + current.Key + "," + current.Value + ") " + (Math.Abs(current.Key) + Math.Abs(current.Value)));
                }
                visited.Add(current);
            }

            this.x += x;
            this.y += y;
        }

        internal void PrintSolution()
        {
            Console.WriteLine("Solution " + (Math.Abs(x) + Math.Abs(y)));
        }

    }
}
