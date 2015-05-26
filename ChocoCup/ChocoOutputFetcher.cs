using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace ChocoCup
{
    class ChocoOutputFetcher : IVisitable
    {
        private const string LIST_PACKGES_COMMAND = "list";
        private const string LIST_LOCAL_PACKAGAES_ONLY_OPT = "-localonly";
        private const string DEFAULT_CHOCO_PATH = @"C:\ProgramData\chocolatey\bin\choco.exe";

        private ProcessStartInfo pStartInfo;
        private string chocoPath;
  
        private string args; // command-line arguments
        public ChocoOutputFetcher(string chocoPath, string args) 
        {
            this.args = LIST_PACKGES_COMMAND + " " + LIST_LOCAL_PACKAGAES_ONLY_OPT;
            
            if (args != null)
                this.args = args;

            if (chocoPath != null)
                this.chocoPath = chocoPath;
            else
                this.chocoPath = DEFAULT_CHOCO_PATH;

            pStartInfo = ProcessStartInfoBuilder();
        }
        public ChocoOutputFetcher() : this(null, null) { }
        public ChocoOutputFetcher(string chocoPath) : this(chocoPath, null) { }

        public List<String> GetProcessOutput()
        {
            string result;
            string[] splitResult;

            List<String> packages = new List<String>();
            ProcessOutputFetcher pof = new ProcessOutputFetcher(pStartInfo);


            result = pof.Fetch();
            splitResult = result.Split(new string[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in splitResult)
            {
                packages.Add(s);
            }

            return packages;
        }

        private ProcessStartInfo ProcessStartInfoBuilder()
        {
            /* Builds the default Process Start Info */
            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.FileName = chocoPath;
            startInfo.Arguments = args;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;

            return startInfo;
        }


        public List<string> Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
        
    }
}
