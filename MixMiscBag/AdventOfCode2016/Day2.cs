using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Day2
    {
        AdventParser parser;
        private int x;
        private int y;
        KeyValuePair<char[,], Func<int, int, bool>> keyPad1 = new KeyValuePair<char[,], Func<int, int, bool>>(new char[3, 3] { 
        { '1', '4', '7' }, 
        { '2', '5', '8' }, 
        { '3', '6', '9' } },
        (x, y) =>
            (x > 2) ||
            (x < 0) ||
            (y > 2) ||
            (y < 0));
        KeyValuePair<char[,], Func<int, int, bool>> keyPad2 = new KeyValuePair<char[,], Func<int, int, bool>>(new char[5, 5] { 
        { '0', '0', '5', '0', '0' }, 
        { '0', '2', '6', 'A', '0' }, 
        { '1', '3', '7', 'B', 'D' }, 
        { '0', '4', '8', 'C', '0' }, 
        { '0', '0', '9', '0', '0' } },
        (x, y) =>
            (x != 2 && (y == 0 || y == 4)) ||
            (y != 2 && (x == 0 || x == 4)) ||
            (x > 4) ||
            (x < 0) ||
            (y > 4) ||
            (y < 0));

        string result = "";

        public Day2(AdventParser parser)
        {
            this.parser = parser;
            x = 1;
            y = 1;
            Solve(keyPad1);
            x = 0;
            y = 2;
            Solve(keyPad2);
        }

        private void Solve(KeyValuePair<char[,], Func<int, int, bool>> keyPad)
        {
            string[] data = parser.Parse("input_day2.txt", "\n");
            foreach (string instruction in data)
            {
                if (instruction.Equals(""))
                {
                    continue;
                }
                for (int i = 0; i < instruction.Length; i++)
                {
                    switch (instruction[i])
                    {
                        case 'U':
                            if (!keyPad.Value(x, y - 1)) y -= 1;
                            break;
                        case 'R':
                            if (!keyPad.Value(x + 1, y)) x += 1;
                            break;
                        case 'D':
                            if (!keyPad.Value(x, y + 1)) y += 1;
                            break;
                        case 'L':
                            if (!keyPad.Value(x - 1, y)) x -= 1;
                            break;
                        default:
                            break;
                    }
                }
                result += keyPad.Key[x, y];
            }
            result += "\n";
        }

        internal void PrintSolution()
        {
            Console.WriteLine("Solution " + result);
        }
    }
}
