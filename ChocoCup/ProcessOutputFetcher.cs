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
        private const string INVALID_OP_EXCEPTION_MSG = "The StandardOutput stream has not been defined for redirection;" + 
            "ensure ProcessStartInfo.RedirectStandardOutput is set to true and ProcessStartInfo.UseShellExecute is set to false.";

        private const string CANT_START_PROCEESS_MSG = "Error starting the process. Make sure the path to chocolatey executable is correct.";
        private const string GENERAL_PROCESS_ERR_MSG = "Could not get output from process.";

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
            this.process = new Process();
        }

        public string Fetch()
        {
            /*  Fetch the output from the process */
            string output = null;

            process.StartInfo = pStartInfo;

       
            try { process.Start(); }
            catch (Exception) { throw new ProcessOutputFetcherException(CANT_START_PROCEESS_MSG + " You specified the following path: " + PStartInfo.FileName); }

            try
            {
                output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }
            catch (Exception) { throw new ProcessOutputFetcherException(GENERAL_PROCESS_ERR_MSG); }

            return output;
        }

    }
}
