using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ananke
{

    /* Ananke is the ancient greek deity of inevitability
     * Read more here: https://en.wikipedia.org/wiki/Ananke_(mythology)
     * 
     */
    class Program
    {
        static string SEARCH_FOR = @"!= ?null|null ?!=|== ?null";
        static string SEARCH_FOR_FILE_TYPE = ".java";
        static int totalOccurances = 0;
        static int totalFiles = 0;
        static int totalLines = 0;
        static int totalInfectedFiles = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Ananke-code analyzer\nAnanke is the ancient greek deity of inevitability");
            Console.WriteLine("searching for regex: \"" + SEARCH_FOR + "\" in *" +SEARCH_FOR_FILE_TYPE);
            Console.WriteLine();

            string folderPath = args[0]; 
            CountStringInFilesInFolders(folderPath);
            double fileInfectionRate = totalInfectedFiles * 1.0 / totalFiles;
            double averageOccurances = totalOccurances * 1.0 / totalFiles;
            double averageOccurancesInInfectedFile = totalOccurances * 1.0 / totalInfectedFiles;
            Console.WriteLine("Results:\n");
            Console.WriteLine("Number of files: " + totalFiles + " ");
            Console.WriteLine("Number of lines: " + totalLines + " ");
            Console.WriteLine("Files with string: " + totalInfectedFiles + " ({0:P2})", fileInfectionRate);
            Console.WriteLine("Total occurances: " + totalOccurances);
            Console.WriteLine("Average {0:N1} occurances in *" + SEARCH_FOR_FILE_TYPE, averageOccurances);
            Console.WriteLine("Average {0:N1} occurances in a file with at least 1 occurance", averageOccurancesInInfectedFile);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("This gives an Ananke-score of");
            Console.WriteLine("\n{0:N0}", 10 * fileInfectionRate * averageOccurances * averageOccurancesInInfectedFile);
            Console.ReadKey();
        }

        static void CountStringInFilesInFolders(string searchDir)
        {
            try
            {
                foreach (string fileName in Directory.GetFiles(searchDir))
                {
                    if(fileName.EndsWith(SEARCH_FOR_FILE_TYPE)){
                        var lines = File.ReadLines(fileName);
                        totalLines += lines.Count();
                        totalFiles++;
                        int found = lines.Select(line => Regex.Matches(line, SEARCH_FOR).Count).Sum();
                        totalOccurances += found;
                        if(found > 0){
                            totalInfectedFiles++;
                        }
                    }
                }
                foreach (string d in Directory.GetDirectories(searchDir))
                {
                    CountStringInFilesInFolders(d);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
            
        }
    }
}
