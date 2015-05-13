using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ChocoCup
{
    class Program
    {
        static void Main(string[] args)
        {
            ChocoCup cc = new ChocoCup();
            List<String> l = cc.getPackageNames();
            foreach(string s in l)
                Console.WriteLine(s);
            Console.ReadLine();
        }
    }
}
