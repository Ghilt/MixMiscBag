using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class AdventParser
    {
        private string inputBasePath;

        public AdventParser(string inputBasePath)
        {
            this.inputBasePath = inputBasePath;
        }

        public string[] Parse(string fileName, string separator)
        {
            string filePath = inputBasePath + fileName;
            return ParseToString(filePath).Split(new string[] { separator }, StringSplitOptions.None);
        }

        private string ParseToString(string filePath)
        {
            return System.IO.File.ReadAllText(filePath);
        }
    }
}
