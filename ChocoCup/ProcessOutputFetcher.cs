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
        public string ProgPath
        {
            set
            { progPath = value; }
            get
            { return progPath; }
        }


        public ProcessStartInfo PStartInfo
        {
            set { pStartInfo = value; }
            get { return pStartInfo; }
        }

        private string progPath;
        ProcessStartInfo pStartInfo;
        public ProcessOutputFetcher(string progPath, ProcessStartInfo pStartInfo)
        {
            this.pStartInfo = pStartInfo;
            this.progPath = progPath;
        }

        public string fetch()
        {
            /*  Fetch the output from the process */
            // TODO
            return null;
        }
        /* TODO:
         * 1. Make sure that program is run as admin
         * 2. Check if file exists.
         * */
    }
}
