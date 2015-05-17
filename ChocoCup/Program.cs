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
            PSScriptBuilder<string> ps = new PSScriptBuilder<string>(new ChocoCup().Accept(new FullPackageParserVisitor()));
            Console.WriteLine(ps.BuildScript());
            Console.ReadLine();
        }
    }
}
