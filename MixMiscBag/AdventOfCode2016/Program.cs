using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Program
    {
        static void Main(string[] args)
        {

            AdventParser parser = new AdventParser(@"..\..\Input\");

            var problem = new Day1(parser);
            problem.PrintSolution();

            Console.ReadLine();
        }
    }
}
