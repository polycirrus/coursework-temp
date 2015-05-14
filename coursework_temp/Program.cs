using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace coursework_temp
{
    class Program
    {
        static void Main(string[] args)
        {
            var drek = File.ReadAllText("E:\\misc\\dump\\text.txt");
            var anl = new SemanticAnalyzer(drek);
            Console.Write(anl.ToString());
            Console.ReadLine();
        }
    }
}
