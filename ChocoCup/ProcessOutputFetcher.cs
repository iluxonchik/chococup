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

        public ProcessStartInfo PStartInfo
        {
            set { pStartInfo = value; }
            get { return pStartInfo; }
        }

        private ProcessStartInfo pStartInfo;
        private Process process;
        public ProcessOutputFetcher(ProcessStartInfo pStartInfo, bool requireAdmin)
        {
            if (requireAdmin)
                CheckAdminPermissions();

            this.pStartInfo = pStartInfo;
            this.process = new Process();
        }

        public string Fetch()
        {
            /*  Fetch the output from the process */
            string output = null;

            process.StartInfo = pStartInfo;

            try 
            {
                // TODO: throw exceptions, specify exceptions
                process.Start();
                output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }
            catch (ArgumentNullException e) { Console.WriteLine(e.Message); }
            catch (InvalidOperationException e) { Console.WriteLine(e.Message + "\n" + INVALID_OP_EXCEPTION_MSG ); }
            catch (Exception e) { Console.WriteLine(e.Message); }

            return output;
        }

        private void CheckAdminPermissions()
        {
            // TODO
        }

        /* TODO:
         * 1. Make sure that program is run as admin
         * 2. Check if file exists.
         * */
    }
}
