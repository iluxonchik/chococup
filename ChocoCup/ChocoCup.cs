using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocoCup
{
    class ChocoCup
    {
        private const string LIST_PACKGES_COMMAND = "list";
        private const string LIST_LOCAL_PACKAGAES_ONLY_OPT = "-localonly";

        private string args; // command-line arguments
        public ChocoCup(string args) 
        {
            this.args = LIST_PACKGES_COMMAND + LIST_LOCAL_PACKAGAES_ONLY_OPT;
            this.args += args; 
        }
        public ChocoCup() : this("") { }

    }
}
