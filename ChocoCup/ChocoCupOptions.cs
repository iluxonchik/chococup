using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoCup
{
    class ChocoCupOptions
    {
        public string ChocoPath { get; set; }
        public string OutFilePath { get; set; }
        public bool PrintScript { get; set; }

        public ChocoCupOptions()
        {
            ChocoPath = null;
            OutFilePath = null;
            PrintScript = false;
        }
    }
}
