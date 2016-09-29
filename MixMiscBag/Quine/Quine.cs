using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuineNamespace
{
    class Quine
    {
        static string wl = "Console.Write(s + \"\\n\\t    string s = @\\\"\" + fix(s,\"\\\"\") + \"\\\";\\n\\t    \" + wl);\n\t    Console.ReadKey();\n\t}\n    }\n}";

        private static string fix(string s,string c)
        {
            string r = s.Insert(203, c).Insert(224, c).Insert(252, c).Insert(254, c).Insert(266, c).Insert(271, c);
            return r.Insert(274, c).Insert(281, c).Insert(286, c).Insert(300, c).Insert(350, c);
        }

        static void Main(string[] args)
        {
            string s = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuineNamespace
{
    class Quine
    {
        static string wl = ""Console.Write(s + \""\\n\\t    string s = @\\\""\"" + fix(s,\""\\\""\"") + \""\\\"";\\n\\t    \"" + wl);\n\t    Console.ReadKey();\n\t}\n    }\n}"";

        private static string fix(string s,string c)
        {
            string r = s.Insert(203, c).Insert(224, c).Insert(252, c).Insert(254, c).Insert(266, c).Insert(271, c);
            return r.Insert(274, c).Insert(281, c).Insert(286, c).Insert(300, c).Insert(350, c);
        }

        static void Main(string[] args)
        {";
            Console.Write(s + "\n\t    string s = @\"" + fix(s,"\"") + "\";\n\t    " + wl);
            Console.ReadKey();
        }
    }
}
