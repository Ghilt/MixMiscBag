﻿using System;
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
            AdventParser parser = new AdventParser(@"C:\Code\MixMiscBag\MixMiscBag\AdventOfCode2016\Input\");

            var problem = new Day2(parser);
            problem.PrintSolution();

            Console.ReadLine();
        }
    }
}
