using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ChocoCup
{
    class ProcessOutputFetcher
    {
        public ProcessStartInfo PStartInfo
        {
            set { pStartInfo = value; }
            get { return pStartInfo; }
        }

        private ProcessStartInfo pStartInfo;
        private Process process;
        public ProcessOutputFetcher(ProcessStartInfo pStartInfo)
        {
            this.pStartInfo = pStartInfo;
        }

        public string fetch()
        {
            /*  Fetch the output from the process */
            process.StartInfo = pStartInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return output;
        }
        /* TODO:
         * 1. Make sure that program is run as admin
         * 2. Check if file exists.
         * */
    }
}
