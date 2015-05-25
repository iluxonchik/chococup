using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace ChocoCup
{
    class Program
    {
        static void Main(string[] args)
        {
     
           ChocoCup cs = new ChocoCup(args);
            try { 
           cs.Run();
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
